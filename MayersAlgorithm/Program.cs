namespace MayersAlgorithm
{
    class MyersDiff
    {
        static int[,] LCSTable(char[] startWord, char[] endWord)
        {
            int rowCount = endWord.Length;

            int columnCount = startWord.Length;

            int[,] C = new int[rowCount + 1 , columnCount + 1];

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
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
        
        static void PrintDiff(int[,] C, char[] startWord, char[] endWord, int i, int j)
        {
            snakeIndexes[i, j] = true;
            if (i-1 >= 0 && j-1 >= 0 && startWord[j - 1] == endWord[i-1])
            {   
                PrintDiff(C, startWord, endWord, i - 1, j - 1);
                Console.WriteLine("= " + endWord[i - 1]);
            }
            else if (j > 0 && (i == 0 || C[i, j - 1] > C[i - 1, j]))
            {
                PrintDiff(C, startWord, endWord, i, j - 1);
                Console.WriteLine("- " + startWord[j-1]);
            }
            else if (i > 0 && (j == 0 || C[i, j - 1] <= C[i - 1, j]))
            {
                PrintDiff(C, startWord, endWord, i - 1, j);
                Console.WriteLine("+ " + endWord[i - 1]);
            }
            else
            {
                return;
            }
        }
        static bool[,] snakeIndexes;
        static void Main()
        {
            char[] startWord = "BACAAC".ToCharArray();
            char[] endWord = "CBCBAB".ToCharArray();

            int[,] result = LCSTable(startWord, endWord);
            snakeIndexes = new bool[endWord.Length+1, startWord.Length+1];
            
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

            Console.Write("    ");
            for (int k = 0; k < startWord.Length; k++)
            {
                Console.Write(startWord[k] + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < endWord.Length + 1; i++)
            {
                if (i >= 1)
                {
                    Console.Write(endWord[i - 1] + " ");
                }
                else
                {
                    Console.Write("  ");
                }
                for (int j = 0; j < startWord.Length + 1; j++)
                {
                    if (snakeIndexes[i, j])
                    {   
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(result[i, j] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(result[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        } 
    }

}
