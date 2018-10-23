using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class NegationImageOperation : IImageOperation
    {
        public string Name => "NegationImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            byte b = 255;
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    Color c = sourceImage[x, y];
                    targetImage[x, y] = Color.FromRgb((byte)(b - c.R), (byte)(b - c.G), (byte)(b - c.B));
                }
        }
    }
}
