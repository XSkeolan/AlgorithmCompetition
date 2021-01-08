using System;
using System.Collections.Generic;
using System.Drawing;
using Algoritmic.Controller;

namespace Algoritmic.Model
{
    public class Snake : ISnake
    {
        private int size = 5;
        private GameField field;
        private Point point;
        private Point tailPoint;

        public int Size
        {
            get => size;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Size", "Значение размера змеи должно быть не менее 1");
                size = value;
                SizeChanged.Invoke(this, new EventArgs());
            }
        }

        public bool IsDied
        {
            get
            {
                for (int i = 0; i < field.Borders.Length; i++)
                    if (field.Borders[i] == HeaderPosition)
                        return true;
                if (HeaderPosition.X < 0 || HeaderPosition.Y < 0 ||
                    HeaderPosition.Y > field.Height || HeaderPosition.X > field.Width)
                    return true;
                return false;
            }
        }

        public Color Color { get; } = Color.AliceBlue;

        public Direction Direction { get; set; }
        public Point HeaderPosition
        {
            get => point;
            set => SetPosition(value);
        }
        public Point TailPosition { get => tailPoint; }

        public Snake(GameField gameField, Point startPos, Direction direction = Direction.Top)
        {
            if (startPos.X < 0 || startPos.Y < 0)
                throw new ArgumentOutOfRangeException("startPos", "Координаты игрового поля начинаются с 0");
            HeaderPosition = startPos;
            field = gameField;
            Direction = direction;
            Random rnd = new Random();
            //добавить проверку на существование такого цвета и не равен ли он заднему фону
            Color = Color.FromArgb(rnd.Next(50, 101), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(2, 256));
        }
        public Snake(GameField gameField, Point startPos, Direction direction, Color color)
        {
            if (startPos.X < 0 || startPos.Y < 0)
                throw new ArgumentOutOfRangeException("startPos", "Координаты игрового поля начинаются с 0");
            HeaderPosition = startPos;
            field = gameField;
            Direction = direction;
            Color = color;
        }

        public event EventHandler SizeChanged;
        public event EventHandler DirectionChanged;

        protected virtual void SetPosition(Point p)
        {
            if (p.X < 0 || p.X > field.Width - 1)
                throw new ArgumentOutOfRangeException("HeaderPosition.X", "Позиция по оси X должна быть положительной и не больше длинный игрового поля или 0");
            if (p.Y < 0 || p.Y > field.Height -1)
                throw new ArgumentOutOfRangeException("HeaderPosition.Y", "Позиция по оси Y должна быть положительной и не больше ширины игрового поля или 0");

            Point delta = p - point;
            tailPoint += delta;
            point = p;
        }

        private bool CheckColor()
        {
            return true;
        }
    }
}
