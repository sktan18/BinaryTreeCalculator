using BinaryTreeCalculator.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator
{
    public static class BinaryTreeCalculator
    {
        public static int EvaluateExpression(string expression)
        {
            NodeParser parser = new NodeParser();
            parser.Parse(expression);
            return 0;
        }
    }
}
