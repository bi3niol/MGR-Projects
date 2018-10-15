using ProjectLAB.UserControls.ImageOperations;
using ProjectLAB.UserControls.ImageOperations.Functions;
using ProjectLAB.UserControls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectLAB.UserControls.Menagers
{
    public class ProcessorManager
    {
        private PictureHandler SourcePicture;
        private PictureHandler TargetPicture;
        private WriteableBitmap bmp;

        public Image TargetImage
        {
            get;
            set;
        }

        public bool LoadPicture(string path)
        {
            SourcePicture = ImageHelper.GetPictureHandler(path);
            var res = SourcePicture != null;
            Apply(res);
            return res;
        }

        public Task FireOperation(IImageOperation operation)
        {
            return Task.Factory.StartNew(() =>
            {
                operation?.ProcessImage(SourcePicture, TargetPicture);
            }, TaskCreationOptions.LongRunning);
        }

        private void Apply(bool loaded)
        {
            if (loaded)
            {
                TargetPicture = SourcePicture?.Clone() as PictureHandler;
                bmp = new WriteableBitmap(TargetPicture?.CreateBitmapSourceHandler());
                TargetImage.Source = bmp;
                Apply();
            }
        }

        public void Reset()
        {   
            Apply(true);
        }

        public void Apply()
        {
            if (TargetImage == null) return;
            bmp.WritePixels(new System.Windows.Int32Rect(0, 0, TargetPicture.PixelWidth, TargetPicture.PixelHeight), TargetPicture.data, TargetPicture.Stride, 0);
        }
    }
}
