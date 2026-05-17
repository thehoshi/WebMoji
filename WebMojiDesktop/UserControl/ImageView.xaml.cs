using System;
using System.Windows.Media.Imaging;
using WebMojiCore;

namespace WebMojiDesktop.UserControl
{
    public partial class ImageView : System.Windows.Controls.UserControl
    {
        private readonly GestureImageMap imageMap = new();

        public ImageView()
        {
            InitializeComponent();

            imageMap.Set(GestureType.Fist, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\FistImage.jpg");
            imageMap.Set(GestureType.OneFinger, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\OneFingerImage.jpg");
            imageMap.Set(GestureType.Swag, "C:\\Users\\Admin\\source\\repos\\WebMoji\\WebMojiDesktop\\Image\\SwagImage.jpg");
        }

        public void ShowGesture(GestureType gesture)
        {
            var path = imageMap.Get(gesture);
            if (path == null) return;

            var fullPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, path);

            if (!System.IO.File.Exists(fullPath)) return;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullPath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            GestureImage.Source = bitmap;
        }
    }
}