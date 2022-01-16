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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class BoardGeneration
    {
        static public GamePanel[,] GenerateBoard() // генерация массива игровых полей
        {
            GamePanel[,] board = new GamePanel[10, 10]; // сам массив с полями
            for (int i = 1; i < 101; i++) // наполнение нашего массива полями
            {
                GamePanel btt = GamePanel.PanelGen(i);
                btt.type = "def";
                board[btt.xpos - 1, btt.ypos - 1] = btt;
            }
            return board; // возвращение функцией массива
        }

        static public void WrapBoard(WrapPanel pan, GamePanel[,] board, GamePanel selected) // метод для отрисовки игрового поля
        {
            pan.Children.Clear(); // очистка поля после предыдущего заполнения
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[j, i].Background = Brushes.WhiteSmoke;
                    if (board[j, i].type == "town")
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/town.jpg"));
                        board[j, i].Background = brush;
                    }
                    if (board[j, i].type == "def")
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/def.jpg"));
                        board[j, i].Background = brush;
                    }

                    if (board[j, i].type == "war")
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/war.jpg"));
                        board[j, i].Background = brush;
                    }
                    pan.Children.Add(board[j, i]);//добавление полей на wrappanel
                }
            }
        }



    }
}
