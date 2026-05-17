using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using WebMojiCore;
using WebMojiDesktop.UserControl;


namespace WebMojiDesktop.UserControl
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : System.Windows.Controls.UserControl
    {
        private readonly CameraView cameraViewInstance;

        public ImageView()
        {
            InitializeComponent();

            imageMap.Set(GestureType.Fist, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\FistImage.jpg");
            imageMap.Set(GestureType.OneFinger, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\OneFingerImage.jpg");
            imageMap.Set(GestureType.Swag, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\SwagImage.jpg");

            cameraViewInstance = new CameraView();
            cameraViewInstance.GestureDetected += OnGestureDetected;
        }

        private void OnGestureDetected(GestureType gesture)
        {
            var path = imageMap.Get(gesture);
            if (path == null) return;

            GestureImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            cameraViewInstance.Stop();
        }

    }
}
