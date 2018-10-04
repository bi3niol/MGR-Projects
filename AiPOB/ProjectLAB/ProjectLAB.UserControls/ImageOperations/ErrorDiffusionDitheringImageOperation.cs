using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Extensions;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    partial class ErrorDiffusionDitheringImageOperation : IImageOperation
    {
        public string Name => "ErrorDiffusionDitheringImageOperation";
        private float[,] filter = new[,]
        {
            {0,0,0 },
            {0,0,7/16f },
            {3/16f, 5/16f,1/16 }
        };
        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            var source = sourceImage.Clone() as PictureHandler;
            Color eCol, bOw, c;
            byte e;
            float error;
            int i, j;
            for (int x = 0; x < source.PixelWidth; x++)
                for (int y = 0; y < source.PixelHeight; y++)
                {
                    c = source[x, y];
                    bOw = c.BlacOrWite();
                    targetImage[x, y] = bOw;
                    error = c.R - targetImage[x, y].R;
                    e = (byte)Math.Abs(error);
                    eCol = Color.FromRgb(e, e, e);
                    for (i = -1; i < 2; i++)
                        for (j = -1; j < 2; j++)
                        {
                            if (error > 0)
                                source[x + i, y + j] = Color.Add(source[x + i, y + j], Color.Multiply(eCol, filter[i + 1, j + 1]));
                            else
                                source[x + i, y + j] = Color.Subtract(source[x + i, y + j], Color.Multiply(eCol, filter[i + 1, j + 1]));
                        }
                }
        }
    }
}
