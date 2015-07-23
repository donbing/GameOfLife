using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
		static Random random = new Random ();

		static void Main(string[] args)
		{
			Console.Clear();
			foreach (var item in generateLiveCells())
			{
				Console.SetCursorPosition(item[0], item[1]);
				Console.Write("X");
			}

			Console.ReadKey();
		}

        static List<List<int>> generateLiveCells()
        {
            Console.Clear();
            var builder = new StringBuilder();
			builder.AppendLine("Welcome to the Game of Life!")
				.AppendLine("Options")
				.AppendLine("Press 1 for a randomly generated game")
				.AppendLine("Press 2 to enter live cells");
			
            Console.WriteLine(builder);

            var selectedOption = Console.ReadKey();
            if (selectedOption.Key == ConsoleKey.D1)
            {
				return AddRandomNumberOfRandomlyPositionedCells ();
            }
            if (selectedOption.Key == ConsoleKey.D2)
            {
				return GetUserGeneratedCellPositions ();
            }

			return new List<List<int>>();
        }

		static List<List<int>> AddRandomNumberOfRandomlyPositionedCells ()
		{
			var allCoordinates = new List<List<int>> ();
			var numberOfLiveCells = random.Next (40, 50);
			for (var count = 0; count <= numberOfLiveCells; count++) {
				var yCoordinate = random.Next (80);
				var xCoordinate = random.Next (40);
				var coordinatePair = new List<int> {
					yCoordinate,
					xCoordinate
				};
				allCoordinates.Add (coordinatePair);
			}
			return allCoordinates;
		}

		static List<List<int>> GetUserGeneratedCellPositions ()
		{
			var allCoordinates = new List<List<int>>();
			Console.Clear ();
			Console.WriteLine ("Enter co ordinates of cells with X and Y values seperated by a comma, press s to finish!");
			Console.WriteLine ("Press o to return to Options");
			var sNotPressed = true;
			while (sNotPressed) {
				var inputValue = Console.ReadLine ();
				if (inputValue == "o" || inputValue == "O") {
					return generateLiveCells ();
				}
				else
					if (inputValue == "s" || inputValue == "S") {
						sNotPressed = false;
					}
					else {
						var cleansedInput = new string (inputValue.Where (character => char.IsNumber (character) || character == ',').ToArray ());
						var commaSeperatedInput = cleansedInput.Split (',');
						var xCoordinate = int.Parse (commaSeperatedInput [0]);
						var yCoordinate = int.Parse (commaSeperatedInput [1]);
						var coordinatePair = new List<int> {
							xCoordinate,
							yCoordinate
						};
						allCoordinates.Add (coordinatePair);
						return allCoordinates;
					}
			}
			return allCoordinates;
		}
    }
}
