using ProjectLAB.UserControls.ImageOperations;
using ProjectLAB.UserControls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectLAB.UserControls.Menagers
{
    public class ProcessorManager
    {
        private PictureHandler SourcePicture;
        private PictureHandler TargetPicture;

        private IImageOperation CurrentOperation;
        public readonly List<IImageOperation> Operations = new List<IImageOperation>()
        {
            new ErrorDiffusionDitheringImageOperation(),
            new ToGrayImageOperation()
        };

        public Image TargetImage { get; set; }

        public ProcessorManager()
        {
            CurrentOperation = Operations[0];
        }

        public bool LoadPicture(string path)
        {
            SourcePicture = ImageHelper.GetPictureHandler(path);
            TargetPicture = SourcePicture?.Clone() as PictureHandler;
            var res = SourcePicture != null;
            Apply(res);
            return res;
        }

        public bool FireCurrentOperation()
        {
            CurrentOperation?.ProcessImage(SourcePicture, TargetPicture);
            if (CurrentOperation != null)
                Apply();
            return CurrentOperation != null;
        }

        private void Apply(bool loaded)
        {
            if (loaded)
                Apply();
        }

        public void Apply()
        {
            if (TargetImage == null) return;

            TargetImage.Source = TargetPicture?.CreateBitmapSourceHandler();
        }
    }
}
