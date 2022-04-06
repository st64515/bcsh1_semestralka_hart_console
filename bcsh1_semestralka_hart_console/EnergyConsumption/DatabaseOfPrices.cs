namespace EnergyConsumptionLibrary;

using System.Collections;

/// <summary>
/// Třída je databází cen. Do třídy jdou uložit pouze validní intervaly,
/// které se navzájem nepřekrývají.
/// Implementuje rozhraní IEnumerable.
/// </summary>
public class DatabaseOfPrices : IEnumerable
{
    private readonly List<PriceTag> pricesList = new();
    private EnergyConsumptionValidator validator = new();

    public int Count => pricesList.Count;

    public PriceTag this[int index]
    {
        get => pricesList[index];
    }

    public void Add(DateOnly dateStart, DateOnly dateEnd, double price)
    {
        if (dateStart > dateEnd)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        if (validator.Overlaps(pricesList, dateStart, dateEnd))
        {
            throw new InvalidOperationException("Inserted period must not overlap existing ones.");
        }
        pricesList.Add(new PriceTag(dateStart, dateEnd, price));
        pricesList.Sort(new PriceTag.PriceTagIntervalComparer());
    }

    public IEnumerator GetEnumerator() => pricesList.GetEnumerator();

    

}
