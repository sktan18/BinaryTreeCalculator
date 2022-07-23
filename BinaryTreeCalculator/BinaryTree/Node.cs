using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.BinaryTree
{
    public class Node
    {
        public OperatorEnumClass.OperatorEnum Operand { get; set; }
        public int Value { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }

        public Node (int value)
        {
            Value = value;
        }

        public Node (OperatorEnumClass.OperatorEnum operand)
        {
            Operand = operand;
        }
    }
}
