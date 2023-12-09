namespace MayersAlgorithm
{
    using System;
    using System.Collections.Generic;

    class MyersDiff
    {
        static int[,] LCSLength(char[] X, char[] Y)
        {
            int m = X.Length;
            int n = Y.Length;

            int[,] C = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                C[i, 0] = 0;
            }
            for (int j = 0; j <= n; j++)
            {
                C[0, j] = 0;
            }
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (X[i - 1] == Y[j - 1])
                    {
                        C[i, j] = C[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        C[i, j] = Math.Max(C[i, j - 1], C[i - 1, j]);
                    }
                }
            }

            return C;
        }
        static void PrintDiff(int[,] C, char[] X, char[] Y, int i, int j)
        {
            if (i-1 >= 0 && j -1>= 0 && X[i-1] == Y[j-1])
            {
                PrintDiff(C, X, Y, i - 1, j - 1);
                Console.WriteLine("= " + X[i - 1]);
            }
            else if (j > 0 && (i == 0 || C[i, j - 1] >= C[i - 1, j]))
            {
                PrintDiff(C, X, Y, i, j - 1);
                Console.WriteLine("+ " + Y[j-1]);
            }
            else if (i > 0 && (j == 0 || C[i, j - 1] < C[i - 1, j]))
            {
                PrintDiff(C, X, Y, i - 1, j);
                Console.WriteLine("- " + X[i - 1]);
            }
            else
            {
                Console.Write("");
            }
        }
        static void Main()
        {
            char[] X = "BACAAC".ToCharArray();
            char[] Y = "CBCBAB".ToCharArray();

            int[,] result = LCSLength(X, Y);
            
            int m = X.Length;
            int n = Y.Length;
            PrintDiff(result, X, Y, m, n);
        } 
    }

}
