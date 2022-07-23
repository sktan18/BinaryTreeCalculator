using BinaryTreeCalculator.BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeCalculator.Parser
{
    public class PostFixExpression
    {
        private readonly Stack<Token> _operatorTokenStack;
        private readonly List<Token> _postfixTokens;
        public PostFixExpression()
        {
            _operatorTokenStack = new Stack<Token>();
            _postfixTokens = new List<Token>();
        }

        /// <summary>
        /// Transform list of tokens into postfix order 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>List of tokens in postfix format</returns>
        public IEnumerable<Token> Apply(IEnumerable<Token> tokens)
        {
            bool isPreviousUnary = false;
            foreach (var token in tokens)
            {
                ProcessToken(token, isPreviousUnary);

                if (isPreviousUnary)
                    isPreviousUnary = false;

                if (token.OperatorType == OperatorEnumClass.OperatorEnum.UnaryMinus)
                    isPreviousUnary = true;
                
            }
            return GetResult();
        }

        /// <summary>
        /// Handles token processing depending on operand or operator
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isPreviousUnary"></param>
        private void ProcessToken(Token token, bool isPreviousUnary)
        {
            if (token.OperatorType == BinaryTree.OperatorEnumClass.OperatorEnum.Number)
                StoreOperand(token, isPreviousUnary);
            else
                ProcessOperator(token);
        }

        /// <summary>
        /// Stores operand onto stack. Special handling for unary minus
        /// </summary>
        /// <param name="operandToken"></param>
        /// <param name="isPreviousUnary"></param>
        private void StoreOperand(Token operandToken, bool isPreviousUnary)
        {
            _postfixTokens.Add(operandToken);

            if (isPreviousUnary && _operatorTokenStack.Peek().OperatorType == OperatorEnumClass.OperatorEnum.UnaryMinus)
            {
                _postfixTokens.Add(_operatorTokenStack.Pop());
            }
        }

        /// <summary>
        /// Stores operator onto operator stack
        /// </summary>
        /// <param name="operatorToken"></param>
        private void ProcessOperator(Token operatorToken)
        {
            switch (operatorToken.OperatorType)
            {
                case OperatorEnumClass.OperatorEnum.OpenParenthesis:
                    PushOpeningBracket(operatorToken);
                    break;
                case OperatorEnumClass.OperatorEnum.CloseParenthesis:
                    PushClosingBracket(operatorToken);
                    break;
                default:
                    PushOperator(operatorToken);
                    break;
            }
        }

        /// <summary>
        /// Pushes '(' to operator stack
        /// </summary>
        /// <param name="operatorToken"></param>
        private void PushOpeningBracket(Token operatorToken)
        {
            _operatorTokenStack.Push(operatorToken);
        }

        /// <summary>
        /// Pushes operators on stack to operand stack until '(' is found
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <exception cref="Exception"></exception>
        private void PushClosingBracket(Token operatorToken)
        {
            bool openingBracketFound = false;

            while (_operatorTokenStack.Count > 0)
            {
                var stackOperatorToken = _operatorTokenStack.Pop();
                if (stackOperatorToken.OperatorType == OperatorEnumClass.OperatorEnum.OpenParenthesis)
                {
                    openingBracketFound = true;
                    break;
                }

                _postfixTokens.Add(stackOperatorToken);
            }

            if (!openingBracketFound)
            {
                throw new Exception("An unexpected closing bracket.");
            }
        }

        /// <summary>
        /// Push operator onto operator stack depending on priority of operator
        /// </summary>
        /// <param name="operatorToken"></param>
        private void PushOperator(Token operatorToken)
        {
            var operatorPriority = GetOperatorPriority(operatorToken);

            while (_operatorTokenStack.Count > 0)
            {
                var stackOperatorToken = _operatorTokenStack.Peek();
                if (stackOperatorToken.OperatorType == OperatorEnumClass.OperatorEnum.OpenParenthesis)
                {
                    break;
                }

                var stackOperatorPriority = GetOperatorPriority(stackOperatorToken);
                if (stackOperatorPriority < operatorPriority)
                {
                    break;
                }

                _postfixTokens.Add(_operatorTokenStack.Pop());
            }

            _operatorTokenStack.Push(operatorToken);
        }

        /// <summary>
        /// Determines the priority of operator
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private int GetOperatorPriority(Token operatorToken)
        {
            switch (operatorToken.OperatorType)
            {
                case OperatorEnumClass.OperatorEnum.Plus:
                case OperatorEnumClass.OperatorEnum.Minus:
                    return 1;
                case OperatorEnumClass.OperatorEnum.Multiply:
                case OperatorEnumClass.OperatorEnum.Divide:
                    return 2;
                case OperatorEnumClass.OperatorEnum.UnaryMinus:
                    return 3;
                default:
                    var exMessage = "An unexpected action for the operator: " +
                        $"{operatorToken.OperatorType}.";
                    throw new Exception(exMessage);
            }
        }

        /// <summary>
        /// Generates list of tokens in postfix order
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private List<Token> GetResult()
        {
            while (_operatorTokenStack.Count > 0)
            {
                var token = _operatorTokenStack.Pop();
                if (token.OperatorType == OperatorEnumClass.OperatorEnum.OpenParenthesis)
                {
                    throw new Exception("A redundant opening bracket.");
                }
                _postfixTokens.Add(token);
            }
            return _postfixTokens.ToList();
        }
    }
}
