using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ResourcesPlayer
    {
        public static int player1z = 500;
        public static int player2z = 500;
        public static string player1nick = "";
        public static string player2nick = "";
        public static void Hod()
        {
            player1z = player1z + MainWindow.player1ctrlled.Count * 100 - MainWindow.player1ctrlledwar.Count * 50;
            player2z = player2z + MainWindow.player2ctrlled.Count * 100 - MainWindow.player2ctrlledwar.Count * 50;
        }
    }
}
