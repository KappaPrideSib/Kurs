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
    public partial class TownPanel : GamePanel
    {
        public TownPanel() : base()//Базовые настройки при создании панели
        {
            type = "town";
            Content = "";
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/town.jpg"));
            Background = brush;
        }
    }
}
