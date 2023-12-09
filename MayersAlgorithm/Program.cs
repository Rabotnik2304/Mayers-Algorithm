namespace MayersAlgorithm
{
    class MyersDiff
    {
        static void Main()
        {
            string oldText = "ABCABBA";
            string newText = "CBABAC";

            List<string> differences = MyersDiffAlgorithm(oldText, newText);

            foreach (var diff in differences)
            {
                Console.WriteLine(diff);
            }
        }

        static List<string> MyersDiffAlgorithm(string oldText, string newText)
        {
            List<string> differences = new List<string>();
            int[][] v = new int[oldText.Length + newText.Length + 1][];
            int[] path = new int[2 * (oldText.Length + newText.Length) + 2];

            for (int d = 0; d < v.Length; d++)
            {
                v[d] = new int[oldText.Length + 1];
            }

            for (int d = 0; d < v.Length; d++)
            {
                for (int k = -d; k <= d; k += 2)
                {
                    int x, y;
                    bool down = (k == -d || (k != d && v[d - 1][k - 1] < v[d - 1][k + 1]));

                    if (down)
                    {
                        x = v[d - 1][k + 1];
                    }
                    else
                    {
                        x = v[d - 1][k - 1] + 1;
                    }

                    y = x - k;

                    while (x < oldText.Length && y < newText.Length && oldText[x] == newText[y])
                    {
                        x++;
                        y++;
                    }

                    v[d][k] = x;

                    if (x == oldText.Length && y == newText.Length)
                    {
                        Backtrack(differences, path, oldText, newText, v, path.Length - 2);
                        return differences;
                    }
                }

                for (int k = -d; k <= d; k += 2)
                {
                    int x, y;
                    bool down = (k == -d || (k != d && v[d - 1][k - 1] < v[d - 1][k + 1]));

                    if (down)
                    {
                        x = v[d - 1][k + 1];
                    }
                    else
                    {
                        x = v[d - 1][k - 1] + 1;
                    }

                    y = x - k;

                    path[path.Length - 1] = x;
                    path[path.Length - 2] = y;

                    while (x < oldText.Length && y < newText.Length && oldText[x] == newText[y])
                    {
                        x++;
                        y++;
                        path[path.Length - 1]++;
                        path[path.Length - 2]++;
                    }

                    v[d][k] = x;

                    if (x == oldText.Length && y == newText.Length)
                    {
                        Backtrack(differences, path, oldText, newText, v, path.Length - 2);
                        return differences;
                    }
                }
            }

            return differences;
        }

        static void Backtrack(List<string> differences, int[] path, string oldText, string newText, int[][] v, int pathIndex)
        {
            if (pathIndex <= 0)
            {
                return;
            }

            int x = path[pathIndex];
            int y = path[--pathIndex];

            Backtrack(differences, path, oldText, newText, v, pathIndex);

            while (x < oldText.Length && y < newText.Length && oldText[x] == newText[y])
            {
                x++;
                y++;
            }

            if (x < oldText.Length || y < newText.Length)
            {
                differences.Add($"Delete: {oldText.Substring(x, oldText.Length - x)}, Insert: {newText.Substring(y, newText.Length - y)}");
            }
        }
    }
}