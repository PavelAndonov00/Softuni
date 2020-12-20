using System;
using System.Collections.Generic;
using System.Linq;

namespace Permutations_With_Repetition
{
    class Program
    {
        // The main function that recursively prints  
        // all repeated permutations of the given string.  
        // It uses data[] to store all permutations one by one 
        static void allLexicographicRecur(String str, char[] data,
                                          int last, int index)
        {
            int length = str.Length;

            // One by one fix all characters at the given index  
            // and recur for the subsequent indexes 
            for (int i = 0; i < length; i++)
            {

                // Fix the ith character at index and if  
                // this is not the last index then  
                // recursively call for higher indexes 
                data[index] = str[i];

                // If this is the last index then print  
                // the string stored in data[] 
                if (index == last)
                    Console.WriteLine(new String(data));
                else
                    allLexicographicRecur(str, data, last,
                                               index + 1);
            }
        }

        // This function sorts input string, allocate memory  
        // for data(needed for allLexicographicRecur()) and calls 
        // allLexicographicRecur() for printing all permutations 
        static void allLexicographic(String str)
        {
            int length = str.Length;

            // Create a temp array that will be used by 
            // allLexicographicRecur() 
            char[] data = new char[length + 1];
            char[] temp = str.ToCharArray();

            // Sort the input string so that we get all  
            // output strings in lexicographically sorted order 
            Array.Sort(temp);
            str = new String(temp);

            // Now print all permutaions 
            allLexicographicRecur(str, data, length - 1, 0);
        }

        // Driver Code 
        public static void Main(String[] args)
        {
            String str = "abb";
            Console.Write("All permutations with " +
                       "repetition of {0} are: \n", str);
            allLexicographic(str);
        }
    }
}
