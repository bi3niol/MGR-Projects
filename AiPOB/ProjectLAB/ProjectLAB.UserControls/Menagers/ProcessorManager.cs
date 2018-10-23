using ProjectLAB.UserControls.ImageOperations;
using ProjectLAB.UserControls.ImageOperations.Functions;
using ProjectLAB.UserControls.Models;
using ProjectLAB.UserControls.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ImageProcessorViewModel processorViewModel;

        public Image TargetImage
        {
            get;
            set;
        }

        public ProcessorManager(ImageProcessorViewModel vm)
        {
            processorViewModel = vm;
        }

        public bool LoadPicture(string path)
        {
            SourcePicture = ImageHelper.GetPictureHandler(path);
            var res = SourcePicture != null;
            Apply(res);
            return res;
        }

        internal void SavePicture(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.OpenOrCreate))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(TargetImage.Source as BitmapSource));
                encoder.Save(fileStream);
            }
        }

        public Task FireOperation(IImageOperation operation)
        {
            return Task.Factory.StartNew(() =>
            {
                operation?.ProcessImage(TargetPicture.Clone() as PictureHandler, TargetPicture);
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

        public async void Apply()
        {
            if (TargetImage == null) return;
            bmp.WritePixels(new System.Windows.Int32Rect(0, 0, TargetPicture.PixelWidth, TargetPicture.PixelHeight), TargetPicture.data, TargetPicture.Stride, 0);
            await CalculateHistogram();
            ApplyHistogram();
        }

        public Task CalculateHistogram()
        {
            return TargetPicture.PrepareHistogramData();
        }

        public void ApplyHistogram()
        {
            processorViewModel.HistogramDataR = TargetPicture.HistogramDataR;
            processorViewModel.HistogramDataG = TargetPicture.HistogramDataG;
            processorViewModel.HistogramDataB = TargetPicture.HistogramDataB;

            processorViewModel.VerticalProjectionData = TargetPicture.VerticalProjectionData;
            processorViewModel.HorizontalProjectionData = TargetPicture.HorizontalProjectionData;
        }
    }
}
