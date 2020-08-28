using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class AddMusic
    {
      public   List<System.Media.SoundPlayer> soundlist = new List<System.Media.SoundPlayer>();

        public AddMusic(List<System.Media.SoundPlayer> soundlist)
        {
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.sound1)); //0
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.gameover)); //1
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.sound2)); //2
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.sound3)); //3
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.Click)); //4
            soundlist.Add(new System.Media.SoundPlayer(Properties.Resources.invade)); //5
        }

    }
}
