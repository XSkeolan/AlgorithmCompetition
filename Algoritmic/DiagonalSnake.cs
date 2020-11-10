using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public DiagonalSnake(int size, Color color, Direction direction) : this(size, color) { Direction = direction; }
        public DiagonalSnake(int size, Direction direction) : this(size) { Direction = direction; }
        public DiagonalSnake(int size, Point startPoint) : base(size, startPoint) => Direction = Direction.RightTop;
        public DiagonalSnake(int size, Point startPoint, Direction direction) : this(size, startPoint) { Direction = direction; }

        public void Play(int time)
        {
            throw new NotImplementedException();
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