namespace AlgorithmTasks;

public class AddTwoNumbers
{
    public void Execute()
    {
        var solution = new Solution();
        
        var number1 = new ListNode(9);
        var node1_2 = new ListNode(9);
        number1.Next = node1_2;
        var node1_3 = new ListNode(9);
        node1_2.Next = node1_3;
        var node1_4 = new ListNode(9);
        node1_3.Next = node1_4;
        var node1_5 = new ListNode(9);
        node1_4.Next = node1_5;
        var node1_6 = new ListNode(9);
        node1_5.Next = node1_6;
        var node1_7 = new ListNode(9);
        node1_6.Next = node1_7;
        
        var number2 = new ListNode(9);
        var node2_2 = new ListNode(9);
        number2.Next = node2_2;
        var node2_3 = new ListNode(9);
        node2_2.Next = node2_3;
        var node2_4 = new ListNode(9);
        node2_3.Next = node2_4;
        var result = solution.AddTwoNumbers(number1, number2);

        var last = result;
        while (last != null)
        {
            Console.Write(last.Value);
            last = last.Next;
        }
    }
}

 public class ListNode {
     public int Value;
     public ListNode Next;
     public ListNode(int value=0, ListNode next=null) {
         this.Value = value;
         this.Next = next;
    }
 }
public class Solution 
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) 
    {
        ListNode result = null;
        int sumRemainder = 0;
        while (true)
        {
            if (l1 == null && l2 == null)
            {
                if (sumRemainder != 0)
                {
                    result = AddNode(result, sumRemainder);
                }
                break;
            }
            if (l1 != null && l2 != null)
            {
                int tempNodeSum = l1.Value + l2.Value + sumRemainder;
                sumRemainder = tempNodeSum / 10;
                int value = tempNodeSum > 9 ? tempNodeSum % (sumRemainder * 10) : tempNodeSum;
                result = AddNode(result, value);
            }
            else if(l1 != null)
            {
                int tempNodeSum = l1.Value + sumRemainder;
                sumRemainder = tempNodeSum / 10;
                int value = tempNodeSum > 9 ? tempNodeSum % (sumRemainder * 10) : tempNodeSum;
                result = AddNode(result, value);
            }else if(l2 != null)
            {
                int tempNodeSum = l2.Value + sumRemainder;
                sumRemainder = tempNodeSum / 10;
                int value = tempNodeSum > 9 ? tempNodeSum % (sumRemainder * 10) : tempNodeSum;
                result = AddNode(result, value);
            }

            if (l1 != null)
            {
                if (l1.Next != null)
                {
                    l1 = l1.Next;
                }
                else
                {
                    l1 = null;
                }
            }

            if (l2 != null)
            {
                if (l2.Next != null)
                {
                    l2 = l2.Next;
                }
                else
                {
                    l2 = null;
                }
            }
        }
        return result;
    }

    private ListNode AddNode(ListNode? list, int value)
    {
        var newNode = new ListNode(value);
        var last = list;
        if (last == null)
        {
            last = newNode;
            list = last;
            return list;
        }
        while (last?.Next != null)
        {
            last = last.Next;
        }

        last.Next = newNode;

        return list;
    }
}