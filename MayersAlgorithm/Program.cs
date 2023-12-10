namespace MayersAlgorithm
{
    class MyersDiff
    {
        static int[,] LCSTable(char[] startWord, char[] endWord)
        {
            int n = endWord.Length;

            int m = startWord.Length;

            int[,] C = new int[n + 1 , m + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (startWord[j - 1] == endWord[i - 1])
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
        static void PrintDiff(int[,] C, char[] startWord, char[] endWord, int lineIndex, int columnIndex)
        {
            if (lineIndex-1 >= 0 && columnIndex-1 >= 0 && startWord[columnIndex - 1] == endWord[lineIndex-1])
            {
                PrintDiff(C, startWord, endWord, lineIndex - 1, columnIndex - 1);
                Console.WriteLine("= " + endWord[lineIndex - 1]);
            }
            else if (columnIndex > 0 && (lineIndex == 0 || C[lineIndex, columnIndex - 1] > C[lineIndex - 1, columnIndex]))
            {
                PrintDiff(C, startWord, endWord, lineIndex, columnIndex - 1);
                Console.WriteLine("- " + startWord[columnIndex-1]);
            }
            else if (lineIndex > 0 && (columnIndex == 0 || C[lineIndex, columnIndex - 1] <= C[lineIndex - 1, columnIndex]))
            {
                PrintDiff(C, startWord, endWord, lineIndex - 1, columnIndex);
                Console.WriteLine("+ " + endWord[lineIndex - 1]);
            }
            else
            {
                Console.Write("");
            }
        }
        static void Main()
        {
            char[] startWord = "BACAAC".ToCharArray();
            char[] endWord = "CBCBAB".ToCharArray();

            int[,] result = LCSTable(startWord, endWord);

            Console.Write("    ");
            for(int k=0; k < startWord.Length; k++)
            {    
               Console.Write(startWord[k] + " ");
            }
            Console.WriteLine();

            for(int i=0;i<endWord.Length+1;i++)
            {
                if (i >= 1)
                {
                    Console.Write(endWord[i-1] + " ");
                }
                else
                {
                    Console.Write("  ");
                }
                for (int j = 0; j < startWord.Length+1; j++)
                {
                    Console.Write(result[i,j]+" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            int n = endWord.Length;
            int m = startWord.Length;
            PrintDiff(result, startWord, endWord, n, m);
        } 
    }

}
