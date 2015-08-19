using System.Security;

namespace ConsoleApplication1
{
    class Cell
    {
        protected bool Equals(Cell toCompareTo)
        {
            return X == toCompareTo.X && Y == toCompareTo.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static Cell CreatePosition (int yCoordinate, int xCoordinate)
        {
            return new Cell
            {
                X = xCoordinate,
                Y = yCoordinate,
            };
        }

        public override string ToString()
        {
            return ""+X+","+Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Cell) obj);
        }
    }
}