using System;
using System.Threading;
using System.Drawing;

namespace Algoritmic
{
    public class DiagonalSnake : Snake, IAlgorithm
    {
        private Thread loop;
        private IAsyncResult loopingResult = null;
        private Action<int> playing;

        public DiagonalSnake(int size) : base(size) => Direction = Direction.RightTop;
        public DiagonalSnake(int size, Color color) : base(size, color) => Direction = Direction.RightTop;
        public DiagonalSnake(int size, Color color, Direction direction) : this(size, color) { Direction = direction; }//добавить проверку на диагональныне направление
        public DiagonalSnake(int size, Direction direction) : this(size) { Direction = direction; }
        public DiagonalSnake(int size, Point startPoint) : base(size, startPoint) => Direction = Direction.RightTop;
        public DiagonalSnake(int size, Point startPoint, Direction direction) : this(size, startPoint) { Direction = direction; }

        public void Play(int time)
        {
            DateTime dTN = DateTime.Now;
            while ((DateTime.Now - dTN).TotalSeconds < time)
            {
                switch (Direction)
                {
                    case Direction.RightTop:
                        //esli mi dostigaem ugla
                        if (point.X == 1 && point.Y == 1)
                            Direction = Direction.Buttom;
                        else if (point.Y == 1)
                            Direction = Direction.RightButtom;
                        else if (point.X == 1)
                            Direction = Direction.LeftTop;
                        break;
                    case Direction.LeftTop:
                        if (point.X == 0 && point.Y == 1)
                            Direction = Direction.Right;
                        else if (point.X == 0)
                            Direction = Direction.RightTop;
                        else if (point.Y == 1)
                            Direction = Direction.LeftButtom;
                        break;
                    case Direction.RightButtom:
                        if (point == new Point(1, 0))
                            Direction = Direction.Left;
                        else if (point.X == 1)
                            Direction = Direction.LeftButtom;
                        else if (point.Y == 0)
                            Direction = Direction.RightTop;
                        break;
                    case Direction.LeftButtom:
                        if (point == new Point(0, 0))
                            Direction = Direction.Top;
                        else if (point.X == 0)
                            Direction = Direction.RightButtom;
                        else if (point.Y == 0)
                            Direction = Direction.LeftTop;
                        break;
                    case Direction.Top:
                        Direction = Direction.RightTop;
                        break;
                    case Direction.Right:
                        Direction = Direction.RightButtom;
                        break;
                    case Direction.Buttom:
                        Direction = Direction.LeftButtom;
                        break;
                    case Direction.Left:
                        Direction = Direction.LeftTop;
                        break;
                }
                //можно сделать общий метод у родителя для смены позиции
                switch (Direction)
                {
                    case Direction.Buttom:
                        point.Y--;
                        break;
                    case Direction.Left:
                        point.X--;
                        break;
                    case Direction.Top:
                        point.Y++;
                        break;
                    case Direction.Right:
                        point.X++;
                        break;
                    case Direction.RightButtom:
                        point.X++;
                        point.Y--;
                        break;
                    case Direction.RightTop:
                        point.X++;
                        point.Y++;
                        break;
                    case Direction.LeftButtom:
                        point.X--;
                        point.Y--;
                        break;
                    case Direction.LeftTop:
                        point.X--;
                        point.Y++;
                        break;
                }

            }
        }

        public void PlayLooping()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}