using System;

namespace AlgoritmosGeneticos
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = new int[] { 100, 5, 6, 10, 2, 5, 1 };
            int[] weights = new int[] { 23, 2, 1, 5, 1, 2, 40 };
            WeightProblem wp = new WeightProblem(values, weights, 23);

            wp.Train();
        }
    }
}
