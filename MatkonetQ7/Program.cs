namespace MatkonetQ7;


//And yes before you ask it is me Itai Bergman who wrote all this code and some weird but functunal comments
public static class Program
{
    private const string downUpBadStr = "[-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34]";
    private const string upDownBadStr = "[34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2]";

    private const string downUpGoodStr = "[-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2]";
    private const string upDownGoodStr = "[34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34]";

    public static void Main(string[] args)
    {
        int[] values = { 34, 15, 7, 4, -2 };

        // [34, 15, 7, 4, -2]</code> 
        Node<int> lst1 = values.ToNode();

        // [-2, 4, 7, 15, 34]
        Node<int> lst2 = values.ToNode(reverse: true);

        // [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2]
        Node<int>[] node1 = { lst1, lst2, lst1 };

        // [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34]
        Node<int>[] node2 = { lst2, lst1, lst2 };

        // [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2]
        Node<Node<int>> UpDownBadCase = node1.ToNode();

        // [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34]
        Node<Node<int>> DownUpBadCase = node2.ToNode();

        // [-2, 4, 7, 15, 34] --> [34, 15, 7, 4, -2]
        Node<Node<int>> UpDownGoodCase = new Node<Node<int>>(lst2, new Node<Node<int>>(lst1));

        // [34, 15, 7, 4, -2] --> [-2, 4, 7, 15, 34]
        Node<Node<int>> DownUpGoodCase = new Node<Node<int>>(lst1, new Node<Node<int>>(lst2));

        #region Test IsAscending
        //should return true
        Console.WriteLine($"{nameof(lst1)} --> [{lst1}]: returned {IsAscending(lst1)}");

        //should return false
        Console.WriteLine($"{nameof(lst2)} --> [{lst2}]: returned {IsAscending(lst2)}");
        #endregion

        ///should return false
        #region Print bad cases of IsBalanced test
        Console.WriteLine($"{nameof(DownUpBadCase)}: {{ {downUpBadStr} }} returned " +
                $"{IsBalanced(DownUpBadCase)}");

        Console.WriteLine($"{nameof(UpDownBadCase)}: {{ {upDownBadStr} }} returned " +
            $"{IsBalanced(UpDownBadCase)}");
        #endregion

        ///should return true
        #region Print good cases of IsBalanced test
        Console.WriteLine($"{nameof(DownUpGoodCase)}: {{ {downUpGoodStr} }} returned " +
            $"{IsBalanced(DownUpGoodCase)}");

        Console.WriteLine($"{nameof(UpDownGoodCase)}: {{ {upDownGoodStr} }} returned " +
            $"{IsBalanced(UpDownGoodCase)}");
        #endregion
    }

    public static bool IsAscending(Node<int> lst)
    {
        while (lst.HasNext())
        {
            int val = lst.GetValue();
            Node<int> p = lst.GetNext();
            int sum = 0;
            if (!p.HasNext())
            {
                return true;
            }

            while (p != null)
            {
                sum += p.GetValue();
                p = p.GetNext();
            }
            if (val <= sum)
            {
                return false;
            }
            lst = lst.GetNext();
        }
        return true;
    }
    /// <summary>
    /// <paramref name="flag"/> is used to ask:
    /// loop as long as lst has a next
    /// if we have reached the point where they are either all ascending or not from now
    /// <br></br>
    /// if val is true 
    /// then as long as val is equal to IsAscending then flag will stay false
    /// upon val not being equal to IsAscending then:
    /// flag = true
    /// <br></br>
    /// if val is equal to IsAscending and flag is true it means you had a:
    /// [-2, 4, 7, 15, 34] [34, 15, 7, 4, -2] [-2, 4, 7, 15, 34] or <br></br>
    /// [34, 15, 7, 4, -2] [-2, 4, 7, 15, 34] [34, 15, 7, 4, -2]
    /// therfore you return false;
    /// upon the loop ending (lst doesn't have a next):
    /// return true (you ran out of things to scan and there are no more checks left).
    /// </summary>
    public static bool IsBalanced(Node<Node<int>> lst)
    {
        bool val = IsAscending(lst.GetValue());
        lst = lst.GetNext();
        bool flag = false;
        while (lst != null)
        {
            bool isAscending = IsAscending(lst.GetValue());
            if (flag)
            {
                if (val == isAscending)
                {
                    return false;
                }
            }
            else if (val != isAscending)
            {
                flag = true;
            }
            lst = lst.GetNext();
        }
        return true;
    }
}
public class Node<T>
{
    private T Value;
    private Node<T> Next;
    public void SetValue(T value)
    {
        this.Value = value;
    }
    public T GetValue()
    {
        return this.Value;
    }
    public void SetNext(Node<T> node)
    {
        this.Next = node;
    }
    public Node<T> GetNext()
    {
        return this.Next;
    }

    public bool HasNext() => this.Next != null;
    public override string ToString() => $"{this.Value} --> {(this.Next == null ? "null" : this.Next)}";
    public Node(T value, Node<T> next = null)
    {
        this.Value = value;
        this.Next = next;
    }
    public Node(Node<T> node)
    {
        this.Value = node.Value;
        this.Next = new Node<T>(node.Next);
    }
}
public static class NodeUtils
{
    /// <remarks>
    /// <code>
    /// when in a static class in a static func you can use the this keyword on the first]
    /// param to indicate that you can call it with a val.FuncName(params); instead of FuncName(val, params);
    /// </code>
    /// To learn some more about this cool feature look at:
    /// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    /// </remarks>
    public static Node<T> ToNode<T>(this T[] values, bool reverse = false)
    {
        if (reverse)
        {
            Node<T> node = new Node<T>(values[values.Length - 1]);
            Node<T> p = node;

            for (int i = values.Length - 2; i >= 0; i--, p = p.GetNext())
            {
                p.SetNext(new Node<T>(values[i]));
            }
            return node;
        }
        else
        {
            Node<T> node = new Node<T>(values[0]);
            Node<T> p = node;
            for (int i = 1; i < values.Length; i++, p = p.GetNext())
            {
                p.SetNext(new Node<T>(values[i]));
            }

            return node;
        }
    }

}
