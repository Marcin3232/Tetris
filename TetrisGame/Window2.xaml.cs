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
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        List<System.Media.SoundPlayer> soundlist = new List<System.Media.SoundPlayer>();

        public Window2()
        {
            InitializeComponent();
            AddMusic dodaj = new AddMusic(soundlist);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Music click = new Music(4, soundlist);
            new Window1().Show();
            this.Close();
        }

    }
}
