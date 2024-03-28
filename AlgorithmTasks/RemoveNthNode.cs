﻿namespace AlgorithmTasks;

public class RemoveNthNode
{
    public void Execute()
    {
        var sol = new Solution();
        var number1 = new ListNode(1);
        var node1_2 = new ListNode(2);
        number1.Next = node1_2;
        var node1_3 = new ListNode(3);
        node1_2.Next = node1_3;
        var node1_4 = new ListNode(4);
        node1_3.Next = node1_4;
        var node1_5 = new ListNode(5);
        node1_4.Next = node1_5;
        // var node1_6 = new ListNode(9);
        // node1_5.Next = node1_6;
        // var node1_7 = new ListNode(9);
        // node1_6.Next = node1_7;
        var nodeListResult = sol.RemoveNthFromEnd(number1, 2);

        while (nodeListResult != null)
        {
            Console.Write(nodeListResult.Value+ "-");
            nodeListResult = nodeListResult.Next;
        }
    }
}
partial class Solution {
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        if (head.Next == null && n == 1)
        {
            return new ListNode();
        }

        int treeLength = 0;
        var currNode = head;
        while (currNode != null)
        {
            treeLength++;
            currNode = currNode.Next;
        }

        if (treeLength == n)
        {
            head = head.Next;
            return head;
        }
        currNode = head;
        ListNode prevNode = null;
        int indexRemovedNode = treeLength - n;
        int indexer = 0;
        while (true)
        {
            if (indexer == indexRemovedNode)
            {
                prevNode.Next = currNode.Next;
                currNode.Next = null;
                break;
            }
            indexer++;
            prevNode = currNode;
            currNode = currNode.Next;
        }
        return head;
    }
}