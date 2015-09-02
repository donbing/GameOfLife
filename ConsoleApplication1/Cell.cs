using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace ConsoleApplication1
{
    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Cell CreatePosition(int yCoordinate, int xCoordinate)
        {
            return new Cell
            {
                X = xCoordinate,
                Y = yCoordinate,
            };
        }
        
        public List<Cell> GetLiveCellNeighbours()

        {
            return offSets
                .Select(offSet => CreatePosition(Y + offSet.Item2, X + offSet.Item1))
                .ToList();
        }

        static Tuple<int, int>[] offSets =
        {
            Tuple.Create(-1, -1),
            Tuple.Create(-1, 0),
            Tuple.Create(-1, +1),
            Tuple.Create(0, -1),
            Tuple.Create(0, +1),
            Tuple.Create(+1, -1),
            Tuple.Create(+1, 0),
            Tuple.Create(+1, +1),
        };

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
    }
}