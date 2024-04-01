namespace AlgorithmTasks;

public class ReverseNodesInK_Group
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
        // var node1_6 = new ListNode(6);
        // node1_5.next = node1_6;
        // var node1_7 = new ListNode(7);
        // node1_6.next = node1_7;

        var result = sol.ReverseKGroup(list1, 3);

        while (result != null)
        {
            Console.Write($"{result.val} ");
            result = result.next;
        }
    }
}

partial class Solution
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null || k == 1)
        {
            return head;
        }

        if (head.next == null)
        {
            return head;
        }

        int count = 0;
        var current = head;
        while (current != null && count < k)
        {
            count++;
            current = current.next;
        }

        if (count < k)
        {
            return head;
        }
        ListNode prev = null;
        ListNode next = null;
        current = head;
        for (int j = 0; j < k; j++)
        {
            next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }

        head.next = ReverseKGroup(current, k);
        return prev;
    }
}