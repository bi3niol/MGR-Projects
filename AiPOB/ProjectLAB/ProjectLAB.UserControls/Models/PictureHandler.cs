using OxyPlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectLAB.UserControls.Models
{
    [Serializable]
    public class PictureHandler : ICloneable
    {
        #region Fields
        public readonly int PixelWidth;
        public readonly int PixelHeight;
        public readonly double DpiX;
        public readonly double DpiY;

        public int Stride { get; private set; }

        private const int SpacePerPixel = 4;
        private const int RedOffset = 0;
        private const int GreenOffset = 1;
        private const int BlueOffset = 2;
        private const int AlphaOffset = 3;

        private bool isHistogramPreparing = false;
        private int[] rHistogramData;
        private int[] gHistogramData;
        private int[] bHistogramData;

        [NonSerialized]
        public List<DataPoint> HistogramDataR;
        [NonSerialized]
        public List<DataPoint> HistogramDataG;
        [NonSerialized]
        public List<DataPoint> HistogramDataB;

        [NonSerialized]
        public List<DataPoint> VerticalProjectionData;
        [NonSerialized]
        public List<DataPoint> HorizontalProjectionData;

        private byte[] imin = new byte[3];
        private byte[] imax = new byte[3];

        public Color Imin
        {
            get
            {
                return Color.FromRgb(imin[0], imin[1], imin[2]);
            }
            private set
            {
                imin[0] = value.R;
                imin[1] = value.G;
                imin[2] = value.B;
            }
        }
        public Color Imax
        {
            get
            {
                return Color.FromRgb(imax[0], imax[1], imax[2]);
            }
            private set
            {
                imax[0] = value.R;
                imax[1] = value.G;
                imax[2] = value.B;
            }
        }
        public byte[] data { get; private set; }
        #endregion
        public PictureHandler(byte[] data, int pixelWidth, int pixelHeight, double dpiX, double dpiY)
        {
            this.data = data;
            PixelHeight = pixelHeight;
            PixelWidth = pixelWidth;

            Stride = SpacePerPixel * PixelWidth;
            DpiX = dpiX;
            DpiY = dpiY;
        }

        #region Indexers
        public Color this[int x, int y]
        {
            get
            {
                return GetPixelColor(x, y);
            }
            set
            {
                SetPixelColor(x, y, value);
            }
        }
        #endregion

        #region methods
        //Privates
        private int GetPositionOfPixel(int x, int y)
        {
            //clip positions
            x = x < 0 ? 0 : x >= PixelWidth ? PixelWidth - 1 : x;
            y = y < 0 ? 0 : y >= PixelHeight ? PixelHeight - 1 : y;

            int position = y * Stride + SpacePerPixel * x;
            return position;
        }

        public void GetHistogramDataFor(int i, out float r, out float g, out float b)
        {
            r = g = b = 0;
            if (rHistogramData != null)
            {
                r = rHistogramData[i];
                g = gHistogramData[i];
                b = bHistogramData[i];
            }
        }

        //Publics
        public Color GetPixelColor(int x, int y)
        {
            var pos = GetPositionOfPixel(x, y);
            return Color.FromArgb(data[pos + AlphaOffset], data[pos + RedOffset], data[pos + GreenOffset], data[pos + BlueOffset]);
        }

        public void SetPixelColor(int x, int y, Color c)
        {
            SetPixelColor(x, y, c.A, c.R, c.G, c.B);
        }

        public void SetPixelColor(int x, int y, byte a, byte r, byte g, byte b)
        {
            int pos = GetPositionOfPixel(x, y);
            data[pos + AlphaOffset] = a;
            data[pos + RedOffset] = r;
            data[pos + GreenOffset] = g;
            data[pos + BlueOffset] = b;
        }

        public BitmapSource CreateBitmapSourceHandler()
        {
            return BitmapSource.Create(PixelWidth, PixelHeight, DpiX, DpiY, PixelFormats.Bgra32, null, data, Stride);
        }

        #endregion
        public object Clone()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }

        private object syncPreparingFlag = new object();



        [NonSerialized]
        private Task histogramPreparingTask = null;
        [NonSerialized]
        private int[] horizontalProjectionData;
        [NonSerialized]
        private int[] verticalProjectionData;
        public Task PrepareHistogramData()
        {
            lock (syncPreparingFlag)
            {
                if (isHistogramPreparing) return histogramPreparingTask;

                isHistogramPreparing = true;
                histogramPreparingTask = new Task(() =>
                {
                    horizontalProjectionData = new int[PixelWidth];
                    verticalProjectionData = new int[PixelHeight];
                    for (int x = 0; x < PixelWidth; x++)
                    {
                        int sum = 0;
                        for (int y = 0; y < PixelHeight; y++)
                        {
                            int pos = GetPositionOfPixel(x, y);
                            sum += data[pos + RedOffset] == 0 ? 1 : 0;
                            verticalProjectionData[y] += data[pos + RedOffset] == 0 ? 1 : 0;
                            horizontalProjectionData[x] += data[pos + RedOffset] == 0 ? 1 : 0;
                            rHistogramData[data[pos + RedOffset]]++;
                            gHistogramData[data[pos + GreenOffset]]++;
                            bHistogramData[data[pos + BlueOffset]]++;
                        }
                    }
                }, TaskCreationOptions.LongRunning);
            }
            rHistogramData = new int[256];
            gHistogramData = new int[256];
            bHistogramData = new int[256];
            histogramPreparingTask.GetAwaiter().OnCompleted(() =>
            {
                byte rmin, gmin, bmin, rmax, gmax, bmax;
                rmin = gmin = bmin = 255;
                rmax = gmax = bmax = 0;

                HistogramDataR?.Clear();
                HistogramDataG?.Clear();
                HistogramDataB?.Clear();
                List<DataPoint> rpoints = new List<DataPoint>();
                List<DataPoint> gpoints = new List<DataPoint>();
                List<DataPoint> bpoints = new List<DataPoint>();
                for (int i = 0; i <= 255; i++)
                {
                    byte b = (byte)i;
                    if (rHistogramData[i] != 0)
                    {
                        rmin = rmin > b ? b : rmin;
                        rmax = rmax < b ? b : rmax;
                    }
                    if (gHistogramData[i] != 0)
                    {
                        gmin = gmin > b ? b : gmin;
                        gmax = gmax < b ? b : gmax;
                    }
                    if (bHistogramData[i] != 0)
                    {
                        bmin = bmin > b ? b : bmin;
                        bmax = bmax < b ? b : bmax;
                    }

                    rpoints.Add(new DataPoint(i, rHistogramData[i]));
                    gpoints.Add(new DataPoint(i, gHistogramData[i]));
                    bpoints.Add(new DataPoint(i, bHistogramData[i]));
                }

                HistogramDataR = rpoints;
                HistogramDataG = gpoints;
                HistogramDataB = bpoints;
                HorizontalProjectionData = horizontalProjectionData.Select((e, i) => new DataPoint(i, e)).ToList();
                VerticalProjectionData = verticalProjectionData.Reverse().Select((e, i) => new DataPoint(e, i)).ToList();
                lock (syncPreparingFlag)
                {
                    Imin = Color.FromRgb(rmin, gmin, bmin);
                    Imax = Color.FromRgb(rmax, gmax, bmax);
                    isHistogramPreparing = false;
                }
            });
            histogramPreparingTask.Start();
            return histogramPreparingTask;
        }
    }
}
