using System;
using System.Drawing;

namespace Algoritmic
{
    public interface ISnake
    {
        event EventHandler SizeChanged;
        event EventHandler DirectionChanged;

        int Size { get; set; }
        Color Color { get; }
        Direction Direction { get; set; }
        Point Position { get; set; }

        void Draw();
    }
}