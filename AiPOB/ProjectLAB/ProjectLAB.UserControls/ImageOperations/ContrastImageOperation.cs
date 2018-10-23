using ProjectLAB.UserControls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class ContrastImageOperation : IImageOperation
    {
        public float OffSet { get; set; }

        public string Name => "ContrastImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    var c1 = sourceImage[x, y];

                    int cr = (int)(OffSet * (c1.R - 128) + 128);
                    int r = cr > 255 ? 255 : cr < 0 ? 0 : cr;
                    int cg = (int)(OffSet * (c1.G - 128) + 128);
                    int g = cg > 255 ? 255 : cg < 0 ? 0 : cg;
                    int cb = (int)(OffSet * (c1.B - 128) + 128);
                    int b = cb > 255 ? 255 : cb < 0 ? 0 : cb;

                    targetImage[x, y] = Color.FromRgb((byte)r, (byte)g, (byte)b);
                }
        }
    }
}
