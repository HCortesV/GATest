using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgoritmosGeneticos
{
    public class WeightProblem
    {
        public Chromosome[] Chromosomes;
        private int _maxWeight;
        private int[] _values;
        private int[] _weights;

        public WeightProblem(int[] values,int[] weights,int maxWeight)
        {
            _values = values;
            _weights = weights;
            _maxWeight = maxWeight;
            Chromosomes = new Chromosome[20];
            for (int i = 0; i < Chromosomes.Length; i++)
            {
                Chromosomes[i] = new Chromosome(values.Length);
                Chromosomes[i].Creation(CreateFunction);
            }
        }

        public void Train()
        {
            Chromosome tmp = new Chromosome(1);
            while (true)
            {
                var tupleSelection = Selection();
                var maxFit = tupleSelection.Item1;

                if (maxFit == null)
                    return;

                tmp.Solution = maxFit.Solution;

                var score = maxFit.Fitness(Fitness);

                Console.WriteLine($"Score: {score} \t Weight:{CalculateWeight(maxFit.Solution)} " +
                    $"\t Value:{CalculateValue(maxFit.Solution)} \t Array:{string.Join("-", maxFit.Solution.Select(s => s.ToString()))}" +
                    $"\t Qty:{tupleSelection.Item2}");
                
                if (maxFit.Fitness(Fitness) == 0)
                    break;                               
            }

            Console.WriteLine($"Final--> Score:{tmp.Fitness(Fitness)} \t Weight:{CalculateWeight(tmp.Solution)} \t Value:{CalculateValue(tmp.Solution)} \t Array:{string.Join("-", tmp.Solution.Select(s => s.ToString()))}");
        }

        private int[] CreateFunction(int size)
        {
            var array = new int[size];

            var random = new Random();

            for (int i = 0; i < size; i++)
                array[i] = random.Next(0, 2);

            return array;
        }

        public Tuple<Chromosome,int> Selection()
        {
            var fitChromosomes = Chromosomes.Where(w => w.Fitness(Fitness) > 0.6m).ToArray();

            if (fitChromosomes.Length == 0)
                return null;

            var listChild = new List<Chromosome>();

            for (int i = 0; i < fitChromosomes.Count(); i++)
            {
                for (int j = i + 1; j < fitChromosomes.Count(); j++)
                {
                    listChild.Add(fitChromosomes[i].Reproduction(fitChromosomes[j]));
                }
            }

            foreach (var child in listChild)
            {
                child.Mutate();
            }

            Chromosomes = listChild.ToArray();

            return Tuple.Create(Chromosomes.OrderByDescending(o => o.Fitness(Fitness)).FirstOrDefault(), Chromosomes.Length);
        }



        private decimal Fitness(int[] solution)
        {
            var weight = CalculateWeight(solution);

            if (weight > _maxWeight)
                return 0;

            var value = CalculateValue(solution);

            if (value == 0)
                return 0;

            return 1 - 1 / (decimal)value;
        }

        private int CalculateWeight(int[] solution)
        {
            var weight = 0;
            for (int i = 0; i < solution.Length; i++)
                weight += solution[i] * _weights[i];

            return weight;
        }


        private int CalculateValue(int[] solution)
        {
            var value = 0;
            for (int i = 0; i < solution.Length; i++)
                value += solution[i] * _values[i];

            return value;
        }
    }
}
