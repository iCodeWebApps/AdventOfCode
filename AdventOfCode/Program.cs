using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    static class MyMethods
    {
        static void Day3()
        {
            // Lets say the square can be 100 in all directions, starting at origin
            // The challenge is two part:
            // 1. Take Input and and store line in a 2D array
            // 2. Draw second line on 2D array, marking intersections
            // 3. For each intersection, computer manhattan distance back to origin
            // 4. Return shortest distance

            // R8,U5,L5,D3
            // U7,R6,D4,L4 = distance 6

            // R75,D30,R83,U83,L12,D49,R71,U7,L72
            // U62, R66, U55, R34, D71, R55, D58, R83 = distance 159
        }
    }
}
