using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using WebMojiCore;
using WebMojiDesktop.UserControl;

namespace WebMojiDesktop
{
    public partial class MainWindow : Window
    {
        private readonly GestureImageMap imageMap = new();

        public MainWindow()
        {
            InitializeComponent();

            imageMap.Set(GestureType.Fist, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\FistImage.jpg");
            imageMap.Set(GestureType.OneFinger, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\OneFingerImage.jpg");
            imageMap.Set(GestureType.Swag, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\SwagImage.jpg");

            CameraView.GestureDetected += OnGestureDetected;
        }

        private void OnGestureDetected(GestureType gesture)
        {
            var path = imageMap.Get(gesture);
            if (path == null) return;

            GestureImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            CameraView.Stop();
        }
    }
}