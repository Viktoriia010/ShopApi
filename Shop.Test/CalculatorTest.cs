using MyCalculator;

namespace Shop.Test
{
    public class CalculatorTest
    {
        [Fact]
        public void SumTest()
        {
            //A - Arrange
            int a = 10, b = 20;
            Calculator calculator = new Calculator();

            //A - Act
            int res = calculator.Sum(a, b);

            //A - Assert
            Assert.Equal(30 , res);
        }
        [Fact]
        public void SubtractionTest()
        {
            //A - Arrange
            int a = 20, b = 10;
            Calculator calculator = new Calculator();

            //A - Act
            int res = calculator.Subtraction(a, b);

            //A - Assert
            Assert.Equal(10 , res);
        }
        [Fact]
        public void PowerTest()
        {
            //A - Arrange
            int a = 3, b = 3;
            Calculator calculator = new Calculator();

            //A - Act
            double res = calculator.Power(a, b);

            //A - Assert
            Assert.Equal(27 , res);
        }

        [Fact]

        public void DivideShouldThrowException()
        {

            //Arrange
            Calculator calculator = new Calculator();

            int a = 10, b = 0;

            // Act
            var exception = Assert.Throws<DivideByZeroException>(

            () => calculator.Divide(a, b)
        
     );


            // Assert
            Assert.Equal("Zero error", exception.Message);

        }
    }
}
