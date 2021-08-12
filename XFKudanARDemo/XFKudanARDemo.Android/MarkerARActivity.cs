using Android.App;
using Android.OS;
using EU.Kudan.Kudan;
using System;

// see also https://github.com/XLsoft-Corporation/Public-Samples-Android/blob/master/app/src/main/java/com/xlsoft/publicsamples/MarkerActivity.java
namespace XFKudanARDemo.Droid
{
    [Activity(Label = "MarkerARActivity")]
    public class MarkerARActivity : ARActivity, IARImageTrackableListener
    {
        // from https://www.xlsoft.com/doc/kudan/ja/development-license-keys_jp/
        private const string _licenseKey = @"License Key";

        private const string _markerAssetsName = "Kudan_Lego_Marker.jpg";
        private const string _nodeAssetsName = "Kudan_Cow.png";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ARAPIKey.Instance.SetAPIKey(_licenseKey);
        }

        public override void Setup()
        {
            base.Setup();

            // Create our trackable with an image
            var trackable = CreateTrackable("LegoMarker", _markerAssetsName);

            // Get the trackable Manager singleton
            var trackableManager = ARImageTracker.Instance;
            trackableManager.Initialise();

            // Add image trackable to the image tracker manager
            trackableManager.AddTrackable(trackable);

            // Create an image node using an image of the kudan cow
            var cow = new ARImageNode(_nodeAssetsName);

            var textureMaterial = (ARTextureMaterial)cow.Material;
            var scale = trackable.Width / textureMaterial.Texture.Width;
            cow.ScaleByUniform(scale);

            // Add the image node as a child of the trackable's world
            trackable.World.AddChild(cow);

            // Add listener methods that are defined in the ARImageTrackableListener interface
            trackable.AddListener(this);
        }

        private static ARImageTrackable CreateTrackable(string trackableName, string assetName)
        {
            // Create a new trackable instance with a name
            var trackable = new ARImageTrackable(trackableName);

            // Load the image for this marker
            trackable.LoadFromAsset(assetName);

            return trackable;
        }

        public void DidDetect(ARImageTrackable p0) => System.Diagnostics.Debug.WriteLine("Did Detect");
        public void DidLose(ARImageTrackable p0) => System.Diagnostics.Debug.WriteLine("Did Lose");
        public void DidTrack(ARImageTrackable p0) => System.Diagnostics.Debug.WriteLine("Did Track");
    }
}
