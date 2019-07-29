using System;
using System.Collections.Generic;

namespace GeneticsEngine
{
    public class Genome
    {
        public Genome(Int64 id)
        {
            ID = id;
        }
        public Genome(Int64 id, List<String> genes)
        {
            ID = id;
            this.Genes = genes;
        }

        //Give birth to two children
        public Genome(Genome Father, Genome Mother, bool XY, Int64 id)
        {
            ID = id;
            this.Genes = new List<String>();
            //Get half the length
            int HalfLength = (int)Math.Round((Father.Genes.Count / 2D),0);

            //If XY is true then spawn first three of father and last three of mother otherwise do the opposite
            if (XY)
            {
                //Father
                for (int i = 0; i < HalfLength; i++)
                {
                    Genes.Add(Father.Genes[i]);
                }
                //Mother
                for (int i = HalfLength; i < Mother.Genes.Count; i++)
                {
                    Genes.Add(Mother.Genes[i]);
                }
            }
            else
            {
                //Mother
                for (int i = 0; i < HalfLength; i++)
                {
                    Genes.Add(Mother.Genes[i]);
                }
                //Father
                for (int i = HalfLength; i < Father.Genes.Count; i++)
                {
                    Genes.Add(Father.Genes[i]);
                }
            }
        }

        public Int64 ID { get; set; }
        public List<String> Genes { get; set; }
        public int TotalValue { get; set; }
        public double SurvivalChance { get; set; }

        public Genome Clone()
        {
            Genome newGenome = new Genome(ID);
            newGenome.Genes = new List<string>();
            newGenome.Genes = Genes;
            newGenome.TotalValue = TotalValue;
            newGenome.SurvivalChance = SurvivalChance;
            return newGenome;
        }

        //Test against goal string
        public int Total(string Goal)
        {
            int _total = 0;
            //If goal string longer than Genes it will never achieve the goal so return zero
            if (Goal.Length > Genes.Count)
                return 0;
            //Compare character by character to judge genome
            for (int i = 0; i < Goal.Length; i++)
            {
                String character = Goal[i].ToString();
                if (character == Genes[i])
                {
                    _total++;
                }
            }
            TotalValue = _total;
            return _total;
        }

        //Square total for better judgement of survival chance
        public double SquaredTotal()
        {
            return ((double)TotalValue * (double)TotalValue);
        }

        //Write out the genes to string
        public string GeneReadout()
        {
            string output = "[";
            foreach (String s in Genes)
            {
                output += s;
            }
            output += " ]";
            return output;
        }

        //Chance to be included
        public void DetermineSurvivalChance(double SummedTotal)
        {
            SurvivalChance = ((double)SquaredTotal() / (double)SummedTotal);
        }

        public void Mutate(List<String> SampleSpace)
        {
            Random rnd = new Random();
            int MutationPoint = rnd.Next(0, Genes.Count);
            //Generate single character
            Genes[MutationPoint] = Generator.GenerateStringList(SampleSpace,1)[0];
        }
    }
}
