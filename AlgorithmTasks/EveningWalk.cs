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
        Console.WriteLine($"CASE 2.1: a=1 b=700000 c=999999 X={case21}");

        var case3 = FindLowestAmountOfGrivnya(999998, 999999, 999999);
        Console.WriteLine($"CASE 3: a=999998 b=999999 c=999999 X={case3}");
    }

    private static long FindLowestAmountOfGrivnya(long a, long b, long c)
    {
        // var spendMoney = a * c;
        // var tmpSum = spendMoney;
        // while (true)
        // {
        //     var euros = tmpSum / b;
        //     var dollarsResult = euros * a;
        //     tmpSum = tmpSum % b + dollarsResult;
        //     spendMoney += dollarsResult;
        //     if(euros == 0) break;
        // }
        //
        // return spendMoney;
        var dollars = c;
        var euro = dollars * a / b;

        while (dollars - euro != c)
        {
            dollars += 1; // this must be change more another way
            euro = dollars * a / b;
        }
        return dollars * a;
    }
    
}