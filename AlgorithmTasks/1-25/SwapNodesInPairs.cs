namespace AlgorithmTasks;

public class SwapNodesInPairs
{
    public void Execute()
    {
        var sol = new Solution();
        var list1 = new ListNode(1);
        var node1_2 = new ListNode(2);
        list1.next = node1_2;
        var node1_3 = new ListNode(3);
        node1_2.next = node1_3;
        var node1_4 = new ListNode(4);
        node1_3.next = node1_4;
        var node1_5 = new ListNode(5);
        node1_4.next = node1_5;
        var node1_6 = new ListNode(6);
        node1_5.next = node1_6;
        var node1_7 = new ListNode(7);
        node1_6.next = node1_7;

        var result = sol.SwapPairs(list1);

        while (result != null)
        {
            Console.Write($"{result.val} ");
            result = result.next;
        }
    }
}

partial class Solution {
    public ListNode SwapPairs(ListNode head)
    {
        ListNode temp = new ListNode(0, head);
        ListNode prev = temp;
        ListNode curr = temp.next;

        while (curr != null && curr.next != null)
        {
            var first = curr;
            var second = curr.next;

            first.next = second.next;
            second.next = first;
            prev.next = second;

            prev = first;
            curr = prev.next;
        }

        return temp.next;
    }
}