using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ThatWasEasy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Drawing.Bitmap BitMask = new System.Drawing.Bitmap(Application.GetResourceStream(new Uri("pack://application:,,,/res/easy.mask.png")).Stream);
        private readonly BitmapImage ButtonDownImage = new BitmapImage(new Uri("pack://application:,,,/res/easy.down.png"));
        private readonly BitmapImage ButtonUpImage = new BitmapImage(new Uri("pack://application:,,,/res/easy.up.png"));
        private readonly SoundPlayer Player = new SoundPlayer(Application.GetResourceStream(new Uri("pack://application:,,,/res/easy.wav")).Stream);
        private readonly System.Drawing.Color WHITE = System.Drawing.Color.White;
        private readonly System.Drawing.Color BLACK = System.Drawing.Color.Black;
        private bool _isOnRedArea;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            var pt = e.GetPosition(this);
            _isOnRedArea = IsOnRedArea(pt);

            if (_isOnRedArea)
            {
                TheImage.Source = ButtonDownImage;
            }
            else
            {
                TheImage.Source = ButtonUpImage;
                DragMove();
            }
        }

        private void OnLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            if (_isOnRedArea)
            {
                TheImage.Source = ButtonUpImage;
                Player.Play();
            }
        }

        private bool IsOnRedArea(Point pt)
        {
            var pix = BitMask.GetPixel((int)pt.X, (int)pt.Y);

            if (pix.ToArgb() == WHITE.ToArgb())
                return true;

            return false;
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
