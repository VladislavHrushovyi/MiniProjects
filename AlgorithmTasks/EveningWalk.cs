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

    private static long FindLowestAmountOfGrivnya(long a, long b, long c)
    {
        long sum = a * c;
        while (sum / a - sum / b != c)
        {
            sum += a;
        }

        return sum;
    }
}