namespace AlgorithmTasks._51_75;

public class RotateList
{
    public void Execute()
    {
        ListNode node5 = new ListNode(5);
        ListNode node4 = new ListNode(4, node5);
        ListNode node3 = new ListNode(3, node4);
        ListNode node2 = new ListNode(2, node3);
        ListNode node1 = new ListNode(1, node2);
        var sol = new Solution();
        var result = sol.RotateRight(node1, 2);

        while (result != null)
        {
            Console.Write(result.val +" -> ");
            result = result.next;
        }
    }
}

partial class Solution {
    public ListNode RotateRight(ListNode head, int k)
    {
        return new ListNode();
    }
}