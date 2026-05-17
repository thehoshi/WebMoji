using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Windows.Controls;
using System.Windows.Threading;
using WebMojiCore;
using WebMojiVision;

namespace WebMojiDesktop.UserControl
{
    public partial class CameraView : System.Windows.Controls.UserControl
    {
        private readonly CameraService camera = new();
        private readonly HandDetector detector = new();

        public event Action<GestureType>? GestureDetected;

        public CameraView()
        {
            InitializeComponent();
            camera.FrameReady += OnFrameReady;
            camera.Start();
        }

        private void OnFrameReady(Mat frame)
        {
            var result = detector.Detect(frame);

            var bitmap = BitmapSourceConverter.ToBitmapSource(frame);
            bitmap.Freeze();

            Dispatcher.Invoke(() =>
            {
                CameraImage.Source = bitmap;
                DebugText.Text = result.Gesture.ToString();

                if (result.Gesture != GestureType.None)
                    GestureDetected?.Invoke(result.Gesture);
            });
        }

        public void Stop() => camera.Stop();
    }
}