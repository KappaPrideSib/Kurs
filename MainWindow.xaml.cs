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
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static GamePanel[,] board = new GamePanel[10, 10];
        public static WrapPanel wrap;
        public static int playerroll = 0;
        public static List<GamePanel> player1ctrlled = new List<GamePanel>();
        public static List<GamePanel> player2ctrlled = new List<GamePanel>();
        public static List<GamePanel> player1ctrlledwar = new List<GamePanel>();
        public static List<GamePanel> player2ctrlledwar = new List<GamePanel>();
        private MusicPlayer x = new MusicPlayer();
        public System.Windows.Threading.DispatcherTimer time;
        public int allseconds = 0;
        public MainWindow()
        {
            InitializeComponent();


            time = new System.Windows.Threading.DispatcherTimer();
            //назначение обработчика события Тик
            time.Tick += new EventHandler(dispatcherTimer_Tick);
            //установка интервала между тиками
            //TimeSpan – переменная для хранения времени в формате часы/минуты/секунды
            time.Interval = new TimeSpan(0, 0, 1);
            //запуск таймера
            time.Start();

            slvolume.Minimum = 0;
            slvolume.Maximum = 1;

            this.Background = new ImageBrush(new BitmapImage(new Uri("C:/Users/Рабочий/Downloads/KURSPOGI/Strat/Image/bg.png")));

            x.repeat();
            slvolume.Value = 0.25;

            lbnickfst.Content = "Золото " + ResourcesPlayer.player1nick;
            lbnickscnd.Content = "Золото " + ResourcesPlayer.player2nick;

            btbuild.Content = "Постройка города\n(-500 зол. / +100 зол. за ход)";
            btwarcreate.Content = "Создать воина\n(-100 зол. / -50 зол. за ход)";
            lbzol1pl.Content = ResourcesPlayer.player1z;
            lbzol2pl.Content = ResourcesPlayer.player2z;
            board = BoardGeneration.GenerateBoard();
            board[0, 0] = new TownPanel();
            board[0, 0].xpos = 1;
            board[0, 0].ypos = 1;
            player1ctrlled.Add(board[0, 0]);
            board[9, 9] = new TownPanel();
            board[9, 9].xpos = 10;
            board[9, 9].ypos = 10;
            player2ctrlled.Add(board[9, 9]);
            BoardGeneration.WrapBoard(WrapGamePanels, board, GamePanel.selected);
            wrap = WrapGamePanels;
    }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            allseconds++;
            lbtimer.Content = (allseconds / 3600) + " : " + ((allseconds % 3600) / 60) + " : " + allseconds % 60;
        }

        private void btbuild_Click(object sender, RoutedEventArgs e)
        {
            List<GamePanel> around = new List<GamePanel>();
            if (playerroll==0)
            {
                if (player1ctrlled.Count < 3)
                {
                    GamePanel.deactivate(board);
                    foreach (GamePanel pan in player1ctrlled)
                    {
                        if (pan.type == "town")
                        {
                            around = GamePanel.aroundtown(pan);
                            foreach (GamePanel item in around)
                            {
                                item.IsEnabled = true;
                                item.Background = Brushes.Green;
                            }
                        }

                    }
                }
            }

            if (playerroll == 1)
            {
                if (player2ctrlled.Count < 3)
                {
                    GamePanel.deactivate(board);
                    foreach (GamePanel pan in player2ctrlled)
                    {
                        if (pan.type == "town")
                        {
                            around = GamePanel.aroundtown(pan);
                            foreach (GamePanel item in around)
                            {
                                item.IsEnabled = true;
                                item.Background = Brushes.Green;
                            }
                        }

                    }
                }
            }
        }

        private void btroll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Warrior pan in player1ctrlledwar)
                pan.moveroll = 1;
            foreach (Warrior pan in player2ctrlledwar)
                pan.moveroll = 1;
            if (player1ctrlled.Count == 0)
            {
                MessageBox.Show("Игрок 2 выиграл!");
                x.stop();
                Environment.Exit(0);
            }
            if (player2ctrlled.Count == 0)
            {
                MessageBox.Show("Игрок 1 выиграл!");
                x.stop();
                Environment.Exit(0);
            }
            ResourcesPlayer.Hod();
            lbroll.Content = int.Parse(lbroll.Content.ToString())+1;
            lbzol1pl.Content = ResourcesPlayer.player1z;
            lbzol2pl.Content = ResourcesPlayer.player2z;
            playerroll = (playerroll + 1)%2;


        }

        private void btcancelbuild_Click(object sender, RoutedEventArgs e)
        {
            GamePanel.cancelbuild();

        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
            GamePanel.destroypanel();

        }

        private void btwarcreate_Click(object sender, RoutedEventArgs e)
        {
            List<GamePanel> around = new List<GamePanel>();
            if (playerroll == 0)
            {
                GamePanel.deactivate(board);
                foreach (GamePanel pan in player1ctrlled)
                {
                    if (pan.type == "town")
                    {
                        around = GamePanel.aroundwar(pan);
                        foreach (GamePanel item in around)
                        {
                            item.IsEnabled = true;
                            item.Background = Brushes.Blue;
                        }
                    }

                }
            }

            if (playerroll == 1)
            {
                GamePanel.deactivate(board);
                foreach (GamePanel pan in player2ctrlled)
                {
                    if (pan.type == "town")
                    {
                        around = GamePanel.aroundwar(pan);
                        foreach (GamePanel item in around)
                        {
                            item.IsEnabled = true;
                            item.Background = Brushes.Blue;
                        }
                    }

                }
            }
        }

        private void btwarup_Click(object sender, RoutedEventArgs e)
        {
            if (GamePanel.selected.type == "war")
            {
                if (((Warrior)GamePanel.selected).moveroll == 1)
                {
                    GamePanel.MoveUp();
                    ((Warrior)GamePanel.selected).moveroll = 0;
                }
            }
        }

        private void btwardown_Click(object sender, RoutedEventArgs e)
        {
            if (GamePanel.selected.type == "war")
            {
                if (((Warrior)GamePanel.selected).moveroll == 1)
                {
                    GamePanel.MoveDown();
                    ((Warrior)GamePanel.selected).moveroll = 0;
                }
            }
        }

        private void btwarleft_Click(object sender, RoutedEventArgs e)
        {
            if (GamePanel.selected.type == "war")
            {
                if (((Warrior)GamePanel.selected).moveroll == 1)
                {
                    GamePanel.MoveLeft();
                    ((Warrior)GamePanel.selected).moveroll = 0;
                }
            }
        }

        private void btwarright_Click(object sender, RoutedEventArgs e)
        {
            if (GamePanel.selected.type == "war")
            {
                if (((Warrior)GamePanel.selected).moveroll == 1)
                {
                    GamePanel.MoveRight();
                    ((Warrior)GamePanel.selected).moveroll = 0;
                }
            }
        }

        private void slvolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            x.Change(slvolume.Value);
        }
    }
}
