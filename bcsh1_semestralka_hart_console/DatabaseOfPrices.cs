namespace ElectricityMeterLibrary;
public class DatabaseOfPrices
{
    public DateOnly? DayWithNoPrice { get; set; }
    private readonly List<PriceTag> prices_list = new();


    public PriceTag this[int index]
    {
        get => prices_list[index];
    }

    internal bool ExistsPriceBetween(DateOnly interval_begin, DateOnly interval_end)
    {
        DateOnly now = interval_begin;
        bool found;
        while (now < interval_end)
        {
            found = false;
            foreach (PriceTag priceTag in prices_list)
            {
                if ((now >= priceTag.StartDate.AddDays(-1)) && (now < priceTag.EndDate))
                {
                    now = priceTag.EndDate;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                DayWithNoPrice = now.AddDays(1);
                return false;
            }

        }
        return true;
    }

    internal void Add(DateOnly dateStart, DateOnly dateEnd, int price)
    {
        //Pokud je zadané počáteční datum pozdější než datum konce, vyhoď výjimku.
        if (dateStart > dateEnd)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        //Pokud se zadaná cena překrývá s již existující cenou, vyhoď výjimku.
        if (Overlaps(dateStart, dateEnd))
        {
            throw new InvalidOperationException("Inserted period must not overlap existing ones.");
        }
        prices_list.Add(new PriceTag(dateStart, dateEnd, price));
        prices_list.Sort(new PriceTag.PriceTagIntervalComparer());


    }

    private bool Overlaps(DateOnly newDateStart, DateOnly newDateEnd)
    {
        bool overlaps = false;
        List<PriceTag> copiedPrices_list = new(prices_list);
        copiedPrices_list.Add(new(newDateStart, newDateEnd, 0));
        copiedPrices_list.Sort(new PriceTag.PriceTagIntervalComparer());

        for (int i = 0; i < (prices_list.Count - 1); i++)
        {
            if (prices_list[i].EndDate >= prices_list[i + 1].StartDate)
            {
                overlaps = true;
                break;
            }
        }
        overlaps = false;

        return overlaps;

    }
    
}
