using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Cell
    {
        public int x;
        public int y;
    }

    class Program
    {
		static Random random = new Random ();
		static int width = 80, height = 40;

		static void Main(string[] args)
		{
		    var liveCells = GenerateLiveCells();
            var newGeneration = new List<Cell>();
            DrawCells(liveCells);

            var offSets = new[]
            {
                    Tuple.Create(-1,-1),
                    Tuple.Create(-1,0),
                    Tuple.Create(-1,+1),
                    Tuple.Create(0,-1),
                    Tuple.Create(0,-1),
                    Tuple.Create(+1,-1),
                    Tuple.Create(+1,0),
                    Tuple.Create(+1,+1),
            };

            foreach (var cell in liveCells)
		    {
		        var count = 0;
		        foreach (var offSet in offSets)
		        {
		            var newX = cell.x + offSet.Item1;
		            var newY = cell.y + offSet.Item2;

		            var newCell = CreatePosition (newY, newX);

		            if (liveCells.Contains(newCell))
		            {
		                count++;
		            }
		        }
               
		        if (count == 2 || count == 3)
		        {
		            newGeneration.Add(cell);
		        }
                
            }
            Thread.Sleep(5000);

            DrawCells(newGeneration);

            Console.ReadKey();
		}

        private static void DrawCells(List<Cell> liveCells)
        {
            Console.Clear();
            foreach (var cell in liveCells)
            {
                Console.SetCursorPosition(cell.x, cell.y);
                Console.Write("X");
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
			var numberOfLiveCells = random.Next (40, 50);
			for (var count = 0; count <= numberOfLiveCells; count++) {
				var coordinatePair = CreatePosition (random.Next (width), random.Next (height));
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

		static Cell CreatePosition (int yCoordinate, int xCoordinate)
		{
			return new Cell {x=xCoordinate, y=yCoordinate};
		}

		static Cell CreatePositionFromCrappyUserKeyedInput (string inputValue)
		{
			var commaSeperatedInput = NumbersOrCommasOnly (inputValue).Split (',');
			return CreatePosition (int.Parse (commaSeperatedInput [0]), int.Parse (commaSeperatedInput [1]));
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
