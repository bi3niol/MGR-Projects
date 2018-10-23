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
            if(img.Format!=PixelFormats.Bgra32)
                img = new FormatConvertedBitmap(img, PixelFormats.Bgra32, null, 100);
            byte[] pixels = new byte[4 * img.PixelHeight * img.PixelWidth];
            img.CopyPixels(pixels, (int)(4 * img.PixelWidth), 0);
            return new PictureHandler(pixels, img.PixelWidth, img.PixelHeight, img.DpiX, img.DpiY);
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
