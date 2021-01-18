using Algoritmic.Controller;
using System;
using System.Collections;
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

namespace SnakeApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameField game;
        private System.Drawing.Color back;

        public MainWindow()
        {
            InitializeComponent();

            game = new GameField(0, 0);
            game.BackgroundChanged += Game_BackgroundChanged;
            game.FieldChanged += Game_FieldChanged;
            back = game.BackGround;
            Color c = Color.FromArgb(game.BackGround.A, game.BackGround.R, game.BackGround.G, game.BackGround.B);
            GameGrid.Background = new SolidColorBrush(c);
        }

        private void Game_FieldChanged(object sender, EventArgs e)
        {
            int[,] pole = ((GameField)sender).GetField();
            for(int i=0;i<pole.GetLength(0);i++)
                for(int j=0;j< pole.GetLength(1);j++)
                {
                    switch (pole[i, j])
                    {
                        case 1:
                            {
                                //сделать отдельную UI
                                Obstacle o = new Obstacle();
                                Grid.SetColumn(o, j);
                                Grid.SetRow(o, i);
                                GameGrid.Children.Add(o);
                                break;
                            }
                        case 0:
                            {
                                GameGrid.Children.Remove(
                                    new List<UIElement>(GameGrid.Children.OfType<UIElement>()).Find(el => Grid.GetColumn(el) == j && Grid.GetRow(el) == i));
                                break;
                            }
                        case -1:
                            break;
                        case -2:
                            break;
                        case -5:
                            break;
                        default:
                            {
                                Rectangle r = new Rectangle();
                                System.Drawing.Color c = System.Drawing.Color.FromArgb(pole[i, j]);
                                r.Fill = new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
                                Grid.SetColumn(r, j);
                                Grid.SetRow(r, i);
                                GameGrid.Children.Add(r);
                                break;
                            }
                    }
                }
        }

        private void Game_BackgroundChanged(object sender, EventArgs e)
        {
            Color c = Color.FromArgb(game.BackGround.A, game.BackGround.R, game.BackGround.G, game.BackGround.B);
            GameGrid.Background = new SolidColorBrush(c);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
            var dres = dialog.ShowDialog();

            if (dres != System.Windows.Forms.DialogResult.Cancel)
                game.BackGround = back = dialog.Color;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GenerateBtn.IsEnabled = uint.TryParse(LengthTextBox.Text, out uint c) && c != 0 &&
                uint.TryParse(WidthTextBox.Text, out uint w) && w != 0 &&
                (uint.TryParse(SnakeCountTextBox.Text, out uint y) && y != 0 || SnakeCountTextBox.Text == string.Empty);

            if (!uint.TryParse((sender as TextBox).Text, out uint t) || t == 0)
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.DarkRed);
            else
            {
                TextBox txt = sender as TextBox;
                txt.Foreground = new SolidColorBrush(Colors.Black);

                switch (txt.Tag)
                {
                    case "0":
                        {
                            GameGrid.ColumnDefinitions.Clear();
                            int count = int.Parse(txt.Text);
                            for (int i = 0; i < count; i++)
                                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                            break;
                        }
                    case "1":
                        {
                            GameGrid.RowDefinitions.Clear();
                            int count = int.Parse(txt.Text);
                            for (int i = 0; i < count; i++)
                                GameGrid.RowDefinitions.Add(new RowDefinition());
                            break;
                        }
                }
            }
        }

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            StartGameBtn.IsEnabled = false;
            game = new GameField(int.Parse(WidthTextBox.Text), int.Parse(LengthTextBox.Text));
            int cnt = 0;
            if(uint.TryParse(SnakeCountTextBox.Text, out uint c) && c != 0)
            {
                cnt = int.Parse(SnakeCountTextBox.Text);
                game = new GameField(game.Width, game.Height, false, cnt);
            }
            else
                cnt = new Random().Next(2, (int)Math.Sqrt(game.Height * game.Width));
            game.BackgroundChanged += Game_BackgroundChanged;
            game.FieldChanged += Game_FieldChanged;
            game.SetBorders(new Algoritmic.Point[] { new Algoritmic.Point(2, 2), new Algoritmic.Point(2, 3), new Algoritmic.Point(2, 4), new Algoritmic.Point(3, 4) });

            for (int i = 0; i < cnt; i++)
                game.AddSnake(new BorderSnake());
            

            StartGameBtn.IsEnabled = true;
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            game.StartGame();
        }
    }
}
