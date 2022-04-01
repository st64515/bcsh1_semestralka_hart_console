namespace ElectricityMeterLibrary;
public class Tax
{
    public Tax(string name, DateOnly dateStart, DateOnly dateEnd, int price, Intervals interval)
    {
        Name = name;
        DateStart = dateStart;
        DateEnd = dateEnd;
        Price = price;
        Interval = interval;
    }

    public string Name { get; }
    public DateOnly DateStart { get; }
    public DateOnly DateEnd { get; }
    public int Price { get; }
    public Intervals Interval { get; }

    new public string ToString()
    {
        return $"{Name}, platny od {DateStart} do {DateEnd}: cena {Price}Kč {Interval}";
    }
}