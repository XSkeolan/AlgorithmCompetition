using System;
using System.Drawing;

namespace Algoritmic
{
    public abstract class Snake : ISnake
    {
        protected int size = 1;
        protected readonly Color color = Color.Black;
        protected Point point = new Point(0, 0);
        protected States state;
        
        public int Size
        {
            get => size;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Size", "Значение размера змеи должно быть не менее 1");
                size = value;
            }
        }
        public Color Color => color;
        public Direction Direction { get; set; } = Direction.Top;
        public Point Position
        {
            get => point;
            set => SetPosition(value);
        }

        public States State => state;

        public Snake() { }
        public Snake(int size)
        {
            if(size < 0)
                throw new ArgumentOutOfRangeException("size", "Значение размера змеи должно быть не менее 1");
            this.size = size;
        }
        public Snake(int size, Color color) : this(size) => this.color = color;
        public Snake(int size, Color color, Point start) : this(size, color) => SetPosition(start);
        public Snake(int size, Color color, Point start, Direction direction) : this(size, color, start) => Direction = direction;
        public Snake(int size, Direction direction) : this(size) => Direction = direction;
        public Snake(int size, Point start):this(size) => SetPosition(start);


        public event EventHandler SizeChanged;
        public event EventHandler DirectionChanged;

        public void Draw() //мб не понадобиться а только в полигоне для игры
        {
            throw new NotImplementedException();
        }
        protected void SetPosition(Point p)
        {
            if (p.X < 0)
                throw new ArgumentOutOfRangeException("Position.X", "Позиция по оси X должна быть положительной или 0");
            if (p.Y < 0)
                throw new ArgumentOutOfRangeException("Position.Y", "Позиция по оси Y должна быть положительной или 0");
            //добавить проверку на максимум 
            point = p;
        }
    }
}