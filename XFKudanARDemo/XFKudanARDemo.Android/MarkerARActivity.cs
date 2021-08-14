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
        // 2021年 12月 31日まで有効
        // from https://www.xlsoft.com/jp/products/kudan/download.html
        private const string _licenseKey = @"WBDmRLHaAWDgkMp5ppfqCSwZVHUhW5xwWOzlQGdrJc/Cq8fBz9SPo8ZIYU06y11iCwZHux0DGYKQTieMwqd+CkVNm0SqU01K8aUhJRhIOwqmdr8S3+9sh2+++eCGfAYvdjY2E73nRF+iuqMO0gTuLYZkke46OSEPu/J7uoq7iYkElW8QzWiEDLlUV0WPjS6DGExYakp5MoVM+LSIgjzxPYuu0BemqXdMnJyR/WZgFdmpTflEFdjnnZI66/7ZArFoUi9Krrp4kCPdjypaPbOTqYWdnajxGFTWN54Bp2fXIv8kpd5QLzzubtnpNK078WtE84+5q1+nS0reH3rEaVFM4XpIP0ug25iofJb4G9JZXKZ8PlGI8+zVuZwI3JNkJsbqpuVpH3o6s0dwjjGOgc/A3KVMMoOAWnizxZNbVApYcmYp+sGj26aoNqvCAeNlqPWAnk2wi1300ce01cj7JlzQuL8ANCzpRzv3BENINKQUtnlxaHbeIc4nyGscbIcy0ts8+t+ObJTA1TV3LtYMONtxzPHD88+y3Xv056k1mbPCSJeISpC+uEuYz/BO+ePEqV+PxW3m69blwhttc7FGilpQpeIo5M6R0MlyL4mlLvx74vh0+YMt8L0ykCNq8SzeLPIlTgQQYvPkrgb1HF44Ocfox4yUjpZWJWliEovJd6DJlWs=";

        public static string MarkerAssetsName => "KudanMarker.jpg";
        private const string _nodeAssetsName = "KudanNode.png";

        private ARImageTrackable _imageTrackable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ARAPIKey.Instance.SetAPIKey(_licenseKey);
        }

        public override void Setup()
        {
            base.Setup();

            // Create our trackable with an image
            var trackable = CreateTrackable("MyMarker", MarkerAssetsName);

            // Get the trackable Manager singleton
            var trackableManager = ARImageTracker.Instance;
            trackableManager.Initialise();

            // Add image trackable to the image tracker manager
            trackableManager.AddTrackable(trackable);

            // Create an image node using an image of the kudan cow
            var node = new ARImageNode(_nodeAssetsName);

            var textureMaterial = (ARTextureMaterial)node.Material;
            var scale = trackable.Width / textureMaterial.Texture.Width;
            node.ScaleByUniform(scale);

            // Add the image node as a child of the trackable's world
            trackable.World.AddChild(node);

            // Add listener methods that are defined in the ARImageTrackableListener interface
            trackable.AddListener(this);

            _imageTrackable = trackable;
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

        public new void Dispose()
        {
            _imageTrackable?.Dispose();
            base.Dispose();
        }
    }
}
