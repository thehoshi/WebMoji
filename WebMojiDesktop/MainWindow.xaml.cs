using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.ComponentModel;
using System.Windows;
using WebMojiCore;
using WebMojiVision;

namespace WebMojiDesktop
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly CameraService camera = new();
        private readonly HandDetector detector = new();

        public MainWindow()
        {
            InitializeComponent();
            camera.FrameReady += OnFrameReady;
            camera.Start();
        }

        private void OnFrameReady(Mat frame)
        {
            // детектируем жест
            var result = detector.Detect(frame);

            // конвертируем и показываем кадр с нарисованным контуром
            var bitmap = BitmapSourceConverter.ToBitmapSource(frame);
            bitmap.Freeze();

            Dispatcher.Invoke(() =>
            {
                CameraImage.Source = bitmap;

                // пока просто выводим жест в заголовок окна
                Title = $"WebMoji — {result.Gesture}";
            });
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            camera.Stop();
        }
    }
}