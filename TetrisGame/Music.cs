using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
   public class Music
    {

        public  Music(int index,List<System.Media.SoundPlayer> soundlist)
        {
            soundlist[index].Play();
        }

    }
}
