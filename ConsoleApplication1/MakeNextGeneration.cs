using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class NextGenerationCreator
    {
        public static void MakeNextGeneration(HashSet<Cell> liveCells, HashSet<Cell> newGeneration)
        {
          //  Console.WriteLine("liveCells: {0} - newGeneration: {1}",liveCells.Count, newGeneration.Count);
            foreach (var cell in liveCells)
            {
                // find all the neighbours of the current cell.
                // count now many of those neighbours are alive.
                // if it's 2 or 3 thencreate a live cell at our curren cells position in the next generation.
                var liveCellNeighbours = cell.GetLiveCellNeighbours();
                var count = liveCellNeighbours.Count(liveCells.Contains);
                if (count == 2 || count == 3)
                {
                    newGeneration.Add(cell);
                }

                foreach (var neighbour in liveCellNeighbours)
                {
                    var neighbourCellNeighbours = neighbour.GetLiveCellNeighbours();
                    // count the neighbourneighbours that are alive
                    var countOfLiveNeighbours = neighbourCellNeighbours.Count(liveCells.Contains);
                    // work out if this neighbour is alive/dead
                    var isAlive = liveCells.Contains(neighbour);
                    // ignore this cell if the neighbour is alive.
                    if (isAlive == false && countOfLiveNeighbours == 3)
                    {
                        // if count of live neighbours is exactly 3
                        // then add cell to next generation
                        newGeneration.Add(neighbour);                        
                    }
                }
            }
        }
    }
}
