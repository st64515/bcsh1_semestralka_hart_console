namespace ElectricityMeterLibrary;
public class Reading
{
    public int StateOfGauge { get; init; }
    public DateOnly Date { get; init; }

    public Reading(int stateOfGauge, DateOnly date)
    {
        StateOfGauge = stateOfGauge;
        this.Date = date;
    }
    new public string ToString()
    {
        return ($"{Date}: {StateOfGauge}");
    }
}
