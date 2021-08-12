using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFKudanARDemo
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand StartArCommand => _startArCommand ??= new Command(async () =>
            await DependencyService.Get<IKudanARService>().StartMarkerARActivityAsync());
        private ICommand _startArCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //    => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
