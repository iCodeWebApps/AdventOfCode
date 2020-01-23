using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var x = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            //var y = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

            //var x = "R8,U5,L5,D3";
            //var y = "U7,R6,D4,L4";

            var x = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            var y = "U62,R66,U55,R34,D71,R55,D58,R83";

            var _grid1 = new int[1000, 1000];
            var _grid2 = new int[1000, 1000];

            var origin = _grid1.GetOrigin();

            _grid1.AddRoute(origin, x.AsRoute());
            _grid2.AddRoute(origin, y.AsRoute());

            var intersections = _grid1.GetIntersections(_grid2);

            var intersectionCoordinates = intersections.GetIntersectionCoordinates();

            var totalDistanceAways = new List<int>();

            foreach (var coordinate in intersectionCoordinates)
            {
                var distanceAwayRow = System.Math.Abs(coordinate.Row - origin.Row);
                var distanceAwayColumn = System.Math.Abs(coordinate.Column - origin.Column);

                var totalDistanceAway = distanceAwayRow + distanceAwayColumn;

                totalDistanceAways.Add(totalDistanceAway);
            }

            int shortestDistance = totalDistanceAways.Min();

            Console.WriteLine(shortestDistance);
            Console.ReadKey();

        }
    }    

    public class Step
    {
        public char Direction { get; set; }
        public int Count { get; set; }
    }

    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public static class ExtensionMethods
    {
        public static int[,] GetIntersections(this int[,] grid1, int[,] grid2)
        {
            var result = new int[grid1.GetLength(0), grid1.GetLength(1)];

            for (int i = 0; i < grid1.GetLength(0); i++)
            {
                for (int j = 0; j < grid1.GetLength(1); j++)
                {
                    if (grid1[i, j] > 0 && grid2[i, j] > 0)
                    {
                        result[i, j] = 2;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
            }
            return result;
        }

        public static List<Coordinate> GetIntersectionCoordinates(this int[,] grid)
        {
            var list = new List<Coordinate>();

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    if (grid[row, column] == 2)
                    {
                        list.Add(new Coordinate
                        {
                            Row = row,
                            Column = column
                        });
                    }
                }
            }

            return list;
        }

        public static int[,] AddRoute(this int[,] grid, Coordinate origin, List<Step> steps)
        {
            var position = new Coordinate
            {
                Row = origin.Row,
                Column = origin.Column
            };

            foreach (var step in steps)
            {
                grid.TakeStep(position, step);
            }

            return grid;
        }

        public static int[,] TakeStep(this int[,] grid, Coordinate position, Step step)
        {
            if (step.Count == 0) return grid;

            switch (step.Direction)
            {
                case 'R':
                    ++position.Column; // Walk one square right
                    ++grid[position.Row, position.Column]; // Mark current square
                    break;
                case 'D':
                    ++position.Row; // Walk one square down
                    ++grid[position.Row, position.Column]; // Mark current square
                    break;
                case 'L':
                    --position.Column; // Walk one square left
                    ++grid[position.Row, position.Column]; // Mark current square
                    break;
                case 'U':
                    --position.Row; // Walk one square up
                    ++grid[position.Row, position.Column]; // Mark current square
                    break;
                default:
                    throw new Exception();
            }

            --step.Count;

            grid.TakeStep(position, step);

            return grid;
        }

        public static List<Step> AsRoute(this string route)
        {
            var listOfInstructions = route.Split(',').ToList();

            var steps = new List<Step>();

            foreach (var instruction in listOfInstructions)
            {
                var step = new Step
                {
                    Direction = instruction[0],
                    Count = int.Parse(instruction.ToString().Substring(1))
                };

                steps.Add(step);
            }

            return steps;
        }

        public static Coordinate GetOrigin(this int[,] grid)
        {
            var coordinate = new Coordinate
            {
                Row = grid.GetLength(0) / 2,
                Column = grid.GetLength(1) / 2
            };

            return coordinate;
        }
    }
}