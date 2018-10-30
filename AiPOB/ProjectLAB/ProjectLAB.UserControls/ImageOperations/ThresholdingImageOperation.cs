using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLAB.UserControls.Extensions;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class ThresholdingImageOperation : IImageOperation
    {
        public string Name => "ThresholdingImageOperation";
        public int Threshold { get; set; }
        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                    targetImage[x, y] = sourceImage[x, y].BlacOrWite(Threshold);
        }
    }
}
