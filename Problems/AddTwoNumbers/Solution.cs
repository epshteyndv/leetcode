namespace Problems.AddTwoNumbers
{
    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return SumLists(l1, l2);
        }

        private static ListNode SumListsRecursive(ListNode a, ListNode b, int overflow)
        {
            if (a == null && b == null && overflow == 0)
                return null;

            var sum = (a?.val ?? 0) + (b?.val ?? 0) + overflow;

            return new ListNode(sum % 10) {next = SumListsRecursive(a?.next, b?.next, sum / 10)};
        }

        private static ListNode SumLists(ListNode l1, ListNode l2)
        {
            var a = l1;
            var b = l2;
            var overflow = 0;

            ListNode head = null;
            ListNode current = null;

            while (a != null || b != null || overflow != 0)
            {
                var sum = (a?.val ?? 0) + (b?.val ?? 0) + overflow;

                var newNode = new ListNode(sum % 10);

                if (current != null)
                    current.next = newNode;

                if (head == null)
                    head = newNode;
                
                a = a?.next;
                b = b?.next;
                overflow = sum / 10;
                current = newNode;
            }

            return head;
        }
    }
}