using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlg
{
    class Genetic
    {
        private int from = -100;
        private int to = 100;
        private Individ bestFromAll;
        private Individ bestFromThisPopulation;
        private int[] koefs;
        private int numberOfPopulation = 50;
        private int repeats = 0;
        private int maxRepeat = 50;
        private double mutationVar = 0.2;
        private double nuclearBlastMutation = 0.6;

        public Genetic(int[] elems)
        {
            this.koefs = elems;
        }

        private void SetFirstPopulation(List<Individ> population)
        {
            var random = new Random();
            for (int i = 0; i < numberOfPopulation; i++)
            {
                var individ = new Individ(random.Next(from, to),random.Next(from, to),random.Next(from, to),random.Next(from, to), random.Next(from, to));
                population.Add(individ);
                individ.Diff = findDiffWithIndividual(individ);
            }
        }

        public Individ Solve()
        {
            List<Individ> population = new List<Individ>();
            SetFirstPopulation(population);
            population.Sort((x, y) => x.Diff.CompareTo(y.Diff));
            bestFromAll = population[0];
            bestFromThisPopulation = population[0];

            double notNuclear = mutationVar;
            var isNuclearBlast = false;

            for (int i = 0; bestFromThisPopulation.Diff != 0; i++)
            {
                if (repeats > maxRepeat)
                {
                    isNuclearBlast = true;
                    mutationVar = nuclearBlastMutation;
                }

                population = new List<Individ>(ChangeGen(population));
                bestFromThisPopulation = population[0];

                if (bestFromThisPopulation.Diff < bestFromAll.Diff)
                {
                    bestFromAll = bestFromThisPopulation;
                    repeats = 0;
                }
                else
                {
                    repeats++;
                }
                Console.WriteLine("Best from All: " + bestFromAll.Show());
                Console.WriteLine("Best from  gen " + (i + 1) + ": " + bestFromThisPopulation.Show());
                if (isNuclearBlast)
                {
                    mutationVar = notNuclear;
                    repeats = 0;
                }
            }
            return bestFromAll;
        }

        private List<Individ> ChangeGen(List<Individ> population)
        {
            List<Individ> newGen = new List<Individ>();
            List<Individ> popForSelect = new List<Individ>();
            for (int start = 1; start < numberOfPopulation - 1; start += 2)
            {
                popForSelect.Add(population[start - 1]);
            }
            for (int j = 1; j < popForSelect.Count(); j++)
            {
                for (int i = 1; i < popForSelect.Count(); i++)
                {
                    newGen = newGen.Concat(Crossbreeding(popForSelect[0], popForSelect[i])).ToList();
                }
                popForSelect.Remove(popForSelect[0]);
            }
            for (int i = 0; i < newGen.Count(); i++)
            {
                newGen[i] = Mutation(newGen[i]);
            }
            newGen.Sort((x, y) => x.Diff.CompareTo(y.Diff));
            List<Individ> trueNewGen = new List<Individ>();
            for (int i = 0; i < numberOfPopulation; i++)
            {
                trueNewGen.Add(newGen[i]);
            }
            return trueNewGen;
        }

        private List<Individ> Crossbreeding(Individ first, Individ second)
        {
            Individ firstChildThirdGenMut = new Individ(first.U, first.W, second.X, second.Y, second.Z);
            Individ secondChildThirdGenMut = new Individ(second.U, second.W, first.X, first.Y, first.Z);
            Individ thirdChildSecondFourthGenMut = new Individ(first.U, second.W, second.X, first.Y, first.Z);
            Individ fourthChildSecondFourthGenMut = new Individ(second.U, first.W, first.X, second.Y, second.Z);

            firstChildThirdGenMut.Diff = findDiffWithIndividual(firstChildThirdGenMut);
            secondChildThirdGenMut.Diff = findDiffWithIndividual(secondChildThirdGenMut);
            thirdChildSecondFourthGenMut.Diff = findDiffWithIndividual(thirdChildSecondFourthGenMut);
            fourthChildSecondFourthGenMut.Diff = findDiffWithIndividual(fourthChildSecondFourthGenMut);

            List<Individ> result = new List<Individ>();

            result.Add(firstChildThirdGenMut);
            result.Add(secondChildThirdGenMut);
            result.Add(thirdChildSecondFourthGenMut);
            result.Add(fourthChildSecondFourthGenMut);

            return result;
        }

        private Individ Mutation(Individ individ)
        {
            double newKoef = mutationVar * 1000;
            var isSomethingChange = false;
            Random random = new Random();

            individ.U = MutateGen(random, newKoef, isSomethingChange, individ.U);
            individ.W = MutateGen(random, newKoef, isSomethingChange, individ.W);
            individ.X = MutateGen(random, newKoef, isSomethingChange, individ.X);
            individ.Y = MutateGen(random, newKoef, isSomethingChange, individ.Y);
            individ.Z = MutateGen(random, newKoef, isSomethingChange, individ.Z);

            if (isSomethingChange)
            {
                individ.Diff = findDiffWithIndividual(individ);
            }

            return individ;
        }

        private int MutateGen(Random random, double newKoef, bool isSomethingChange, int gen)
        {
            int temp;
            temp = random.Next(1000);
            if (temp <= newKoef)
            {
                isSomethingChange = true;
                gen = random.Next(from, to);
            }
            return gen;
        }

        private int findDiffWithIndividual(Individ individ)
        {
            int resultOfMult = 1;
            int result = 0;
            for (int i = 0; i < 25;)
            {
                resultOfMult *= (int)Math.Pow(individ.U, koefs[i++]);
                resultOfMult *= (int)Math.Pow(individ.W, koefs[i++]);
                resultOfMult *= (int)Math.Pow(individ.X, koefs[i++]);
                resultOfMult *= (int)Math.Pow(individ.Y, koefs[i++]);
                resultOfMult *= (int)Math.Pow(individ.Z, koefs[i++]);
                result += resultOfMult;
                resultOfMult = 1;
            }
            return Math.Abs(result - koefs[25]);
        }

        public class IndividComparerDescending : IComparer<Individ>
        {
            public int Compare(Individ x, Individ y)
            {
                if (x.Diff > y.Diff)
                {
                    return 1;
                }

                if (x.Diff < y.Diff)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
