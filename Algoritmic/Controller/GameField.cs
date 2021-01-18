using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Algoritmic.Model;

namespace Algoritmic.Controller
{
    public class GameField
    {
        private int[,] field;
        //кодирование на поле:
        //1 - граница
        //0 - пустота
        //2 - 16777215 - змейки
        //-1 - минус размер(мухоморчик)
        //-2 - +1 в размере
        //-5 - бонус(пока не реализован)
        private Thread t;
        private List<IAlgorithm> alg;
        private readonly int initialCount;
        private Color backColor = Color.Green;

        public int Height { get; }
        public int Width { get; }
        public bool Bounce { get; } = false;
        public bool IsPlaying { get; private set; } = false;
        public Color BackGround
        {
            get => backColor;
            set
            {
                backColor = value;
                BackgroundChanged?.Invoke(this,new EventArgs());
            }
        }
        public Point[] Borders
        {
            get
            {
                List<Point> borders = new List<Point>();
                for (int i = 0; i < field.GetLength(0); i++)
                    for (int j = 0; j < field.GetLength(1); j++)
                        if (field[i, j] == 1)
                            borders.Add(new Point(i, j));
                return borders.ToArray();
            }
        }
        public List<ISnake> Snakes { get; }

        public GameField(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException("width", "Длина игрового поля должна быть больше 0");
            if (height < 0)
                throw new ArgumentOutOfRangeException("height", "Высота игрового поля должна быть больше 0");
            //if (width * height < 25)
            //    throw new ArgumentOutOfRangeException("width, height", "Площадь игрового поля должна быть не меньше 25 клеток");

            Width = width;
            Height = height;
            field = new int[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    field[i, j] = 0;
            Snakes = new List<ISnake>();
            alg = new List<IAlgorithm>();
            initialCount = 0;
            BackgroundChanged?.Invoke(this, new EventArgs());
            FieldChanged?.Invoke(this, new EventArgs());
        }
        public GameField(int width, int height, bool bonuce, int countSnake) : this(width, height)
        {
            if (countSnake < 1)
                throw new ArgumentOutOfRangeException("countSnake", "Количество змеек в игровом поле должно быть больше 0");
            Bounce = bonuce;
            initialCount = countSnake;
            Snakes = new List<ISnake>(countSnake);
            alg = new List<IAlgorithm>(countSnake);
        }

        public void AddSnake(IAlgorithm snake)
        {
            if (snake == null)
                throw new ArgumentNullException("snake", "Параметр snake равен null");
            
            BeforeAdding();
            Random rnd = new Random();
            int x; //= rnd.Next(0, Width);
            int y;// = rnd.Next(0, Height);
            bool IsEmpty = true;
            Snake s = null;

            do
            {
                x = rnd.Next(0, Width);
                y = rnd.Next(0, Height);

                Point pos = PositionToArray(new Point(x, y));

                if (IsEmpty && field[pos.X, pos.Y] != 0)
                    continue;

                s = new Snake(this, new Point(x, y));
                s.Size = rnd.Next(1, Height < Width ? Height : Width);
                for (int i = 5; i < 9; i++)
                {
                    IsEmpty = true;
                    s.Direction = (Direction)i;
                    switch (s.Direction)
                    {
                        case Direction.Top:
                            {
                                if (y + s.Size < Height)
                                    for (int j = y; j < y + s.Size; j++)
                                    {
                                        pos = PositionToArray(new Point(x, j));
                                        IsEmpty = IsEmpty && field[pos.X,pos.Y] == 0;
                                    }
                                else
                                    IsEmpty = false;
                                break;
                            }
                        case Direction.Left:
                            {
                                if (x + s.Size < Width)
                                    for (int j = x; j < x + s.Size; j++)
                                    {
                                        pos = PositionToArray(new Point(j, y));
                                        IsEmpty = IsEmpty && field[pos.X, pos.Y] == 0;
                                    }
                                else
                                    IsEmpty = false;
                                break;
                            }
                        case Direction.Right:
                            {
                                if (x - s.Size > 0)
                                    for (int j = x; j > x - s.Size; j--)
                                    {
                                        pos = PositionToArray(new Point(j, y));
                                        IsEmpty = IsEmpty && field[pos.X, pos.Y] == 0;
                                    }
                                else
                                    IsEmpty = false;
                                break;
                            }
                        case Direction.Buttom:
                            {
                                if (y - s.Size > 0)
                                    for (int j = y; j > y - s.Size; j--)
                                    {
                                        pos = PositionToArray(new Point(x, j));
                                        IsEmpty = IsEmpty && field[pos.X, pos.Y] == 0;
                                    }
                                else
                                    IsEmpty = false;
                                break;
                            }
                    }
                    if (IsEmpty)
                        break;
                }
            }
            while (!IsEmpty);

            Snakes.Add(s);
            s.SizeChanged += Snake_SizeChanged;
            AddSnakeToField(s);
            alg.Add(snake);
            //мб вернуть цвет
        }

        private void Snake_SizeChanged(object sender, EventArgs e)
        {
            ISnake snake = (ISnake)sender;
            Point lastPoint = snake.TailPosition;
            int color = snake.Color.ToArgb();

            if (lastPoint == 0)
                if (field[lastPoint.X + 1, lastPoint.Y] == color)
                    field[lastPoint.X, lastPoint.Y + 1] = color;
                else
                    field[lastPoint.X + 1, lastPoint.Y] = color;
            else if(lastPoint == new Point(Width-1,Height-1))
                if (field[lastPoint.X - 1, lastPoint.Y] == color)
                    field[lastPoint.X, lastPoint.Y - 1] = color;
                else
                    field[lastPoint.X - 1, lastPoint.Y] = color;
            else if(lastPoint == new Point(0,Height-1))
                if (field[lastPoint.X + 1, lastPoint.Y] == color)
                    field[lastPoint.X, lastPoint.Y - 1] = color;
                else
                    field[lastPoint.X + 1, lastPoint.Y] = color;
            else if(lastPoint == new Point(Width-1,0))
                if (field[lastPoint.X - 1, lastPoint.Y] == color)
                    field[lastPoint.X, lastPoint.Y + 1] = color;
                else
                    field[lastPoint.X - 1, lastPoint.Y] = color;
            else
                if (field[lastPoint.X + 1, lastPoint.Y] == color)
                    field[lastPoint.X - 1, lastPoint.Y] = color;
                else if (field[lastPoint.X - 1, lastPoint.Y] == color)
                    field[lastPoint.X + 1, lastPoint.Y] = color;
                else if (field[lastPoint.X, lastPoint.Y + 1] == color)
                    field[lastPoint.X, lastPoint.Y - 1] = color;
                else
                    field[lastPoint.X, lastPoint.Y + 1] = color;
        }

        public void AddSnake(IAlgorithm snake, ISnake view)
        {
            BeforeAdding(view);
            Snakes.Add(view);
            view.SizeChanged += Snake_SizeChanged;
            AddSnakeToField(view);
            alg.Add(snake);
        }

        private void AddSnakeToField(ISnake s)
        {
            Point pos = PositionToArray(s.HeaderPosition);
            int x = pos.X;
            int y = pos.Y;
            switch (s.Direction)
            {
                case Direction.Top:
                    {
                        for (int i = y; i < y + s.Size; i++)
                            field[x, i] = s.Color.ToArgb();
                        break;
                    }
                case Direction.Buttom:
                    {
                        for (int i = y; i > y - s.Size; i--)
                            field[x, i] = s.Color.ToArgb();
                        break;
                    }
                case Direction.Left:
                    {
                        for (int i = x; i < x + s.Size; i++)
                            field[i, y] = s.Color.ToArgb();
                        break;
                    }
                case Direction.Right:
                    {
                        for (int i = x; i > x - s.Size; i--)
                            field[i, x] = s.Color.ToArgb();
                        break;
                    }
            }
            FieldChanged?.Invoke(this, new EventArgs());
        }
        private Point PositionToArray(Point p) => new Point(Height - 1 - p.Y, p.X);
        private void BeforeAdding(ISnake snake = null)
        {
            if (IsPlaying)
                throw new InvalidOperationException("Невозможно добавить змею, когда игра уже идёт");
            if (initialCount != 0 && Snakes.Count == Snakes.Capacity)
                throw new ArgumentOutOfRangeException("Невозможно добавить змею! Превышен лимит количества змеек");
            if (snake != null)
            {
                if (snake.Size < Width && snake.Size < Height)
                    throw new InvalidOperationException("Невозможно добавить змею! Длинна змеи должна быть в границах от 0 до длины или ширины игрового поля");
                Point pos = PositionToArray(snake.HeaderPosition);
                if (field[pos.X, pos.Y] != 0)
                    throw new InvalidOperationException($"Змея со стартовой позицией {snake.HeaderPosition} не может быть добавлена, так как место занято другим объектом");

                bool IsEmpty = true;
                int x = pos.X;
                int y = pos.Y;

                switch (snake.Direction)
                {
                    case Direction.Top:
                        {
                            if (y + snake.Size < Height)
                                for (int j = y; j < y + snake.Size; j++)
                                    IsEmpty = IsEmpty && field[x, j] == 0;
                            else
                                throw new InvalidOperationException("Змея не может быть добавлена, так как из-за размера выходит за границы игрового поля");

                            if (!IsEmpty)
                                throw new InvalidOperationException("Змея не может быть добавлена! Попытка добавления змеи на не пустое место");
                            break;
                        }
                    case Direction.Left:
                        {
                            if (x + snake.Size < Width)
                                for (int j = x; j < x + snake.Size; j++)
                                    IsEmpty = IsEmpty && field[j, y] == 0;
                            else
                                throw new InvalidOperationException("Змея не может быть добавлена, так как из-за размера выходит за границы игрового поля");

                            if (!IsEmpty)
                                throw new InvalidOperationException("Змея не может быть добавлена! Попытка добавления змеи на не пустое место");
                            break;
                        }
                    case Direction.Right:
                        {
                            if (x - snake.Size > 0)
                                for (int j = x; j > x - snake.Size; j--)
                                    IsEmpty = IsEmpty && field[j, y] == 0;
                            else
                                throw new InvalidOperationException("Змея не может быть добавлена, так как из-за размера выходит за границы игрового поля");

                            if (!IsEmpty)
                                throw new InvalidOperationException("Змея не может быть добавлена! Попытка добавления змеи на не пустое место");
                            break;
                        }
                    case Direction.Buttom:
                        {
                            if (y - snake.Size > 0)
                                for (int j = y; j > y - snake.Size; j--)
                                    IsEmpty = IsEmpty && field[x, j] == 0;
                            else
                                throw new InvalidOperationException("Змея не может быть добавлена, так как из-за размера выходит за границы игрового поля");

                            if (!IsEmpty)
                                throw new InvalidOperationException("Змея не может быть добавлена! Попытка добавления змеи на не пустое место");
                            break;
                        }
                }
                //мб сделать своё исключение и подать информацию об этом объекте
            }
        }

        public void RemoveSnake(int color)
        {
            bool IsExist = false;
            foreach (ISnake snake in Snakes)
                if (snake.Color == Color.FromArgb(color))
                {
                    IsExist = true;
                    break;
                }
            if (!IsExist)
                throw new ArgumentException("Змеи такого цвета не существует", "color");

            if (IsPlaying && !Snakes.Find((x) => x.Color == Color.FromArgb(color)).IsDied)
                throw new InvalidOperationException("Невозможно удалить змею, когда игра уже идёт");
            else
            {
                Snakes.Remove(Snakes.Find(x => x.Color == Color.FromArgb(color)));
                for (int i = 0; i < field.GetLength(0); i++)
                    for (int j = 0; j < field.GetLength(1); j++)
                        if (field[i, j] == color)
                            field[i, j] = 0;
            }
        }
        public void ClearField()
        {
            if (IsPlaying)
                throw new InvalidOperationException("Очистка игрового поля не может быть выполнена, когда идет игра");

            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    field[i, j] = 0;
        }
        public void Clear()
        {
            ClearField();
            Snakes.Clear();
            alg.Clear();
        }

        //Если есть метод очистки -> должен быть метод перерисовки
        //public void RepaintField()
        //{

        //}

        public void StartGame()
        {
            t = new Thread(() =>
            {
                while(IsPlaying)
                {
                    //На счет времени выполнения 
                    //можно создать отдельный поток, который будет фиксировать время запуска метода Do()
                    //Соответственно если прошло некоторое время а поток до сих пор не завершил работу, то вызвать исключение связанное с временем выполнения метода Do()
                    
                    Parallel.For(0, alg.Count, (i) =>
                      {
                          alg[i].Do();
                      });
                    FieldChanged.Invoke(this, new EventArgs());
                    Thread.Sleep(1000);
                }
            });
            IsPlaying = true;
            t.Start();
        }

        public void SetBorders(params Point[] points)
        {
            for(int i=0;i<points.Length;i++)
            {
                Point p = PositionToArray(points[i]);
                field[p.X, p.Y] = 1;
            }
            FieldChanged.Invoke(this, new EventArgs());
        }

        public void StopGame() => IsPlaying = false;

        public int[,] GetField() => field;

        public event EventHandler FieldChanged;
        public event EventHandler BackgroundChanged;
    }
}
