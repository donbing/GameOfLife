using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
		static Random random = new Random ();
		static int width = 80, height = 40;

		static void Main(string[] args)
		{
		    var liveCells = GenerateLiveCells().ToList();
            var newGeneration = new List<Cell>();
            DrawCells(liveCells);
		    while (true)
		    {
		        var offSets = new[]
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

		        GetValue(liveCells, offSets, newGeneration);

		        Thread.Sleep(800);
		        liveCells = newGeneration;
		        DrawCells(newGeneration);
		    }
		}

        private static void GetValue(List<Cell> liveCells, Tuple<int, int>[] offSets, List<Cell> newGeneration)
        {
            foreach (var cell in liveCells)
            {
                // find all the neighbours of the current cell.
                // count now many of those neighbours are alive.
                // if it's 2 or 3 thencreate a live cell at our curren cells position in the next generation.
                var liveCellNeighbours = offSets
                    .Select(offSet => Cell.CreatePosition(cell.Y + offSet.Item2, cell.X + offSet.Item1));

                var count = liveCellNeighbours.Count(newCell => liveCells.Contains(newCell));

                if (count == 2 || count == 3)
                {
                    newGeneration.Add(cell);
                }

                foreach (var neighbour in liveCellNeighbours)
                {
                    var neighbourCellNeighbours = offSets
                        .Select(offSet => Cell.CreatePosition(neighbour.Y + offSet.Item2, neighbour.X + offSet.Item1));

                    // count the neighbourneighbours that are alive
                    var countOfLiveNeighbours = neighbourCellNeighbours.Count(newCell => liveCells.Contains(newCell));

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

        private static void DrawCells(List<Cell> liveCells)
        {
            Console.Clear();
            foreach (var cell in liveCells)
            {
                if ((cell.X >= 0) && (cell.Y >= 0) && (cell.X <width) && (cell.Y <height))
                {
                    Console.SetCursorPosition(cell.X, cell.Y);
                  Console.Write("X");
                }
            }
        }

        static List<Cell> GenerateLiveCells()
        {
			DisplayMenu (new string[]{
				"Welcome to the Game of Life!",
				"Options",
				"Press 1 for a randomly generated game",
				"Press 2 to enter live cells",
			});

			var selectedOption = Console.ReadKey ();

            if (selectedOption.Key == ConsoleKey.D1)
				return AddRandomNumberOfRandomlyPositionedCells ();

            if (selectedOption.Key == ConsoleKey.D2)
                return GetUserGeneratedCellPositions ();

			return new List<Cell>();
        }

		static List<Cell> AddRandomNumberOfRandomlyPositionedCells ()
		{
			var allCoordinates = new List<Cell> ();
			var numberOfLiveCells = random.Next (800,1000 );
			for (var count = 0; count <= numberOfLiveCells; count++) {
				var coordinatePair = Cell.CreatePosition (random.Next (height), random.Next(width));
				allCoordinates.Add (coordinatePair);
			}
			return allCoordinates;
		}

		static List<Cell> GetUserGeneratedCellPositions ()
		{
			DisplayMenu(new string[]{
				"Enter coordinates of cells with X and Y values seperated by a comma, press s to finish!",
				"Press o to return to Options",
			});

			var allCoordinates = new List<Cell>();
			var sNotPressed = true;
			while (sNotPressed) {
				var inputValue = Console.ReadLine ();

				switch (inputValue.ToLower()) {
					case "o":
						return GenerateLiveCells ();
					case "s":
						sNotPressed = false;
						break;
					default:
						var coordinatePair = CreatePositionFromCrappyUserKeyedInput (inputValue);
						allCoordinates.Add (coordinatePair);
						break;
				}
			}

			return allCoordinates;
		}

        static Cell CreatePositionFromCrappyUserKeyedInput (string inputValue)
		{
			var commaSeperatedInput = NumbersOrCommasOnly (inputValue).Split (',');
			return Cell.CreatePosition (int.Parse (commaSeperatedInput [0]), int.Parse (commaSeperatedInput [1]));
		}

		static string NumbersOrCommasOnly (string inputValue)
		{
			return new string (inputValue.Where (character => char.IsNumber (character) || character == ',').ToArray ());
		}

		static void DisplayMenu (string[] menulines)
		{
			Console.Clear ();
			Console.WriteLine (string.Join(Environment.NewLine, menulines));
		}
    }
}
