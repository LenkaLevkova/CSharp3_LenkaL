namespace ToDoList.Test;

public class UnitTest1
{
    [Fact]
    public void Calculator_NotNull()
    {
        var calculator = new Calculator();
        Assert.NotNull(calculator);
    }

    [Fact]
    public void Divide_WithoutRemainder_Succeeds()
    {
        var calculator = new Calculator();
        Assert.Equal(2, calculator.Divide(10, 5));
    }

    [Fact]
    public void Divide_ByZero_Throws()
    {
        var calculator = new Calculator();
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }
}



public class Calculator
{
    public int Divide(int dividend, int divisor)
    {
        return dividend / divisor;
    }
}
