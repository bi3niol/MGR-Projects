using ProjectLAB.UserControls.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLAB.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImageProcessor : UserControl
    {
        private ImageProcessorViewModel viewModel = new ImageProcessorViewModel();
        public Image TargetImage
        {
            get
            {
                return viewModel.GetTargetImage();
            }
            set
            {
                if (viewModel.GetTargetImage() == value)
                    return;

                viewModel.SetTargetImage(value);
            }
        }
        public ImageProcessor()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
