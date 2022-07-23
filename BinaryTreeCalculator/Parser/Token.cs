using BinaryTreeCalculator.BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.Parser
{
    public class Token
    {
        public int Value { get; set; }
        public OperatorEnumClass.OperatorEnum OperatorType { get; set; }

        public Token (int value)
        {
            Value = value;
            OperatorType = OperatorEnumClass.OperatorEnum.Number;
        }

        public Token (OperatorEnumClass.OperatorEnum type)
        {
            OperatorType = type;
        }

        /// <summary>
        /// Returns if current node is a operand
        /// </summary>
        /// <returns></returns>
        public bool IsOperand()
        {
            return OperatorType == OperatorEnumClass.OperatorEnum.Number;
        }
    }
}
