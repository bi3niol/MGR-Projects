using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectLAB.UserControls.Models
{
    public class PictureHandler : ICloneable
    {
        #region Fields
        public readonly int PixelWidth;
        public readonly int PixelHeight;
        public readonly double DpiX;
        public readonly double DpiY;

        public int Stride { get; private set; }
        private BitmapPalette bmPlatte;
        private PixelFormat pxFormat;

        private const int SpacePerPixel = 4;
        private const int RedOffset = 0;
        private const int GreenOffset = 1;
        private const int BlueOffset = 2;
        private const int AlphaOffset = 3;

        private bool isHistogramPreparing = false;
        private int[] rHistogramData;
        private int[] gHistogramData;
        private int[] bHistogramData;

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
            return new PictureHandler(data.Clone() as byte[], PixelWidth, PixelHeight, DpiX, DpiY);
        }

        private object syncPreparingFlag = new object();
        private Task histogramPreparingTask = null;
        public Task PrepareHistogramData()
        {
            lock (syncPreparingFlag)
            {
                if (isHistogramPreparing) return histogramPreparingTask;

                isHistogramPreparing = true;
                histogramPreparingTask = new Task(() =>
                {
                    for (int x = 0; x < PixelWidth; x++)
                        for (int y = 0; y < PixelHeight; y++)
                        {
                            int pos = GetPositionOfPixel(x, y);
                            rHistogramData[data[pos + RedOffset]]++;
                            gHistogramData[data[pos + GreenOffset]]++;
                            bHistogramData[data[pos + BlueOffset]]++;
                        }
                }, TaskCreationOptions.LongRunning);
            }
            rHistogramData = new int[256];
            gHistogramData = new int[256];
            gHistogramData = new int[256];
            histogramPreparingTask.GetAwaiter().OnCompleted(() =>
            {
                lock (syncPreparingFlag)
                {
                    isHistogramPreparing = false;
                }
            });
            histogramPreparingTask.Start();
            return histogramPreparingTask;
        }
    }
}
