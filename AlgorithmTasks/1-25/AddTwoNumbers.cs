namespace AlgorithmTasks;

public class AddTwoNumbers
{
    public void Execute()
    {
        var solution = new Solution();
        
        var number1 = new ListNode(9);
        var node1_2 = new ListNode(9);
        number1.next = node1_2;
        var node1_3 = new ListNode(9);
        node1_2.next = node1_3;
        var node1_4 = new ListNode(9);
        node1_3.next = node1_4;
        var node1_5 = new ListNode(9);
        node1_4.next = node1_5;
        var node1_6 = new ListNode(9);
        node1_5.next = node1_6;
        var node1_7 = new ListNode(9);
        node1_6.next = node1_7;
        
        var number2 = new ListNode(9);
        var node2_2 = new ListNode(9);
        number2.next = node2_2;
        var node2_3 = new ListNode(9);
        node2_2.next = node2_3;
        var node2_4 = new ListNode(9);
        node2_3.next = node2_4;
        var result = solution.AddTwoNumbers(number1, number2);

        var last = result;
        while (last != null)
        {
            Console.Write(last.val);
            last = last.next;
        }
    }
}

 public class ListNode {
     public int val;
     public ListNode next;
     public ListNode(int val=0, ListNode next=null) {
         this.val = val;
         this.next = next;
    }
 }
partial class Solution 
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2, int remainder = 0) 
    {
        if(l1 == null && l2 == null && remainder == 0) return null;

        int sum = (l1 != null ? l1.val : 0) + (l2 != null ? l2.val : 0) + remainder;

        remainder = sum / 10;

        return new ListNode(sum % 10, AddTwoNumbers(l1?.next, l2?.next, remainder));
    }
}