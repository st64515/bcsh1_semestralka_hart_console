namespace ElectricityMeterLibrary;
internal class DatabaseOfTaxes
{
    public int CountOfTaxes => taxes_list.Count;
    private readonly List<Tax> taxes_list = new();

    public Tax this[int index]
    {
        get => taxes_list[index];
    }
    internal void Add(string name, DateOnly dateStart, DateOnly dateEnd, int price, Intervals interval)
    {
        if (dateStart > dateEnd)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        taxes_list.Add(new Tax(name, dateStart, dateEnd, price, interval));
    }
}