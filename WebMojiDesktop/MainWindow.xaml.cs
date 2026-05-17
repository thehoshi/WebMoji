using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.ComponentModel;
using System.Windows;
using WebMojiVision;

namespace WebMojiDesktop
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly CameraService camera = new();

        public MainWindow()
        {
            InitializeComponent();

            camera.FrameReady += OnFrameReady;
            camera.Start();
        }

        private void OnFrameReady(Mat frame)
        {
            var bitmap = BitmapSourceConverter.ToBitmapSource(frame);
            bitmap.Freeze();

            Dispatcher.Invoke(() =>
            {
                CameraImage.Source = bitmap;
            });
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            camera.Stop();
        }
    }
}