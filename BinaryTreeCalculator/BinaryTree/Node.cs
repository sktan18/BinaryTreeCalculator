using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.BinaryTree
{
    public class Node
    {
        public OperatorEnumClass.OperatorEnum Operand { get; set; }
        public int Value { get; set; }
        public Node? LeftNode { get; set; }
        public Node? RightNode { get; set; }

        /// <summary>
        /// Constructor for value
        /// </summary>
        /// <param name="value"></param>
        public Node (int value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructor for operator
        /// </summary>
        /// <param name="operand"></param>
        public Node (OperatorEnumClass.OperatorEnum operand)
        {
            Operand = operand;
        }

        /// <summary>
        /// Prints value or operator
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string printString = string.Empty;

            switch (Operand)
            {
                case OperatorEnumClass.OperatorEnum.Number:
                    printString = Value.ToString();
                    break;
                case OperatorEnumClass.OperatorEnum.Plus:
                    printString = "+";
                    break;
                case OperatorEnumClass.OperatorEnum.Minus:
                case OperatorEnumClass.OperatorEnum.UnaryMinus:
                    printString = "-";
                    break;
                case OperatorEnumClass.OperatorEnum.Divide:
                    printString = "/";
                    break;
                case OperatorEnumClass.OperatorEnum.Multiply:
                    printString = "*";
                    break;
            }
            return printString;
        }
    }
}
