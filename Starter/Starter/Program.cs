using System;
using System.Text;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running the starter programs: ");

            PrintOddNumber(99);
            ReverseString("Hello");
            IsPowerOf2(2048);
            RepeatString("r", 1005);
            Console.ReadKey();
        }

        #region Specific functions
        //*** : to print odd number, till 99
        static void PrintOddNumber()
        {
            PrintOddNumber(99);
        }

        //*** : to reverse a given string.
        static void ReverseString(string str)
        {
            char[] input = str.ToCharArray();
            Array.Reverse(input);
            Console.WriteLine(new string(input));
        }

        //*** : checks if input is power of 2
        static void IsPowerOf2(int num)
        {
            Console.WriteLine($"Is {num} is power of 2? :{IsPowerOfN( num, 2)}");
        }

        //*** :repeat string, based on input param
        static void RepeatString(string input, int num)
        {
            if (num < 0) Console.WriteLine("repeat param can't be less than 0");

            StringBuilder sb = new StringBuilder();

            while (num > 0)
            {
                sb.Append(input);
                num--;
            }

            Console.WriteLine(sb.ToString());
        }
        #endregion

        #region Generic Functions
        //*** : power of N, generalised function
        static bool IsPowerOfN(int num, int powerOf)
        {
            //***: N power 0 is 1
            if (num == 1)
                return true;

            //***: if not divisible then not power of N;
            else if (num % powerOf != 0 || num == 0)
                return false;

            return IsPowerOfN(num / powerOf, powerOf);
        }

        //*** : to print odd number, till the provided limit
        static void PrintOddNumber(int limit)
        {
            Console.WriteLine($"Odd numbers from 1 to {limit}. Prints one number per line.");

            for (int n = 1; n <= limit; n++)
            {
                if (n % 2 != 0)
                {
                    Console.WriteLine(n.ToString());
                }
            }
        }

        #endregion
    }
}
