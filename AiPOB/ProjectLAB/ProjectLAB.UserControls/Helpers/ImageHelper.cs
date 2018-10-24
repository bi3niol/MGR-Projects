using ProjectLAB.UserControls.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectLAB.UserControls
{
    public static class ImageHelper
    {
        public static PictureHandler GetPictureHandler(string path)
        {
            if (!File.Exists(path))
                return null;
            BitmapSource img = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            return GetPictureHandler(img);
        }

        public static PictureHandler GetPictureHandler(BitmapSource source)
        {
            if (source == null)
                return null;

            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 100);
            byte[] pixels = new byte[4 * source.PixelHeight * source.PixelWidth];
            source.CopyPixels(pixels, (int)(4 * source.PixelWidth), 0);
            return new PictureHandler(pixels, source.PixelWidth, source.PixelHeight, source.DpiX, source.DpiY);
        }

        public static string SelectPicture()
        {
            var openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == true)
                return openFile.FileName;

            return null;
        }

        public static string SavePicture()
        {
            var saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.Filter = "All types (*.png)|*.png";
            saveFile.DefaultExt = "png";
            if (saveFile.ShowDialog() == true)
                return saveFile.FileName;
            return null;
        }
    }
}
