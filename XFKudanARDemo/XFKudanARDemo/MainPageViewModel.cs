using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFKudanARDemo
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IKudanARService _kudanARService;

        public ICommand StartArCommand => _startArCommand ??= new Command(async () =>
            await _kudanARService.StartMarkerARActivityAsync());
        private ICommand _startArCommand;

        public ImageSource MarkerImage { get; }

        public MainPageViewModel()
        {
            _kudanARService = DependencyService.Get<IKudanARService>();
            MarkerImage = _kudanARService.GetMarkerImageSource();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //    => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
