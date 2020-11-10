using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algoritmic
{
    public class CircleSnake : Snake, IAlgorithm
    {
        private readonly StartPosition startPosition = StartPosition.Center;
        /// <param name="time">Время для работы алгоритма в секундах</param>
        public void Play(int time)
        {
            DateTime startDt = DateTime.Now;
            while((DateTime.Now - startDt).TotalSeconds < time)
            {
                switch(Direction)
                {
                }
            }
        }

        public CircleSnake()
        {
            point = new Point(0, 0);
            //Direction = 
        }

        public void PlayLooping()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public StartPosition StartPosition => startPosition;
    }

    public enum StartPosition
    {
        Center,
        Border
    }
}