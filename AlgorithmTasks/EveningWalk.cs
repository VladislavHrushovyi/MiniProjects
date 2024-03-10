namespace AlgorithmTasks;

public class EveningWalk
{
    public void StartTasks()
    {
        //var inputArgsString = Console.ReadLine();

        //var inputArgs = inputArgsString.Split(" ").Select(long.Parse).ToArray();

        var case1 = FindLowestAmountOfGrivnya(3, 4, 2);
        var case2 = FindLowestAmountOfGrivnya(600000, 700000, 800000);
        var case3 = FindLowestAmountOfGrivnya(999998, 999999, 999999);

        Console.WriteLine($"CASE 1: a=3 b=4 c=2 X={case1}");
        Console.WriteLine($"CASE 2: a=600000 b=700000 c=800000 X={case2}");
        Console.WriteLine($"CASE 3: a=999998 b=999999 c=999999 X={case3}");
    }

    private static long FindLowestAmountOfGrivnya(long a, long b, long c)
    {
        long minAmountDollars = c;
        long spendMoney;
        while (true)
        {
            spendMoney = minAmountDollars * a;
            long euros = spendMoney / b;
            if (minAmountDollars % euros == c)
            {
                break;
            }

            minAmountDollars += a;
        }

        return spendMoney;
    }
}