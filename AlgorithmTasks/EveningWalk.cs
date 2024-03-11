namespace AlgorithmTasks;

public class EveningWalk
{
    public void StartTasks()
    {
        //var inputArgsString = Console.ReadLine();

        //var inputArgs = inputArgsString.Split(" ").Select(long.Parse).ToArray();

        var case1 = FindLowestAmountOfGrivnya(3, 4, 2);
        Console.WriteLine($"CASE 1: a=3 b=4 c=2 X={case1}");

        var case2 = FindLowestAmountOfGrivnya(600000, 700000, 800000);
        Console.WriteLine($"CASE 2: a=600000 b=700000 c=800000 X={case2}");
        
        var case21 = FindLowestAmountOfGrivnya(1, 999999, 999999);
        Console.WriteLine($"CASE 2: a=600000 b=700000 c=800000 X={case21}");

        var case3 = FindLowestAmountOfGrivnya(999998, 999999, 999999);
        Console.WriteLine($"CASE 3: a=999998 b=999999 c=999999 X={case3}");
    }

    private static long FindLowestAmountOfGrivnya(long a, long b, long c)
    {
        // long minAmountDollars = c;
        // long spendMoney;
        // while (true)
        // {
        //     spendMoney = minAmountDollars * a;
        //     long euros = spendMoney / b;
        //     if (minAmountDollars % euros == c)
        //     {
        //         break;
        //     }
        //
        //     minAmountDollars += minAmountDollars / euros;
        // }
        //
        // return spendMoney;
        var minDollars = c;
        while (true)
        {
            var euros = (minDollars * a) / b;
            if (minDollars - euros == c)
            {
                return minDollars * a;
            }
        
            minDollars += b / a;
        }
    }
    
}