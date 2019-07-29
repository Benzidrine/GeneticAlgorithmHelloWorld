using System;
using System.Collections.Generic;
using GeneticsEngine;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        public static string Goal = "Hello";
        private static Int64 GlobalID = 0;
        private static List<String> SampleSpace = Generator.CommonAlphaNumericSampleSpace();

        static void Main(string[] args)
        {
            Console.WriteLine("Genetic Algorithm Example:");
            Console.WriteLine("Attempting to  match goal string");
            Console.WriteLine("Epochs (Integer Value):");
            Int64 Epoches = 0;
            string EpochStr = Console.ReadLine();
            if (Int64.TryParse(EpochStr, out Epoches))
            {
                Console.WriteLine(Epoches + " Epochs");
                Console.WriteLine("Mutation Rate (Decimal Value):");
                double MutationRate = 0;
                string MutationRateStr = Console.ReadLine();
                if (double.TryParse(MutationRateStr, out MutationRate))
                {
                    Console.WriteLine(MutationRate + " Mutation Rate");
                    
                    List<Genome> genomes = new List<Genome>();
                    for (int i = 0; i < Epoches; i++)
                    {
                        //Build list of genomes takign into account any survivors
                        for (int j = genomes.Count; j < 4; j++)
                        {
                            //Make children
                            bool XY = (j % 2 == 0);
                            if (genomes.Count >= 2)
                                genomes.Add(new Genome(genomes[0], genomes[1], XY, GlobalID++));
                            else //Create Initial Parents
                                genomes.Add(new Genome(GlobalID++,Generator.GenerateStringList(SampleSpace,Goal.Length)));
                        }
                        Console.WriteLine("Epoch " + (i + 1).ToString());
                        double SummedTotal = 0;
                        foreach (Genome genome in genomes)
                        { 
                            Console.WriteLine(genome.GeneReadout() + " " + genome.Total(Goal));
                            SummedTotal += genome.SquaredTotal();
                        }
                        genomes = genomes.OrderByDescending(g => g.TotalValue).ToList();
                        List<Genome> SurvivedGenomes = new List<Genome>();
                        SurvivedGenomes.Add(genomes[0].Clone());
                        SurvivedGenomes.Add(genomes[1].Clone());
                        //Report Survivors
                        Console.WriteLine("Survivors:");
                        foreach (Genome genome in SurvivedGenomes)
                        {
                            Console.WriteLine(genome.GeneReadout() + " Total: " + genome.Total(Goal) + " ID: " + genome.ID + " Survival Chance: " + genome.SurvivalChance);
                        }
                        Random rnd = new Random();
                        Double Chance = 0;
                        //Mutate Genomes based on mutation rate
                        foreach (Genome genome in SurvivedGenomes)
                        {
                            Chance = rnd.NextDouble();
                            if (Chance < MutationRate)
                            {
                                genome.Mutate(SampleSpace);
                            }
                        }
                        //Mutate second genome on incest 
                        if (SurvivedGenomes.Count == 2)
                        {
                            if (SurvivedGenomes[0].GeneReadout() ==  SurvivedGenomes[1].GeneReadout())
                            {
                                SurvivedGenomes[1].Mutate(SampleSpace);
                            }
                        }
                        genomes = SurvivedGenomes;
                    }
                }
            }

        }
    }
}
