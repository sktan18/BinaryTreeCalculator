using BinaryTreeCalculator.BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.Parser
{
    public class NodeParser
    {
        public void Parse(string expression)
        {
            PostFixExpression postFixExpression = new PostFixExpression();

            List<Token> tokens = ParseExpressionToTokens(expression);

            var postFix = postFixExpression.Apply(tokens);
        }

        private Node CreateNode(OperatorEnumClass.OperatorEnum operand, Stack<Node> stack)
        {
            Node node = new Node(operand);

            node.LeftNode = stack.Pop();
            //node.RightNode = stack.Pop();
            return node;
        }

        private int extractNumber(string s, int position)
        {
            string numberString = String.Empty;

            for (int i = position; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                    numberString += s[i].ToString();
                else
                    break;
            }

            return Int32.Parse(numberString);
        }

        private List<Token> ParseExpressionToTokens(string expression)
        {
            List<Token> tokens = new List<Token>();
            Token token = new Token(0);
            expression = expression.Replace(" ", String.Empty);

            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int num = extractNumber(expression, i);
                        token = new Token(num);
                        break;
                    case '(':
                        token = new Token(OperatorEnumClass.OperatorEnum.OpenParenthesis);
                        break;
                    case ')':
                        token = new Token(OperatorEnumClass.OperatorEnum.CloseParenthesis);
                        break;
                    case '+':
                        token = new Token(OperatorEnumClass.OperatorEnum.Plus);
                        break;
                    case '-':
                        token = new Token(OperatorEnumClass.OperatorEnum.Minus);
                        break;
                    case '*':
                    case 'x':
                        token = new Token(OperatorEnumClass.OperatorEnum.Multiply);
                        break;
                    case '/':
                        token = new Token(OperatorEnumClass.OperatorEnum.Divide);
                        break;
                    default:
                        break;
                }
                tokens.Add(token);
            }
            return tokens;
        }
    }
}
