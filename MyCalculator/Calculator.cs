namespace MyCalculator;

public class Calculator
{
    public int Sum(int a, int b)
    {
        return a + b;
    }
    public int Subtraction(int a, int b)
    {
        return a - b;
    }
    public double Power(int a, int b)
    {
        return Math.Pow(a, b);
    }
    public int Divide(int a, int b)

    {

        if (b == 0)

            throw new DivideByZeroException("Zero error");

        return a / b;

    }
}
