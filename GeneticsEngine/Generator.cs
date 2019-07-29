using System;
using System.Collections.Generic;

namespace GeneticsEngine
{
    public class Generator
    {
        public static List<Boolean> GenerateBooleanList(int length)
        {
            List<Boolean> genes = new List<Boolean>();
            for (int i = 0; i < length; i++)
            {
                Random rnd = new Random();
                bool Gene = false;
                int Decision = rnd.Next(0, 2);
                if (Decision == 1) Gene = !Gene;
                genes.Add(Gene);
            }
            return genes;
        }

        //Sample Space is generally a list of characters 
        public static List<string> GenerateStringList(List<string> SampleSpace, int length)
        {
            List<string> genes = new List<string>();
            for (int i = 0; i < length; i++)
            {
                //Add random value from SampleSpace 
                Random rnd = new Random();
                int Decision = rnd.Next(0, SampleSpace.Count);
                genes.Add(SampleSpace[Decision]);
            }
            return genes;
        }

        //Generate a list of lowercase alphabet, capitalized alphabet, numbers and common special characters
        public static List<String> CommonAlphaNumericSampleSpace()
        {
            List<string> alpha = new List<string>();
            for(int i=33; i <=122; i++) {
                alpha.Add(""+(char)i);
            }
            return alpha;
        }
    }
}