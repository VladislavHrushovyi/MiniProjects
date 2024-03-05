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
        long x = (b * c) / (b - a);
        long iter = b / a;
        while ((x / a) - (x / b) < c)
        {
            x+=iter;
        }
        return x;
    }
}