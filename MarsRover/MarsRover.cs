using System;
using System.Collections.Generic;

namespace MarsRover
{
    internal class MarsRover
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            Rover();
        }

        static void WelcomeMessage()
        {
            Console.WriteLine("Booting up Systems...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Systems Online!");
            Console.WriteLine(new string('-', Console.WindowWidth));
        }

        // collects the grid Size
        static (int MaxX, int MaxY) GetGridSize()
        {
            Console.WriteLine("\nEnter grid size (e.g., 5 5):");
            Console.WriteLine("Must have a space inbetween the 2 numbers!");

            while (true)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("No input given! try agian");
                    continue;
                }

                var parts = line.Split(' ');

                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int gridMaxX) &&
                    int.TryParse(parts[1], out int gridMaxY))
                {
                    return (gridMaxX, gridMaxY);
                }

                Console.WriteLine("Invalid format! \nPlease enter two whole numbers like this: 5 5");
            }
        }

        // collects starting position
        static (int X, int Y, char Bearing) GetStartingPos(int gridMaxX, int gridMaxY)
        {
            while (true)
            {
                Console.WriteLine($"- Enter starting position (e.g., 1 2 N). (max grid is: {gridMaxX}, {gridMaxY})");

                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter something.");
                    continue;
                }

                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                {
                    Console.WriteLine("Format should be: X Y D (e.g., 1 2 N)");
                    continue;
                }

                if (!int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
                {
                    Console.WriteLine("X and Y must be numbers.");
                    continue;
                }

                if (x < 0 || x > gridMaxX || y < 0 || y > gridMaxY)
                {
                    Console.WriteLine("X or Y is outside the grid");
                    continue;
                }

                char bearing = char.ToUpper(parts[2][0]);
                if (bearing != 'N' && bearing != 'E' && bearing != 'S' && bearing != 'W')
                {
                    Console.WriteLine("Direction must be N, E, S, or W");
                    continue;
                }

                return (x, y, bearing);
            }
        }

        static void Rover()
        {
            var rovers = new List<Rover>();
            var (gridMaxX, gridMaxY) = GetGridSize();

            bool addAnother = true;
            int roverNum = 1;

            while (addAnother)
            {
                Console.WriteLine($"\n----- Configure Rover #{roverNum} ----");
                var (x, y, bearing) = GetStartingPos(gridMaxX, gridMaxY);

                Console.WriteLine("- Please Enter Instructions for the Rover!");
                var instructions = Console.ReadLine().ToUpper() ?? string.Empty;

                var r = new Rover(x, y, bearing, gridMaxX, gridMaxY);
                r.Controls(instructions);

                rovers.Add(r);

                Console.WriteLine("- Do you want to add another rover? y/n");
                var answer = Console.ReadLine()?.Trim().ToLower();
                addAnother = answer == "y" || answer == "yes";
                roverNum++;
            }

            // Final Positions
            Console.WriteLine("\nAll rover final positions:");
            Console.WriteLine(new string('-', Console.WindowWidth));
            foreach (var rv in rovers)
            {
                Console.WriteLine($"Rover #{roverNum} is @ {rv.X} {rv.Y} {rv.Bearing}");
            }
        }
    }

    public class Rover
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Bearing { get; private set; }

        private readonly int GridMaxX;
        private readonly int GridMaxY;

        public Rover(int x, int y, char bearing, int gridMaxX, int gridMaxY)
        {
            X = x;
            Y = y;
            GridMaxX = gridMaxX;
            GridMaxY = gridMaxY;
            Bearing = bearing;
        }

        public void Controls(string instructions)
        {
            foreach (var c in instructions)
            {
                if (c == 'L') Left();
                else if (c == 'R') Right();
                else if (c == 'M') Move();
            }
        }

        private void Left()
        {
            switch (Bearing)
            {
                case 'N': Bearing = 'W'; break;
                case 'W': Bearing = 'S'; break;
                case 'S': Bearing = 'E'; break;
                case 'E': Bearing = 'N'; break;
            }
        }

        private void Right()
        {
            switch (Bearing)
            {
                case 'N': Bearing = 'E'; break;
                case 'E': Bearing = 'S'; break;
                case 'S': Bearing = 'W'; break;
                case 'W': Bearing = 'N'; break;
            }
        }

        private void Move()
        {
            int tempx = 0, tempy = 0;
            switch (Bearing)
            {
                case 'N': tempy = 1; break;
                case 'E': tempx = 1; break;
                case 'S': tempy = -1; break;
                case 'W': tempx = -1; break;
            }

            int newX = X + tempx;
            int newY = Y + tempy;

            if (newX >= 0 && newX <= GridMaxX && newY >= 0 && newY <= GridMaxY)
            {
                X = newX;
                Y = newY;
            }
        }
    }
}
