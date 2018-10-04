using ProjectLAB.UserControls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLAB.UserControls.ImageOperations
{
    public interface IImageOperation
    {
        string Name { get; }
        void ProcessImage(PictureHandler sourceImage, PictureHandler targetImage);
    }
}
