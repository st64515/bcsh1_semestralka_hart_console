
using ElectricityMeterLibrary;

Console.WriteLine("Kalkulátor ceny energií! :)");

try
{
    ElectricityMeter meter = new(new DateOnly(2022, 3, 1), 11000);
    meter.AddElectricityPrice(new DateOnly(2020, 1, 1), new DateOnly(2022, 3, 31), 2);
    meter.AddElectricityPrice(new DateOnly(2022, 4, 2), new DateOnly(2023, 12, 31), 10);
    meter.AddElectricityPrice(new DateOnly(2022, 4, 1), new DateOnly(2023, 4, 1), 1);
    meter.AddTax("Platba za jistič", new DateOnly(2020, 1, 1), new DateOnly(2022, 3, 31), 353, Intervals.PerMonth);
    meter.AddReading(new DateOnly(2022, 3, 31), 12000);
    meter.AddReading(new DateOnly(2022, 4, 2), 12200);
    Console.WriteLine("Vypisuji odečty:");
    Console.WriteLine(meter.PrintLastReadings(10));
    Console.WriteLine("Vypisuji poplatky:");
    Console.WriteLine(meter.PrintTaxes());
    Console.WriteLine("Vypisuji kalkulaci spotřeby: (bez utraceno a poplatků)");
    Console.WriteLine(meter.PrintCalculation());
}
catch (Exception e)
{
    Console.WriteLine(e.Message.ToString());
    return;
}
