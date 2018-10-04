using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class ToGrayImageOperation : IImageOperation
    {
        public string Name => "ToGrayImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    Color c = sourceImage[x, y];
                    byte color = (byte)((3 * c.R + 5 * c.G + c.B) / 9);
                    targetImage[x, y] = Color.FromArgb(255, color, color, color);
                }
        }
    }
}
