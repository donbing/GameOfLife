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
		static int width = Console.WindowWidth, height = Console.WindowHeight;

        private static void Main(string[] args)
        {


            var liveCells = GenerateLiveCells().ToList();
            DrawCells(liveCells);
            var allTheTimes = new List<TimeSpan>();
            while (true)
            {
                var keyPress = Console.ReadKey(true).Key;
                
                if (  keyPress != ConsoleKey.Escape)
                {
                    var newGeneration = new List<Cell>();
                    var currentTime = DateTime.Now;
                    NextGenerationCreator.MakeNextGeneration(liveCells, newGeneration);
                    var timeToMakeEachGeneration = DateTime.Now - currentTime;
                    allTheTimes.Add(timeToMakeEachGeneration);
                    Thread.Sleep(800);
                    liveCells = newGeneration;
                    DrawCells(newGeneration);
                }
                else
                {
                    foreach (var time in allTheTimes)
                    {
                        Console.WriteLine(time);
                    }
                }

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
                "Press 3 for a glider"
			});

			var selectedOption = Console.ReadKey ();

            if (selectedOption.Key == ConsoleKey.D1)
				return AddRandomNumberOfRandomlyPositionedCells ();

            if (selectedOption.Key == ConsoleKey.D2)
                return GetUserGeneratedCellPositions ();
            if (selectedOption.Key == ConsoleKey.D3)
                return GetGliderCellPositions();

            return new List<Cell>();
        }

        private static List<Cell> GetGliderCellPositions()
        {
            var gliderCoordinates = new List<Cell>
            {
                Cell.CreatePosition(2,0),
                Cell.CreatePosition(2,1),
                Cell.CreatePosition(2,2),
                Cell.CreatePosition(1,2),
                Cell.CreatePosition(0,1),
            };
            return gliderCoordinates;
        }

        static List<Cell> AddRandomNumberOfRandomlyPositionedCells ()
		{
			var allCoordinates = new List<Cell> ();
			var numberOfLiveCells = random.Next (600,800);
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
