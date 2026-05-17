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
        }
    }
}