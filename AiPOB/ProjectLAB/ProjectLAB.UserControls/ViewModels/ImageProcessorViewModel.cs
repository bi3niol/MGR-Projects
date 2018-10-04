using ProjectLAB.UserControls.Menagers;
using ProjectLAB.UserControls.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectLAB.UserControls.ViewModels
{
    public class ImageProcessorViewModel : Base.BaseViewModel
    {
        #region Fields
        private ProcessorManager manager = new ProcessorManager();

        public string Name { get; set; } = "elo";
        public bool IsEnabled { get; set; } = true;
        #endregion

        #region Commands
        public ICommand LoadImageCommand { get; set; }
        public ICommand TrigerImageOperationCommand { get; set; }
        #endregion

        public ImageProcessorViewModel()
        {
            LoadImageCommand = new RelayCommand(LoadNewImage);
            TrigerImageOperationCommand = new RelayCommand(TrigerImageOperation);
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
        private void LoadNewImage(Object parameters)
        {
            IsEnabled = false;

            string file = ImageHelper.SelectPicture();
            if (!string.IsNullOrEmpty(file))
                manager.LoadPicture(file);

            IsEnabled = true;
        }

        private void TrigerImageOperation(object parameter)
        {
            IsEnabled = false;
            manager.FireCurrentOperation();
            IsEnabled = true;
        }
        #endregion
    }
}
