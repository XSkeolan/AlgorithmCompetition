using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Algoritmic
{
    public class LineSnake //: Snake, IAlgorithm
    {
        //private Thread loop;
        //private IAsyncResult loopingResult = null;
        //private Action<int> playing;

        //public LineType LineType { get; private set; } = LineType.Vertical;

        //public LineSnake(int size) : base(size) => Direction = Direction.Top;
        //public LineSnake(int size, Color color) : base(size, color) => Direction = Direction.Top;
        //public LineSnake(int size, Color color, LineType lineType, Direction direction = Direction.Top) : this(size, color) =>
        //    SetDirection(lineType, direction);
        //public LineSnake(int size, LineType lineType, Direction direction = Direction.Top) : this(size) => SetDirection(lineType, direction);
        //public LineSnake(int size, Point startPoint) : base(size, startPoint) => Direction = Direction.Top;
        //public LineSnake(int size, Point startPoint, LineType lineType, Direction direction = Direction.Top) : this(size, startPoint) =>
        //    SetDirection(lineType, direction);

        //private void SetDirection(LineType line, Direction direction)
        //{
        //    if (line == LineType.Horizontal)
        //    {
        //        LineType = LineType.Horizontal;
        //        if (direction != Direction.Left && direction != Direction.Right)
        //            Direction = Direction.Right;
        //        else
        //            Direction = direction;
        //    }
        //    else
        //    {
        //        if (direction == Direction.Buttom)
        //            Direction = Direction.Buttom;
        //        else
        //            Direction = Direction.Top;
        //    }
        //}

        //private void BeginPlay(int time)
        //{
        //    Point lastPassedCorner = new Point(-1, -1);
        //    DateTime startDt = DateTime.Now;
        //    while (time==0 ? true : (DateTime.Now - startDt).TotalSeconds < time)
        //    {
        //        //check next operation with direction
        //        switch (LineType)
        //        {
        //            //правильно
        //            case LineType.Vertical:
        //                switch (Direction)
        //                {
        //                    case Direction.Top:
        //                        if (point.Y == 1) //до верхней границы
        //                            if (point.X == 0)//левая граница
        //                            {
        //                                Direction = Direction.Right;
        //                                lastPassedCorner = new Point(0, 1);
        //                            }
        //                            else if (point.X == 1)//правая граница
        //                            {
        //                                Direction = Direction.Left;
        //                                lastPassedCorner = new Point(1, 1);
        //                            }
        //                            else //не у края
        //                            {
        //                                if (lastPassedCorner.X != -1)
        //                                {
        //                                    if (lastPassedCorner == new Point(0, 1) || lastPassedCorner == new Point(0, 0))
        //                                        Direction = Direction.Right;
        //                                    else
        //                                        Direction = Direction.Left;
        //                                }
        //                                else
        //                                    Direction = Direction.Right;
        //                            }
        //                        break;
        //                    case Direction.Buttom:
        //                        if (point.Y == 0) //до нижней границы
        //                            if (point.X == 0)//левой стороны
        //                            {
        //                                Direction = Direction.Right;
        //                                lastPassedCorner = new Point(0, 0);
        //                            }
        //                            else if (point.X == 1)//правой стороны
        //                            {
        //                                Direction = Direction.Left;
        //                                lastPassedCorner = new Point(1, 0);
        //                            }
        //                            else
        //                            {
        //                                if (lastPassedCorner.X != -1)
        //                                {
        //                                    if (lastPassedCorner == new Point(0, 0) || lastPassedCorner == new Point(0, 1))
        //                                        Direction = Direction.Right;
        //                                    else
        //                                        Direction = Direction.Left;
        //                                }
        //                                else
        //                                    Direction = Direction.Right;
        //                            }
        //                        break;
        //                    case Direction.Left:
        //                    case Direction.Right:
        //                        if (point.Y == 0)
        //                            Direction = Direction.Top;
        //                        else
        //                            Direction = Direction.Buttom;
        //                        break;
        //                }
        //                break;
        //            //правильно
        //            case LineType.Horizontal:
        //                switch (Direction)
        //                {
        //                    case Direction.Left:
        //                        if (point.X == 0) //до левой границы
        //                            if (point.Y == 0)//низ
        //                            {
        //                                Direction = Direction.Top;
        //                                lastPassedCorner = new Point(0, 0);
        //                            }
        //                            else if (point.Y == 1)//верх
        //                            {
        //                                Direction = Direction.Buttom;
        //                                lastPassedCorner = new Point(0, 1);
        //                            }
        //                            else //не у края
        //                            {
        //                                if (lastPassedCorner.X != -1)
        //                                {
        //                                    if (lastPassedCorner == new Point(1, 0) || lastPassedCorner == new Point(0, 0))
        //                                        Direction = Direction.Top;
        //                                    else
        //                                        Direction = Direction.Buttom;
        //                                }
        //                                else
        //                                    Direction = Direction.Buttom;
        //                            }
        //                        break;
        //                    case Direction.Right:
        //                        if (point.X == 1) //до правой границы
        //                            if (point.Y == 0)//низ
        //                            {
        //                                Direction = Direction.Top;
        //                                lastPassedCorner = new Point(1, 0);
        //                            }
        //                            else if (point.Y == 1)//верх
        //                            {
        //                                Direction = Direction.Buttom;
        //                                lastPassedCorner = new Point(1, 1);
        //                            }
        //                            else //не у края
        //                            {
        //                                if (lastPassedCorner.X != -1)
        //                                {
        //                                    if (lastPassedCorner == new Point(0, 0) || lastPassedCorner == new Point(1, 0))
        //                                        Direction = Direction.Top;
        //                                    else
        //                                        Direction = Direction.Buttom;
        //                                }
        //                                else
        //                                    Direction = Direction.Buttom;
        //                            }
        //                        break;
        //                    case Direction.Top:
        //                    case Direction.Buttom:
        //                        if (point.X == 0)
        //                            Direction = Direction.Right;
        //                        else
        //                            Direction = Direction.Left;
        //                        break;
        //                }
        //                break;
        //        }

        //        //run 1 operation
        //        switch (Direction)
        //        {
        //            case Direction.Top:
        //                point.Y++;
        //                break;
        //            case Direction.Right:
        //                point.X++;
        //                break;
        //            case Direction.Buttom:
        //                point.Y--;
        //                break;
        //            case Direction.Left:
        //                point.X--;
        //                break;
        //        }
        //        if (state == States.Stoped)
        //            break;
        //        //PositionChanged.Invoke(this,new PositionEventArgs(lastPos,nowPos))
        //    }
        //}

        //public void Play(int time)
        //{
        //    if (time < 0)
        //        throw new ArgumentOutOfRangeException();
        //    if (time >= 0)
        //    {
        //        playing = BeginPlay;
        //        loop = new Thread((o) =>
        //        {
        //            int t = Convert.ToInt32(o);
        //            loopingResult = playing.BeginInvoke(t, null, null);
        //        });
        //        loop.Start(time);
        //        state = States.PlayAlgoritm;
        //    }
        //}

        //public void PlayLooping() => Play(0);

        //public void Stop()
        //{
        //    state = States.Stoped;
        //    //ожидаем окончание метода
        //    playing.EndInvoke(loopingResult);
        //    loop.Abort();
        //    state = States.PlayMan;
        //}
    }

    public enum LineType
    {
        Horizontal,
        Vertical
    }

    public enum States
    {
        PlayAlgoritm,
        PlayMan,
        Stoped
    }
}