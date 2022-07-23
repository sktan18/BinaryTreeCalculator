using BinaryTreeCalculator.BinaryTree;
using BinaryTreeCalculator.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator
{
    public static class BinaryTreeCalculator
    {
        private static string printout = string.Empty;
        public static int EvaluateExpression(string expression)
        {
            NodeParser parser = new NodeParser();
            Node root = parser.Parse(expression);
            PrintBTree(root);
            return CalculateResult(root);
        }

        private static int CalculateResult(Node root)
        {
            if (root == null)
                return 0;

            int result = 0;

            switch (root.Operand)
            {
                case OperatorEnumClass.OperatorEnum.Plus:
                    result = CalculateResult(root.LeftNode) + CalculateResult(root.RightNode);
                    break;
                case OperatorEnumClass.OperatorEnum.Minus:
                    result = CalculateResult(root.LeftNode) - CalculateResult(root.RightNode);
                    break;
                case OperatorEnumClass.OperatorEnum.Divide:
                    result = CalculateResult(root.LeftNode) / CalculateResult(root.RightNode);
                    break;
                case OperatorEnumClass.OperatorEnum.Multiply:
                    result = CalculateResult(root.LeftNode) * CalculateResult(root.RightNode);
                    break;
                case OperatorEnumClass.OperatorEnum.Number:
                    result = root.Value;
                    break;
            }
            return result;
        }

        private static void PrintBTree(Node root)
        {
            LinkedList<Node> treeLevel = new LinkedList<Node>();
            treeLevel.AddFirst(root);
        
            LinkedList<Node> tempList = new LinkedList<Node>();
            int counter = 0;
            int height = TreeHeight(root) - 1;

            double noElements = (Math.Pow(2, (height + 1)) - 1);

            while(counter <= height)
            {
                Node removed = treeLevel.First();
                treeLevel.RemoveFirst();
                int spaceRequired = 0;
                if (tempList.Count == 0)
                    spaceRequired = (int)(noElements / Math.Pow(2, counter + 1));
                else
                    spaceRequired =  (int)(noElements / Math.Pow(2, counter));
                printSpace(spaceRequired, removed);

                if (removed == null)
                {
                    Node empty = null;
                    tempList.AddLast(empty);
                    tempList.AddLast(empty);
                }
                else
                {
                    tempList.AddLast(removed.LeftNode);
                    tempList.AddLast(removed.RightNode);
                }

                if (treeLevel.Count == 0)
                {
                    Console.WriteLine("\n\n");
                    treeLevel = tempList;
                    tempList = new LinkedList<Node>();
                    counter++;
                }
            }

        }

        private static void printSpace(int n, Node removed)
        {
            for (;n>0; n--)
            {
                Console.Write("\t");
            }
            
            if (removed == null)
                Console.Write(" ");
            else
                Console.Write(removed.ToString());
        }

        private static int TreeHeight(Node node)
        {
            if (node == null)
                return 0;
            return 1 + Math.Max(TreeHeight(node.LeftNode), TreeHeight(node.RightNode));
        }
    }
}
