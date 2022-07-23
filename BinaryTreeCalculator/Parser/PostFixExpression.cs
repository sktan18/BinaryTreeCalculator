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

        public IEnumerable<Token> Apply(IEnumerable<Token> infixNotationTokens)
        {
            foreach (var token in infixNotationTokens)
            {
                ProcessToken(token);
            }
            return GetResult();
        }

        private void ProcessToken(Token token)
        {
            if (token.OperatorType == BinaryTree.OperatorEnumClass.OperatorEnum.Number)
                StoreOperand(token);
            else
                ProcessOperator(token);
        }

        private void StoreOperand(Token operandToken)
        {
            _postfixTokens.Add(operandToken);
        }

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

        private void PushOpeningBracket(Token operatorToken)
        {
            _operatorTokenStack.Push(operatorToken);
        }

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
                default:
                    var exMessage = "An unexpected action for the operator: " +
                        $"{operatorToken.OperatorType}.";
                    throw new Exception(exMessage);
            }
        }

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
