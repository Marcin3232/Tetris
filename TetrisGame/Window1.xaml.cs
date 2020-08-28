using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TetrisGame
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

       List<System.Media.SoundPlayer> soundlist = new List<System.Media.SoundPlayer>();
        public Window1()
        {
            InitializeComponent();
            AddMusic dodaj = new AddMusic(soundlist);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Music click = new Music(4,soundlist);
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Music click = new Music(4, soundlist);
            new Window2().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Music click = new Music(4, soundlist);
            TimeSpan.FromMilliseconds(1000);
            this.Close();
        }

    }
}
