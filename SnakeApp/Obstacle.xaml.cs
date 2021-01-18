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

namespace SnakeApp
{
    /// <summary>
    /// Логика взаимодействия для Obstacle.xaml
    /// </summary>
    public partial class Obstacle : UserControl
    {
        public Obstacle()
        {
            InitializeComponent();
        }

        public BranchesOut Branching
        {
            get { return (BranchesOut)GetValue(BranchingProperty); }
            set { SetValue(BranchingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Branching.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BranchingProperty =
            DependencyProperty.Register("Branching",
                typeof(BranchesOut),
                typeof(Obstacle),
                new UIPropertyMetadata(BranchesOut.All, new PropertyChangedCallback(BranchingChanged)));
        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(Obstacle), new UIPropertyMetadata(Colors.Black, new PropertyChangedCallback(ColorChanged)));


        private static void BranchingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BranchesOut b = (BranchesOut)e.NewValue;
            Obstacle o = (Obstacle)d;
            Grid grid = o.MainGrid;
            IEnumerable<Rectangle> rect = grid.Children.OfType<Rectangle>();

            switch (b)
            {
                case BranchesOut.All:
                    {
                        foreach (Rectangle ui in rect)
                            ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.UpDown:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 0 || Grid.GetColumn(ui) == 2)
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.LeftRight:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetRow(ui) == 0 || Grid.GetRow(ui) == 2)
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.TUp:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetRow(ui) == 1 || (Grid.GetColumn(ui) == 1 && Grid.GetRow(ui) == 0))
                                ui.Visibility = Visibility.Visible;
                            else
                                ui.Visibility = Visibility.Hidden;
                        break;
                    }
                case BranchesOut.TDown:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetRow(ui) == 1 || (Grid.GetColumn(ui) == 1 && Grid.GetRow(ui) == 2))
                                ui.Visibility = Visibility.Visible;
                            else
                                ui.Visibility = Visibility.Hidden;
                        break;
                    }
                case BranchesOut.TLeft:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 1 || (Grid.GetColumn(ui) == 0 && Grid.GetRow(ui) == 1))
                                ui.Visibility = Visibility.Visible;
                            else
                                ui.Visibility = Visibility.Hidden;
                        break;
                    }
                case BranchesOut.TRight:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 1 || (Grid.GetColumn(ui) == 2 && Grid.GetRow(ui) == 1))
                                ui.Visibility = Visibility.Visible;
                            else
                                ui.Visibility = Visibility.Hidden;
                        break;
                    }
                case BranchesOut.AngleUpLeft:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 2 || Grid.GetRow(ui) == 2 ||(Grid.GetRow(ui) == 0 && Grid.GetColumn(ui) == 0))
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.AngleUpRight:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 0 || Grid.GetRow(ui) == 2 || (Grid.GetRow(ui) == 0 && Grid.GetColumn(ui) == 2))
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.AngleDownRight:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 0 || Grid.GetRow(ui) == 0 || (Grid.GetRow(ui) == 2 && Grid.GetColumn(ui) == 2))
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
                case BranchesOut.AngleDownLeft:
                    {
                        foreach (Rectangle ui in rect)
                            if (Grid.GetColumn(ui) == 2 || Grid.GetRow(ui) == 0 || (Grid.GetRow(ui) == 2 && Grid.GetColumn(ui) == 0))
                                ui.Visibility = Visibility.Hidden;
                            else
                                ui.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Obstacle o = (Obstacle)d;
            Grid grid = o.MainGrid;
            foreach (Rectangle ui in grid.Children.OfType<Rectangle>())
                ui.Fill = new SolidColorBrush((Color)e.NewValue);
        }
    }
}
