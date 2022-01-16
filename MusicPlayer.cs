using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MusicPlayer
    {
        public MediaPlayer music = new MediaPlayer();

        public void repeat()
        {
            music.Open(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Sound/music.mp4", UriKind.Relative));
            music.MediaEnded += new EventHandler(Media_Ended);
            music.Play();
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }


        public void stop()
        {
            music.Stop();
        }

        public void Change(double Res)
        {
            music.Volume = Res;
        }
    }
}
