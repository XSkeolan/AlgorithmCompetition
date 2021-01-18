using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmic.Model
{
    public interface ISnake
    {
        event EventHandler SizeChanged;
        event EventHandler DirectionChanged;
        event EventHandler PositionChanged;

        int Size { get; set; }
        bool IsDied { get; }
        Color Color { get; }
        Direction Direction { get; set; }
        Point HeaderPosition { get; set; }
        Point TailPosition { get; }
    }
}
