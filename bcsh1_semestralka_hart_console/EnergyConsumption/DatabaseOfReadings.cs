namespace EnergyConsumptionLibrary;
/// <summary>
/// Třída je databází odečtů. Do třídy jdou uložit pouze takové odečty,
/// které davájí vzhledem k času a stavu hodin smysl.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class DatabaseOfReadings
{
    public int StateOfGauge { get; set; }
    public DateOnly LastReading { get; set; }
    public int Count => readings_list.Count;
    private readonly List<Reading> readings_list = new();

    public DatabaseOfReadings(DateOnly dateOfFirstReading, int stateOfGauge)
    {
        this.Add(dateOfFirstReading, stateOfGauge);
    }

    public Reading this[int index]
    {
        get => readings_list[index];
    }

    /// <summary>
    /// Metoda zajistí přidání validního čtení vzhledem ke čtením minulým.
    /// </summary>
    /// <param name="dateOfReading">Datum odečtu.</param>
    /// <param name="actualStateOfGauge">Stav hodin při odečtu</param>
    internal void Add(DateOnly dateOfReading, int actualStateOfGauge)
    {
        StateOfGauge = actualStateOfGauge;
        LastReading = dateOfReading;
        readings_list.Add(new Reading(StateOfGauge, dateOfReading));
    }
}
