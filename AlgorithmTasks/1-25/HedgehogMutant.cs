namespace AlgorithmTasks;

public class HedgehogMutant
{
    public void Execute()
    {
        var toColorMutation = 1;
        var hedgehogs = new int[] { 2, 0, 1 };
        var result = DoCalculate(hedgehogs, toColorMutation);
        Console.Write(result);
    }

    private int DoCalculate(int[] hedgehogs, int toColor)
    {
        if (hedgehogs.Count(x => x == 0) > 1)
        {
            return -1;
        }

        int amountMutations = 0;
        var anotherHeg = new List<int>();
        for (int i = 0; i < hedgehogs.Length; i++)
        {
            if (toColor != i)
            {
                anotherHeg.Add(hedgehogs[i]);
            }
        }

        if (anotherHeg.Any(x => x != 0))
        {
            var defineMutation = anotherHeg.Min();
            amountMutations += defineMutation;

            var afterMutation = anotherHeg.Max() - anotherHeg.Min();
            if (afterMutation != 0)
            {
                if (afterMutation % 2 == 0)
                {
                    afterMutation += afterMutation;
                }
                else
                {
                    amountMutations += afterMutation + 2;
                }
            }
        }
        else
        {
            var avHegs = anotherHeg.Max();
            amountMutations = avHegs % 2 == 0 ? avHegs : avHegs + 2;
        }

        return amountMutations;
    }
}