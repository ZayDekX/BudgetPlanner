namespace BudgetPlanner.Data;

public class Money
{
    public Money(double value)
    {
        _value = (int)(value * 100);
    }

    public double Amount => _value / 100d;

    private readonly int _value;

    public static Money Zero { get; } = new Money(0d);

    public override string ToString()
    {
        return $"{Amount:n2}";
    }

    public int CompareTo(Money spent)
    {
        return _value.CompareTo(spent._value);
    }

    public static implicit operator double(Money money)
    {
        return money.Amount;
    }
}
