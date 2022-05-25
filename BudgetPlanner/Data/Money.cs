namespace BudgetPlanner.Data
{
    public class Money
    {
        public Money(int value, string marker)
        {
            _value = value;
            CurrencyMarker = marker;
        }

        public Money(double value, string marker)
        {
            _value = (int)(value * 100);
            CurrencyMarker = marker;
        }

        public double Amount => _value / 100d;

        private readonly int _value;

        public string CurrencyMarker { get; }

        public static Money Zero(string marker)
        {
            return new Money(0, marker);
        }

        public override string ToString()
        {
            return $"{Amount:n2} {CurrencyMarker}";
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
}
