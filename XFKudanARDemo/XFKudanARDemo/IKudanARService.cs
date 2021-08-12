using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFKudanARDemo
{
    public interface IKudanARService
    {
        ImageSource GetMarkerImageSource();

        Task StartMarkerARActivityAsync();
    }
}
