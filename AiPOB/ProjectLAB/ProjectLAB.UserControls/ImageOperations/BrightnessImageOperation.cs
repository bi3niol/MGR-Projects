using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class BrightnessImageOperation : IImageOperation
    {
        public int OffSet { get; set; }

        public string Name => "BrightnessImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    var c1 = sourceImage[x, y];
                    int cr = c1.R + OffSet;
                    int r = cr > 255 ? 255 : cr < 0 ? 0 : cr;
                    int cg = c1.G + OffSet;
                    int g = cg > 255 ? 255 : cg < 0 ? 0 : cg;
                    int cb = c1.B + OffSet;
                    int b = cb > 255 ? 255 : cb < 0 ? 0 : cb;
                    targetImage[x, y] = Color.FromRgb((byte)r, (byte)g, (byte)b);
                }
        }
    }
}
