namespace AlgorithmTasks;

public class MergeTwoSortedLists
{
    public void Execute()
    {
        var sol = new Solution();
        
        var list1 = new ListNode(1);
        var node1_2 = new ListNode(2);
        list1.next = node1_2;
        var node1_3 = new ListNode(4);
        node1_2.next = node1_3;
        var node1_4 = new ListNode(5);
        node1_3.next = node1_4;
        var node1_5 = new ListNode(7);
        node1_4.next = node1_5;
        var node1_6 = new ListNode(9);
        node1_5.next = node1_6;
        var node1_7 = new ListNode(10);
        node1_6.next = node1_7;
        
        var list2 = new ListNode(1);
        var node2_2 = new ListNode(2);
        list2.next = node2_2;
        var node2_3 = new ListNode(3);
        node2_2.next = node2_3;
        var node2_4 = new ListNode(6);
        node2_3.next = node2_4;

        var result = sol.MergeTwoLists(list1, list2);

        while (result != null)
        {
            Console.Write(result.val);
            result = result.next;
        }
    }
}

partial class Solution {
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null && list2 == null)
        {
            return null;
        }

        if (list1 == null) return list2;
        if (list2 == null) return list1;

        var p1 = list1;
        var p2 = list2;
        if (p1.val >= p2.val)
        {
            p1 = list2;
            p2 = list1;
        }

        var merged = p1;

        while (p1 != null && p2 != null)
        {
            if (p1.next == null)
            {
                p1.next = p2;
                break;
            }

            if (p2.val <= p1.next.val)
            {
                var p1Next = p1.next;
                var p2Next = p2.next;

                p1.next = p2;
                p2.next = p1Next;

                p2 = p2Next;
            }

            p1 = p1.next;
        }
        
        
        return merged;
    }
}