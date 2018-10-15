using ProjectLAB.UserControls.ImageOperations;
using ProjectLAB.UserControls.ImageOperations.Functions;
using ProjectLAB.UserControls.Menagers;
using ProjectLAB.UserControls.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectLAB.UserControls.ViewModels
{
    public class ImageProcessorViewModel : Base.BaseViewModel
    {
        #region Fields
        private ProcessorManager manager = new ProcessorManager();
        public readonly List<IImageOperation> Operations = new List<IImageOperation>()
        {
            new ManualFunction(new byte[256]),
            new ErrorDiffusionDitheringImageOperation(),
            new ToGrayImageOperation(),
            new ToGrayAverageImageOperation(),
            new ArrayFilter(ArrayFilter.EDGE_DETECT_LAPLACE_FILTER),
            new ArrayFilter(ArrayFilter.EDGE_DETECT_DIAGONAL_FILTER),
            new ArrayFilter(ArrayFilter.EDGE_DETECT_HORIZONTAL_FILTER),
            new ArrayFilter(ArrayFilter.EDGE_DETECT_VERTICAL_FILTER),

            new ArrayFilter(ArrayFilter.SCULPTURE_EAST_FILTER),
            new ArrayFilter(ArrayFilter.SCULPTURE_SOUTH_EAST_FILTER),

            new ArrayFilter(ArrayFilter.UPPERPROOFFILTER_MEAN_REMOVAL),
            new ArrayFilter(ArrayFilter.LOWERPROOFFILTER_BLUR_N),
            new ArrayFilter(ArrayFilter.LOWERPROOFFILTER_GAUSS),


        };

        public IImageOperation CurrentOperation { get; private set; }
        public bool IsEnabled { get; set; } = true;
        #endregion

        #region Commands
        public ICommand LoadImageCommand { get; set; }
        public ICommand ToGrayAverageCommand { get; set; }
        public ICommand ToGrayWithEyeAdaptationCommand { get; set; }
        public ICommand BinarizeCommand { get; set; }
        public ICommand ShowOrginalImageCommand { get; set; }

        public ICommand EdgeDetectLaplaceCommand { get; set; }
        public ICommand EdgeDetectDiagonalCommand { get; set; }
        public ICommand EdgeDetectHorizontalCommand { get; set; }
        public ICommand EdgeDetectVerticalCommand { get; set; }

        public ICommand SculptureEastCommand { get; set; }
        public ICommand SculptureSouthEastCommand { get; set; }

        public ICommand UpperProofFilterCommand { get; set; }
        public ICommand LowerProofFilterBlurNCommand { get; set; }
        public ICommand LowerProofFilterGaussCommand { get; set; }


        #endregion

        public ImageProcessorViewModel()
        {
            LoadImageCommand = new RelayCommand(LoadNewImage);
            BinarizeCommand = new RelayCommand(TrigerImageOperation(Operations[1]));
            ToGrayWithEyeAdaptationCommand = new RelayCommand(TrigerImageOperation(Operations[2]));
            ToGrayAverageCommand = new RelayCommand(TrigerImageOperation(Operations[3]));
            EdgeDetectLaplaceCommand = new RelayCommand(TrigerImageOperation(Operations[4]));
            EdgeDetectDiagonalCommand = new RelayCommand(TrigerImageOperation(Operations[5]));
            EdgeDetectHorizontalCommand = new RelayCommand(TrigerImageOperation(Operations[6]));
            EdgeDetectVerticalCommand = new RelayCommand(TrigerImageOperation(Operations[7]));

            SculptureEastCommand = new RelayCommand(TrigerImageOperation(Operations[8]));
            SculptureSouthEastCommand = new RelayCommand(TrigerImageOperation(Operations[9]));

            UpperProofFilterCommand = new RelayCommand(TrigerImageOperation(Operations[10]));
            LowerProofFilterBlurNCommand = new RelayCommand(TrigerImageOperation(Operations[11]));
            LowerProofFilterGaussCommand = new RelayCommand(TrigerImageOperation(Operations[12]));


            ShowOrginalImageCommand = new RelayCommand((o=>manager.Reset()));
        }

        public void SetTargetImage(Image targetImage)
        {
            manager.TargetImage = targetImage;
        }
        public Image GetTargetImage()
        {
            return manager.TargetImage;
        }

        #region Command methods
        private void LoadNewImage(object parameters)
        {
            IsEnabled = false;

            string file = ImageHelper.SelectPicture();
            if (!string.IsNullOrEmpty(file))
                manager.LoadPicture(file);

            IsEnabled = true;
        }

        private Action<object> TrigerImageOperation(ImageOperations.IImageOperation operation)
        {
            return async (data) =>
            {
                IsEnabled = false;
                CurrentOperation = operation;
                await manager.FireOperation(operation);
                manager.Apply();
                IsEnabled = true;
            };
        }
        #endregion
    }
}
