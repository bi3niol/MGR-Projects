using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Helpers;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class HistogramEqualizationImageOperation : IImageOperation
    {
        public string Name => "HistogramEqualizationImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            float pixelCount = sourceImage.PixelHeight * sourceImage.PixelWidth;
            float[] cdfR = new float[256];
            float[] cdfG = new float[256];
            float[] cdfB = new float[256];

            float r, g, b;

            sourceImage.GetHistogramDataFor(0, out r, out g, out b);
            cdfR[0] = r;
            cdfG[0] = g;
            cdfB[0] = b;

            for (int i = 1; i < 256; i++)
            {
                sourceImage.GetHistogramDataFor(i, out r, out g, out b);
                cdfR[i] = cdfR[i - 1] + r;
                cdfG[i] = cdfG[i - 1] + g;
                cdfB[i] = cdfB[i - 1] + b;
            }

            float denominatorR = pixelCount - cdfR[0];
            float denominatorG = pixelCount - cdfG[0];
            float denominatorB = pixelCount - cdfB[0];

            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    Color c = sourceImage[x, y];
                    int rr = (int)((cdfR[c.R] - cdfR[0]) / denominatorR * 255);
                    int gg = (int)((cdfG[c.G] - cdfR[0]) / denominatorG * 255);
                    int bb = (int)((cdfB[c.B] - cdfR[0]) / denominatorB * 255);
                    targetImage[x, y] = Color.FromRgb((byte)MathHelper.Limit(rr), (byte)MathHelper.Limit(gg), (byte)MathHelper.Limit(bb));
                }
        }
    }
}
