using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class Warrior : GamePanel
    {
        public int moveroll = 1;
        public Warrior() : base()
        {
            type = "war";
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/war.jpg"));
            Background = brush;
        } 
    }
}
