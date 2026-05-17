using System.ComponentModel;
using System.Windows;
using WebMojiCore;

namespace WebMojiDesktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CameraView.GestureDetected += gesture =>
            {
                Dispatcher.Invoke(() => ImageView.ShowGesture(gesture));
            };
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            CameraView.Stop();
        }
    }
}