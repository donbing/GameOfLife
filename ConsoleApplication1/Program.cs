using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
		static Random random = new Random ();
		static int width = 80, height = 40;

		static void Main(string[] args)
		{
			foreach (var item in generateLiveCells())
			{
				Console.SetCursorPosition(item[0], item[1]);
				Console.Write("X");
			}

			Console.ReadKey();
		}

        static List<List<int>> generateLiveCells()
        {
			var selectedOption = GetMainMenuUserSelection ();

            if (selectedOption.Key == ConsoleKey.D1)
				return AddRandomNumberOfRandomlyPositionedCells ();
            
            if (selectedOption.Key == ConsoleKey.D2)
				return GetUserGeneratedCellPositions ();

			return new List<List<int>>();
        }

		static ConsoleKeyInfo GetMainMenuUserSelection ()
		{
			Console.Clear ();
			var builder = new StringBuilder ();
			builder.AppendLine ("Welcome to the Game of Life!")
				.AppendLine ("Options")
				.AppendLine ("Press 1 for a randomly generated game")
				.AppendLine ("Press 2 to enter live cells");
			
			Console.WriteLine (builder);

			return Console.ReadKey ();
		}

		static List<List<int>> AddRandomNumberOfRandomlyPositionedCells ()
		{
			var allCoordinates = new List<List<int>> ();
			var numberOfLiveCells = random.Next (40, 50);
			for (var count = 0; count <= numberOfLiveCells; count++) {
				var coordinatePair = CreatePosition (random.Next (width), random.Next (height));
				allCoordinates.Add (coordinatePair);
			}
			return allCoordinates;
		}

		static List<List<int>> GetUserGeneratedCellPositions ()
		{
			var allCoordinates = new List<List<int>>();
			Console.Clear ();
			Console.WriteLine ("Enter coordinates of cells with X and Y values seperated by a comma, press s to finish!");
			Console.WriteLine ("Press o to return to Options");

			var sNotPressed = true;
			while (sNotPressed) {
				var inputValue = Console.ReadLine ();

				switch (inputValue.ToLower()) {
					case "o":
						return generateLiveCells ();
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

		static List<int> CreatePosition (int yCoordinate, int xCoordinate)
		{
			return new List<int> {yCoordinate,xCoordinate};
		}

		static List<int> CreatePositionFromCrappyUserKeyedInput (string inputValue)
		{
			var commaSeperatedInput = NumbersOrCommasOnly (inputValue).Split (',');
			return CreatePosition (int.Parse (commaSeperatedInput [0]), int.Parse (commaSeperatedInput [1]));
		}

		static string NumbersOrCommasOnly (string inputValue)
		{
			return new string (inputValue.Where (character => char.IsNumber (character) || character == ',').ToArray ());
		}
    }
}
