using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectLAB.UserControls.Models;

namespace ProjectLAB.UserControls.ImageOperations
{
    public class FunctionOperation : IImageOperation
    {
        public virtual string Name { get; }
        protected Func<Color, Color> TransformFunc { get; set; }

        public FunctionOperation(Func<Color, Color> transform = null)
        {
            TransformFunc = transform;
            if (TransformFunc == null)
                TransformFunc = (c) => c;
        }

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                    targetImage[x, y] = TransformFunc(sourceImage[x, y]);
        }
    }
}
