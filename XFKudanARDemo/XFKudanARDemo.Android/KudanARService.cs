using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFKudanARDemo.Droid;

[assembly: Dependency(typeof(KudanARService))]
namespace XFKudanARDemo.Droid
{
    public class KudanARService : IKudanARService
    {
        private static readonly IReadOnlyList<Permissions.BasePermission> _kudanARPermissions = new Permissions.BasePermission[]
        {
            new Permissions.Camera(),
            new Permissions.StorageWrite(),
            new Permissions.StorageRead(),
        };

        public ImageSource GetMarkerImageSource()
        {
            var assets = MainActivity.Instance.Assets;

            // Stream の Dispose してない。 (Stream)ImageSource がやってくれる雰囲気がある。
            return ImageSource.FromStream(() => assets.Open(MarkerARActivity.MarkerAssetsName));
        }

        public async Task StartMarkerARActivityAsync()
        {
            var isGranted = await Smapho.CheckAndRequestPermissionsAsync(_kudanARPermissions);
            if (!isGranted)
                return;

            MainActivity.Instance.StartActivity<MarkerARActivity>();
        }
    }
}
