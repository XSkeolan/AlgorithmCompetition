namespace Algoritmic
{
    public struct Point
    {
        private int xPos;
        private int yPos;

        public Point(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public int X
        {
            get => xPos;
            set => xPos = value;
        }

        public int Y
        {
            get => yPos;
            set => yPos = value;
        }

        public override string ToString()
        {
            return string.Format("[{0}; {1}]", xPos, yPos);
        }

        public override bool Equals(object obj)
        {
            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public void Reset()
        {
            xPos = default(int);
            yPos = default(int);
        }

        public static Point operator ++(Point p)
        {
            return new Point(p.X++, p.Y++);
        }

        public static Point operator --(Point p)
        {
            return new Point(p.X--, p.Y--);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator +(Point p1, int change)
        {
            return new Point(p1.X + change, p1.Y + change);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator -(Point p1, int change)
        {
            return new Point(p1.X - change, p1.Y - change);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }
    }
}