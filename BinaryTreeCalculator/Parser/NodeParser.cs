using BinaryTreeCalculator.BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.Parser
{
    public class NodeParser
    {
        /// <summary>
        /// Parse string expression into a binary tree
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Root of the binary tree</returns>
        public Node Parse(string expression)
        {
            PostFixExpression postFixExpression = new PostFixExpression();

            List<Token> tokens = ParseExpressionToTokens(expression);

            var postFix = postFixExpression.Apply(tokens);

            Node bTree = BuildTree(postFix);

            return bTree;
        }

        /// <summary>
        /// Build tree to be displayed or for calculation
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private Node BuildTree(IEnumerable<Token> tokens)
        {
            Node node;

            Stack<Node> stack = new Stack<Node>();

            foreach (var token in tokens)
            {
                switch (token.OperatorType)
                {
                    case OperatorEnumClass.OperatorEnum.Number:
                        node = new Node(token.Value);
                        stack.Push(node);
                        break;
                    case OperatorEnumClass.OperatorEnum.Plus:
                    case OperatorEnumClass.OperatorEnum.Minus:
                    case OperatorEnumClass.OperatorEnum.Multiply:
                    case OperatorEnumClass.OperatorEnum.Divide:
                        node = new Node(token.OperatorType);
                        if (stack.Count > 0)
                            node.RightNode = stack.Pop();
                        if (stack.Count > 0)
                            node.LeftNode = stack.Pop();
                        
                        stack.Push(node);
                        break;
                    case OperatorEnumClass.OperatorEnum.UnaryMinus:
                        node = new Node(token.OperatorType);
                        if (stack.Count > 0)
                            node.RightNode = stack.Pop();
                        stack.Push(node);
                        break;
                }
            }
            return stack.Pop();
        }

        /// <summary>
        /// Extracts consecutive digits into a number
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Parse string expression to tokens
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private List<Token> ParseExpressionToTokens(string expression)
        {
            List<Token> tokens = new List<Token>();
            Token token = new Token(0);
            bool ignore = false;

            for (int i = 0; i < expression.Length; i++)
            {
                ignore = false;
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
                        i += num.ToString().Length -1;
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
                        if (Char.IsDigit(expression[i+1]))
                            token = new Token(OperatorEnumClass.OperatorEnum.UnaryMinus);
                        else
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
                        ignore = true;
                        break;
                }
                if (!ignore)
                    tokens.Add(token);
            }
            return tokens;
        }
    }
}
