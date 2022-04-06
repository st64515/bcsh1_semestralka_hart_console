namespace EnergyConsumptionLibrary;
using System.Text;
public class Meter
{
    private readonly DatabaseOfReadings readings;
    private readonly DatabaseOfPrices prices;
    private readonly DatabaseOfTaxes taxes;

    private EnergyConsumptionValidator validator = new();

    int StateOfGauge => readings.StateOfGauge;
    DateOnly LastReading => readings.LastReading;



    public Meter(DateOnly dateOfFirstReading, int StartingStateOfGauge)
    {
        readings = new(dateOfFirstReading, StartingStateOfGauge);
        prices = new();
        taxes = new();
    }

    

    /// <summary>
    /// Metoda sloužící k přidání nového odečtu na elektroměru.
    /// </summary>
    /// <param name="dateOfReading">Datum odečtu.</param>
    /// <param name="actualStateOfGauge">Stav hodin při odečtu.</param>
    /// <exception cref="ArgumentException">Vyvolá výjimku, pokud zadané parametry nejsou validní nebo není pro zadané období nastavena cena.</exception>
    public void AddReading(DateOnly dateOfReading, int actualStateOfGauge)
    {

        //Pokud je zadaný stav elektroměru nižší než předtím, vyhoď výjimku.
        if (actualStateOfGauge <= StateOfGauge)
        {
            throw new ArgumentException(
                "Inserted state of gauge must be higher than last.");
        }
        //Pokud je zadané datum dřívější než datum posledního odečtu, vyhoď výjimku.
        if (dateOfReading <= LastReading)
        {
            throw new ArgumentException(
                "Inserted date of reading must be higher than last.");
        }
        //Pokud pro zadané období neexistuje cena, vyhoď výjimku
        if (!validator.ExistsPriceBetween(prices, LastReading, dateOfReading))
        {
            throw new ArgumentException(
                $"There is not set any price for some day in a given period." +
                $"There is problem with a price in day {validator.Message}.");
        }
        readings.Add(dateOfReading, actualStateOfGauge);
    }
    /// <summary>
    /// Metoda sloužící k zadání nové ceny do databáze cen.
    /// </summary>
    /// <param name="dateStart">Počáteční datum cenovky.</param>
    /// <param name="dateEnd">Expirační datum cenovky.</param>
    /// <param name="price">Cena za kWh.</param>
    public void AddElectricityPrice(DateOnly dateStart, DateOnly dateEnd, int price)
    {
        prices.Add(dateStart, dateEnd, price);
    }

    internal string PrintPrices()
    {
        StringBuilder sb = new();
        for (int i = 0; i < prices.Count; i++)
        {
            sb.AppendLine(prices[i].ToString());

        }
        return sb.ToString();
    }

    /// <summary>
    /// Metoda sloužící k zadání nového poplatku do databáze poplatků.
    /// </summary>
    /// <param name="name">Název poplatku.</param>
    /// <param name="dateStart">Počáteční datum účinnosti poplatku.</param>
    /// <param name="dateEnd">Expirační datum účinnosti poplatku.</param>
    /// <param name="price">Cena za jednotku.</param>
    /// <param name="interval">Interval účtování poplatku.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddTax(string name, DateOnly dateStart, DateOnly dateEnd, int price, Intervals interval)
    {

        taxes.Add(name, dateStart, dateEnd, price, interval);
    }

    /// <summary>
    /// Metoda sloužící k výpisu posledních n záznamů odečtů.
    /// </summary>
    /// <param name="n">Počet záznamů k výpisu</param>
    /// <returns>String s odečty</returns>
    public string PrintLastReadings(int n)
    {
        StringBuilder sb = new();
        if (n < 0)
        {
            return string.Empty;
        }

        for (int i = readings.Count - 1; i >= 0 ; i--)
        {
            if (i >= n)
            {
                break;
            }
                sb.AppendLine(readings[i].ToString());

        }
        return sb.ToString();
    }
    /// <summary>
    /// Metoda sloužící k výpisu všech poplatků.
    /// </summary>
    /// <returns>String s poplatky</returns>
    public string PrintTaxes()
    {
        StringBuilder sb = new();
        for (int i = 0; i < taxes.Count; i++)
        {
            sb.AppendLine(taxes[i].ToString());

        }
        return sb.ToString();
    }

    public string PrintCalculation()
    {
        StringBuilder sb = new();

        sb.AppendLine("DATUM\t\t|STAV\t|DNI\t|SPOTR.\t|PRUM.\t|UTRAC.\t|POPLATKY");
        
        DateOnly initDay = readings[0].Date;
        int lastGaugeState = readings[0].StateOfGauge;
        DateOnly lastDateOfReading = readings[0].Date;
        sb.AppendLine($"{lastDateOfReading}\t|{lastGaugeState}\t|####\t|####\t|####\t|####\t|####");


        for (int i = 1; i < readings.Count; i++)
        {
            double AVGConsumption = GetAVGConsumption(readings[i].StateOfGauge, lastGaugeState, readings[i].Date, lastDateOfReading);
            sb.AppendLine(
                $"{readings[i].Date}\t|" +
                $"{readings[i].StateOfGauge}\t|" +
                $"{GetDaysBetween(readings[i].Date, lastDateOfReading)}\t|" +
                $"{GetConsumption(readings[i].StateOfGauge,lastGaugeState)}\t|" +
                $"{AVGConsumption}\t|" +
                $"{GetCostBetween(lastDateOfReading, readings[i].Date, AVGConsumption)}\t|" +
                $"####" );
            lastGaugeState = readings[i].StateOfGauge;
            lastDateOfReading = readings[i].Date;

        }
        return sb.ToString();

    }

    private double GetCostBetween(DateOnly lastDateOfReading, DateOnly date, double AVGConsumption)
    {
        //TODO dodelat vypocet na zaklade funkce prices.GetPriceFromDate(aktualni den v danem intervalu)
        return -1;
    }

    private object GetDaysBetween(DateOnly date, DateOnly lastDateOfReading)
    {
        return date.DayNumber - lastDateOfReading.DayNumber;
    }

    private object GetConsumption(int stateOfGauge, int lastGaugeState)
    {
        return (stateOfGauge - lastGaugeState);
    }

    private double GetAVGConsumption(int stateOfGauge, int lastGaugeState, DateOnly date, DateOnly lastDateOfReading)
    {
        return ((stateOfGauge - lastGaugeState) / (date.DayNumber - lastDateOfReading.DayNumber));
    }
}