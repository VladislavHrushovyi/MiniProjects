namespace AlgorithmTasks;

public class EveningWalk
{
    public void StartTasks()
    {
        var inputArgsString = Console.ReadLine();

        var inputArgs = inputArgsString.Split(" ").Select(long.Parse).ToArray();

        var minSumOfGrivnya = FindLowestAmountOfGrivnya(inputArgs[0], inputArgs[1], inputArgs[2]);

        Console.WriteLine(minSumOfGrivnya);
    }

    private long FindLowestAmountOfGrivnya(long a, long b, long c)
    {
        long x = 1;
        long delta = 0;

        while (true)
        {
            long left = x / a - x / b;
            long right = c;

            if (left - right > c)
            {
                return x;
            }

            if (left - right > delta)
            {
                delta = left - right;
                x++;
            }
            else
            {
                x += delta - (left - right);
            }
        }
    }
}