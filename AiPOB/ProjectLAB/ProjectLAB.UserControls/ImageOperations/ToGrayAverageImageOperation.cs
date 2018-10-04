using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class ToGrayAverageImageOperation : IImageOperation
    {
        public string Name => "ToGrayAverageImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    Color c = sourceImage[x, y];
                    byte color = (byte)((c.R + c.G + c.B) /3);
                    targetImage[x, y] = Color.FromArgb(255, color, color, color);
                }
        }
    }
}
