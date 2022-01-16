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
    public partial class GamePanel : Button
    {
        public int xpos = 0;
        public int ypos = 0;
        public bool player1control = false;
        public bool freepan = true;
        public bool player2control = false;
        public string type = "void";
        public static GamePanel selected { get; set; }

        public GamePanel() : base()
        {
            type = "def";
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/def.jpg"));
            Width = 80;
            Height = 80;
            Name = "Panel" + xpos + ypos;
            Click += GamePanel_Click;
            BorderThickness = new Thickness(0.5,0.5,0.5,0.5);
        }

        static public GamePanel PanelGen(int i)
        {
            GamePanel btt = new GamePanel();
            btt.Tag = i;
            if (i % 10 != 0) btt.xpos = i % 10;
            else btt.xpos = 10;
            if (i % 10 != 0) btt.ypos = i / 10 + 1;
            else btt.ypos = i / 10;
            //btt.Content = btt.xpos + "-" + btt.ypos;
            return btt;
        }

        private void GamePanel_Click(object sender, RoutedEventArgs e)
        {
            selected = null;
            selected = (GamePanel)sender;
            //if (( selected.type=="town")&&(selected.player1control==true))
            //MessageBox.Show((TownPanel)selected.);//Вывод окна статистики при клике
            if (selected.BorderThickness == new Thickness(2,2,2,2))
            {
                if (MainWindow.playerroll == 0)
                {
                    MainWindow.player1ctrlled.Remove(selected);
                    ResourcesPlayer.player1z += 250;
                }
                if (MainWindow.playerroll == 1)
                {
                    MainWindow.player2ctrlled.Remove(selected);
                    ResourcesPlayer.player2z += 250;
                }
                
                MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                
            }
            if (selected.Background == System.Windows.Media.Brushes.Green)
            {
                if (MainWindow.playerroll == 0)
                {
                    if (ResourcesPlayer.player1z >= 500)
                    {
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new TownPanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        activate(MainWindow.board);
                        MainWindow.player1ctrlled.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 1]);
                        ResourcesPlayer.player1z -= 500;
                        
                    }
                    else
                    {
                        MessageBox.Show("Не хватает золота!");
                        cancelbuild();
                    }
                }
                if (MainWindow.playerroll == 1)
                {
                    if (ResourcesPlayer.player2z >= 500)
                    {
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new TownPanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        activate(MainWindow.board);
                        MainWindow.player2ctrlled.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 1]);
                        ResourcesPlayer.player2z -= 500;
                    }
                    else
                    {
                        MessageBox.Show("Не хватает золота!");
                        cancelbuild();
                    }
                }
            }

            if (selected.Background == System.Windows.Media.Brushes.Blue)
            {
                if (MainWindow.playerroll == 0)
                {
                    if (ResourcesPlayer.player1z >= 100)
                    {
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        activate(MainWindow.board);
                        MainWindow.player1ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 1]);
                        ResourcesPlayer.player1z -= 100;

                    }
                    else
                    {
                        MessageBox.Show("Не хватает золота!");
                        cancelbuild();
                    }
                }
                if (MainWindow.playerroll == 1)
                {
                    if (ResourcesPlayer.player2z >= 100)
                    {
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        activate(MainWindow.board);
                        MainWindow.player2ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 1]);
                        ResourcesPlayer.player2z -= 100;
                    }
                    else
                    {
                        MessageBox.Show("Не хватает золота!");
                        cancelbuild();
                    }
                }
            }
            BoardGeneration.WrapBoard(MainWindow.wrap, MainWindow.board, selected);
        }

        public static void activate(GamePanel[,] board)
        {
            foreach (GamePanel item in board)
                item.IsEnabled = true;
        }
        public static void deactivate(GamePanel[,] board)
        {
            foreach (GamePanel item in board)
                item.IsEnabled = false;
        }

        public static List<GamePanel> aroundtown (GamePanel sel)
        {
            List<GamePanel> around = new List<GamePanel>();
            foreach (GamePanel item in MainWindow.board)
            {
                if (((Math.Abs(sel.xpos - item.xpos) == 2) && (Math.Abs(sel.ypos - item.ypos) == 2)) && (item.type!="town"))
                    around.Add(item);
            }
            return around;
        }

        public static List<GamePanel> aroundwar(GamePanel sel)
        {
            List<GamePanel> around = new List<GamePanel>();
            foreach (GamePanel item in MainWindow.board)
            {
                if ((((Math.Abs(sel.xpos - item.xpos) == 1) && (Math.Abs(sel.ypos - item.ypos) == 0)) || ((Math.Abs(sel.xpos - item.xpos) == 1) && (Math.Abs(sel.ypos - item.ypos) == 1)))|| ((Math.Abs(sel.xpos - item.xpos) == 0) && (Math.Abs(sel.ypos - item.ypos) == 1)))
                    around.Add(item);
            }
            return around;
        }

        public static void cancelbuild()
        {
            List<GamePanel> around = new List<GamePanel>();
            GamePanel.activate(MainWindow.board);
            foreach (GamePanel pan in MainWindow.board)
                pan.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
            if (MainWindow.playerroll == 0)
            {
                foreach (GamePanel pan in MainWindow.player1ctrlled)
                {
                    if (pan.type == "town")
                    {
                        around = GamePanel.aroundtown(pan);
                        foreach (GamePanel item in around)
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/def.jpg"));
                            item.Background = brush;
                        }
                    }

                }
            }

            if (MainWindow.playerroll == 1)
            {
                foreach (GamePanel pan in MainWindow.player2ctrlled)
                {
                    if (pan.type == "town")
                    {
                        around = GamePanel.aroundtown(pan);
                        foreach (GamePanel item in around)
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/def.jpg"));
                            item.Background = brush;
                        }
                    }

                }
            }
        }

        public static void destroypanel()
        {
            if (MainWindow.playerroll == 0)
            {
                deactivate(MainWindow.board);
                foreach(TownPanel pan in MainWindow.player1ctrlled )
                {
                    pan.IsEnabled = true;
                    pan.BorderThickness = new Thickness(2, 2, 2, 2);
                }
            }
            if (MainWindow.playerroll == 1)
            {
                deactivate(MainWindow.board);
                foreach (TownPanel pan in MainWindow.player2ctrlled)
                {
                    pan.IsEnabled = true;
                    pan.BorderThickness = new Thickness(2, 2, 2, 2);
                }
            }
        }

        public static void MoveUp()
        {
          if (selected.type=="war")
            {
                if (MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos > 1)
                {
                    if ((MainWindow.player1ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 0))
                    {
                        MainWindow.player1ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos = selected.ypos - 1;
                        MainWindow.player1ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 2]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 1, selected.ypos - 2];

                    }

                    if ((MainWindow.player2ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 1))
                    {
                        MainWindow.player2ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos = selected.ypos - 1;
                        MainWindow.player2ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos - 2]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos - 2].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 1, selected.ypos - 2];
                    }
                    BoardGeneration.WrapBoard(MainWindow.wrap, MainWindow.board, selected);
                }
            }
        }

        public static void MoveDown()
        {
            if (selected.type == "war")
            {
                if (MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos < 10)
                {
                    if ((MainWindow.player1ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 0))
                    {
                        MainWindow.player1ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 1, selected.ypos] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos].ypos = selected.ypos + 1;
                        MainWindow.player1ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 1, selected.ypos];
                    }

                    if ((MainWindow.player2ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 1))
                    {
                        MainWindow.player2ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 1, selected.ypos] = new Warrior();
                        MainWindow.board[selected.xpos - 1, selected.ypos].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos].ypos = selected.ypos + 1;
                        MainWindow.player2ctrlledwar.Add(MainWindow.board[selected.xpos - 1, selected.ypos]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 1, selected.ypos].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos - 1, selected.ypos].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 1, selected.ypos];
                    }
                    BoardGeneration.WrapBoard(MainWindow.wrap, MainWindow.board, selected);
                }
            }
        }

        public static void MoveLeft()
        {
            if (selected.type == "war")
            {
                if (MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos > 1)
                {
                    if ((MainWindow.player1ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 0))
                    {
                        MainWindow.player1ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos = selected.xpos - 1;
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos = selected.ypos;
                        MainWindow.player1ctrlledwar.Add(MainWindow.board[selected.xpos - 2, selected.ypos - 1]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 2, selected.ypos - 1];
                    }

                    if ((MainWindow.player2ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 1))
                    {
                        MainWindow.player2ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos = selected.xpos - 1;
                        MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos = selected.ypos;
                        MainWindow.player2ctrlledwar.Add(MainWindow.board[selected.xpos - 2, selected.ypos - 1]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos - 2, selected.ypos - 1].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos - 2, selected.ypos - 1];
                    }
                    BoardGeneration.WrapBoard(MainWindow.wrap, MainWindow.board, selected);
                }
            }
        }

        public static void MoveRight()
        {
            if (selected.type == "war")
            {
                if (MainWindow.board[selected.xpos-1, selected.ypos - 1].xpos<10)
                {
                    if ((MainWindow.player1ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 0))
                    {
                        MainWindow.player1ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos, selected.ypos - 1].xpos = selected.xpos + 1;
                        MainWindow.board[selected.xpos, selected.ypos - 1].ypos = selected.ypos;
                        MainWindow.player1ctrlledwar.Add(MainWindow.board[selected.xpos, selected.ypos - 1]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos, selected.ypos - 1].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos, selected.ypos - 1].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos, selected.ypos - 1].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos, selected.ypos - 1].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos, selected.ypos - 1];
                    }

                    if ((MainWindow.player2ctrlledwar.Contains(selected)) && (MainWindow.playerroll == 1))
                    {
                        MainWindow.player2ctrlledwar.Remove(selected);
                        MainWindow.board[selected.xpos, selected.ypos - 1] = new Warrior();
                        MainWindow.board[selected.xpos, selected.ypos - 1].xpos = selected.xpos + 1;
                        MainWindow.board[selected.xpos, selected.ypos - 1].ypos = selected.ypos;
                        MainWindow.player2ctrlledwar.Add(MainWindow.board[selected.xpos, selected.ypos - 1]);
                        for (int i = 0; i < MainWindow.player1ctrlled.Count; i++)
                            if ((MainWindow.player1ctrlled[i].xpos == MainWindow.board[selected.xpos, selected.ypos - 1].xpos) && ((MainWindow.player1ctrlled[i].ypos == MainWindow.board[selected.xpos, selected.ypos - 1].ypos)))
                                MainWindow.player1ctrlled.RemoveAt(i);
                        for (int i = 0; i < MainWindow.player2ctrlled.Count; i++)
                            if ((MainWindow.player2ctrlled[i].xpos == MainWindow.board[selected.xpos, selected.ypos - 1].xpos) && ((MainWindow.player2ctrlled[i].ypos == MainWindow.board[selected.xpos, selected.ypos - 1].ypos)))
                                MainWindow.player2ctrlled.RemoveAt(i);
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1] = new GamePanel();
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].xpos = selected.xpos;
                        MainWindow.board[selected.xpos - 1, selected.ypos - 1].ypos = selected.ypos;
                        selected = MainWindow.board[selected.xpos, selected.ypos - 1];
                    }
                    BoardGeneration.WrapBoard(MainWindow.wrap, MainWindow.board, selected);
                }
            }
        }
    }
}
