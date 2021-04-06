using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgoritmosGeneticos
{
    public class Chromosome
    {
        public int[] Solution;
        private int _size;

        public Chromosome(int size)
        {
            _size = size;
            Solution = new int[size];
        }

        public void Creation(Func<int, int[]> creation)
        {
            Solution = creation(_size);
        }

        public decimal Fitness(Func<int[], decimal> fitnessFunction)
        {
            return fitnessFunction(Solution);
        }

        public void Mutate()
        {
            var random = new Random();

            var index = random.Next(0, _size);

            Solution[index] = Solution[index] == 0 ? 1 : 0;
        }

        public Chromosome Reproduction(Chromosome parent2)
        {
            var child = new Chromosome(_size);

            int middle = _size / 2; //5-->2   4-->2

            Array.Copy(this.Solution, child.Solution, middle);
            Array.Copy(parent2.Solution, middle, child.Solution, middle, _size - middle);

            return child;
        }

    }
}
