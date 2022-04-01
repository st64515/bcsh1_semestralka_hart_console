namespace ElectricityMeterLibrary;
public class PriceTag : IComparable<PriceTag>
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public int Price { get; init; }

    public PriceTag(DateOnly startDate, DateOnly endDate, int price)
    {
        StartDate = startDate;
        EndDate = endDate;
        Price = price;
    }
    public int CompareTo(PriceTag? other) => StartDate.CompareTo(other?.StartDate);

    public class PriceTagIntervalComparer : IComparer<PriceTag>
    {
        public int Compare(PriceTag? x, PriceTag? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}