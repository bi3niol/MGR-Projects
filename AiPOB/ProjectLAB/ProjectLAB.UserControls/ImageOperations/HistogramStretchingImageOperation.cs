using ProjectLAB.UserControls.Models;
using System.Windows.Media;

namespace ProjectLAB.UserControls.ImageOperations
{
    internal class HistogramStretchingImageOperation : IImageOperation
    {
        public string Name => "HistogramStretchingImageOperation";

        public void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage)
        {
            var minmax = sourceImage.Imax - sourceImage.Imin;
            var cr = (float)255 / minmax.R;
            var cg = (float)255 / minmax.G;
            var cb = (float)255 / minmax.B;
            for (int x = 0; x < sourceImage.PixelWidth; x++)
                for (int y = 0; y < sourceImage.PixelHeight; y++)
                {
                    var c = (sourceImage[x, y] - sourceImage.Imin);
                    targetImage[x, y] = Color.FromRgb((byte)(c.R * cr), (byte)(c.G * cg), (byte)(c.B * cb));
                }
        }
    }
}
