
namespace BinaryTreeUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("(1 + 1) x 2", ExpectedResult=4)]
        [TestCase("((15 ÷ (7 − (1 + 1) ) ) × -3 ) − (2 + (1 + 1))", ExpectedResult = -13)]
        public int TestExpressions(string expression)
        {

            return BinaryTreeCalculator.BinaryTreeCalculator.EvaluateExpression(expression);
        }
    }
}