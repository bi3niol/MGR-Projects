using PropertyChanged;
using System.ComponentModel;


namespace ProjectLAB.UserControls.ViewModels.Base
{
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
    }
}
