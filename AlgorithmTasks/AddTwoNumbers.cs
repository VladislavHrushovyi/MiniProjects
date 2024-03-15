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
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2, int remainder = 0) 
    {
        if(l1 == null && l2 == null && remainder == 0) return null;

        int sum = (l1 != null ? l1.Value : 0) + (l2 != null ? l2.Value : 0) + remainder;

        remainder = sum / 10;

        return new ListNode(sum % 10, AddTwoNumbers(l1?.Next, l2?.Next, remainder));
    }
}