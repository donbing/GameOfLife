using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class NextGenerationCreator
    {
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

        public static void MakeNextGeneration(List<Cell> liveCells, List<Cell> newGeneration)
        {
            foreach (var cell in liveCells)
            {
                // find all the neighbours of the current cell.
                // count now many of those neighbours are alive.
                // if it's 2 or 3 thencreate a live cell at our curren cells position in the next generation.
                var liveCellNeighbours = offSets
                    .Select(offSet => Cell.CreatePosition(cell.Y + offSet.Item2, cell.X + offSet.Item1))
                   .ToList();

                var count = liveCellNeighbours.Count(liveCells.Contains);

                if (count == 2 || count == 3)
                {
                    newGeneration.Add(cell);
                }

                foreach (var neighbour in liveCellNeighbours)
                {
                    var neighbourCellNeighbours = offSets
                        .Select(offSet => Cell.CreatePosition(neighbour.Y + offSet.Item2, neighbour.X + offSet.Item1));

                    // count the neighbourneighbours that are alive
                    var countOfLiveNeighbours = neighbourCellNeighbours.Count(liveCells.Contains);

                    // work out if this neighbour is alive/dead
                    var isAlive = liveCells.Contains(neighbour);

                    // ignore this cell if the neighbour is alive.
                    if (isAlive == false)
                    {
                        // if count of live neighbours is exactly 3
                        if (countOfLiveNeighbours == 3)
                        {
                            // then add cell to next generation
                            newGeneration.Add(neighbour);
                        }
                    }
                }

                // go thru each of the neighbours and do the same.
            }
        }
    }
}
