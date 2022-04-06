
using EnergyConsumptionLibrary;



string? option;
Meter meter;

try
{
    meter = new(new DateOnly(2022, 3, 1), 11000);
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

do
{
    PrintMenu();
    option = Console.ReadLine();
    try
    {


        switch (option)
        {

            case "a":
            case "A":
                PridejOdecet();
                break;
            case "b":
            case "B":
                VypisOdecty();
                break;
            case "c":
            case "C":
                VypisPoplaky();
                break;
            case "d":
            case "D":
                VypisKalkulaci();
                break;
            case "e":
            case "E":
                NovaCena();
                break;
            case "f":
            case "F":
                NovyPoplatek();
                break;
            case "g":
            case "G":
                VypisCeny();
                break;
            case "q":
            case "Q":
                Console.WriteLine("Konec Programu.");
                break;
            default:
                Console.WriteLine("Neznámý příkaz.");
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message.ToString());
    }


} while (option != "q" && option != "Q" && option != null);

void VypisCeny()
{
    Console.WriteLine("Vypisuji ceny:");
    Console.WriteLine(meter.PrintPrices());
}

void NovyPoplatek()
{
    Console.WriteLine("Přidání nového poplatku.");
    string? popis;
    int den, mes, rok;
    double cena;
    DateOnly pocatek, konec;
    int interval;
    
    Console.WriteLine("Zadej název poplatku:");
    popis = Console.ReadLine();

    Console.WriteLine("Datum počátku platnosti:");
    Console.WriteLine("\tZadej den");
    int.TryParse(Console.ReadLine(), out den);
    Console.WriteLine("\tZadej měsíc");
    int.TryParse(Console.ReadLine(), out mes);
    Console.WriteLine("\tZadej rok");
    int.TryParse(Console.ReadLine(), out rok);
    pocatek = new(rok, mes, den);

    Console.WriteLine("Datum konce platnosti:");
    Console.WriteLine("\tZadej den");
    int.TryParse(Console.ReadLine(), out den);
    Console.WriteLine("\tZadej měsíc");
    int.TryParse(Console.ReadLine(), out mes);
    Console.WriteLine("\tZadej rok");
    int.TryParse(Console.ReadLine(), out rok);
    konec = new(rok, mes, den);

    Console.WriteLine("Zadej cenu za časovou jednotku:");
    double.TryParse(Console.ReadLine(), out cena);

    Console.WriteLine("Intervaly: ");
    Console.WriteLine("1.....Mesicne");
    Console.WriteLine("2.....za MWh");
    Console.WriteLine("3.....za kWh");
    int.TryParse(Console.ReadLine(), out interval);

    meter.AddTax(popis is not null? popis : "?", pocatek, konec, (int)cena, (Intervals)interval);
}

void NovaCena()
{
    Console.WriteLine("Přidání nové ceny.");
    int den, mes, rok;
    double cena;
    DateOnly pocatek, konec;
    Console.WriteLine("Datum počátku platnosti:");
    Console.WriteLine("\tZadej den");
    int.TryParse(Console.ReadLine(), out den);
    Console.WriteLine("\tZadej měsíc");
    int.TryParse(Console.ReadLine(), out mes);
    Console.WriteLine("\tZadej rok");
    int.TryParse(Console.ReadLine(), out rok);
    pocatek = new(rok, mes, den);

    Console.WriteLine("Datum konce platnosti:");
    Console.WriteLine("\tZadej den");
    int.TryParse(Console.ReadLine(), out den);
    Console.WriteLine("\tZadej měsíc");
    int.TryParse(Console.ReadLine(), out mes);
    Console.WriteLine("\tZadej rok");
    int.TryParse(Console.ReadLine(), out rok);
    konec = new(rok, mes, den);

    Console.WriteLine("Zadej cenu za kWh:");
    double.TryParse(Console.ReadLine(), out cena);

    meter.AddElectricityPrice(pocatek, konec, (int)cena);
}

void VypisKalkulaci()
{
    Console.WriteLine("Vypisuji kalkulaci spotřeby: (bez utraceno a poplatků)");
    Console.WriteLine(meter.PrintCalculation());
}

void VypisPoplaky()
{
    Console.WriteLine("Vypisuji poplatky:");
    Console.WriteLine(meter.PrintTaxes());
}

void VypisOdecty()
{
    int pocet;
    Console.WriteLine("Zadej počet odečtů k vypsání");
    Console.WriteLine("Vypisuji odečty:");
    int.TryParse(Console.ReadLine(), out pocet);
    Console.WriteLine(meter.PrintLastReadings(pocet));
}

void PridejOdecet()
{
    Console.WriteLine("Přidání nového odečtu.");

    int den, mes, rok;
    int stavHodin;
    DateOnly datumOdectu;
    Console.WriteLine("Datum odečtu:");
    Console.WriteLine("\tZadej den");
    int.TryParse(Console.ReadLine(), out den);
    Console.WriteLine("\tZadej měsíc");
    int.TryParse(Console.ReadLine(), out mes);
    Console.WriteLine("\tZadej rok");
    int.TryParse(Console.ReadLine(), out rok);
    datumOdectu = new(rok, mes, den);

    Console.WriteLine("Zadej stav hodin:");

    int.TryParse(Console.ReadLine(), out stavHodin);

    meter.AddReading(datumOdectu, stavHodin);
}

void PrintMenu()
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Kalkulátor ceny energií! :)");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("MENU:");
    Console.WriteLine("[A] - nový odečet...");
    Console.WriteLine("[B] - vypiš odečty...");
    Console.WriteLine("[C] - vypiš poplatky...");
    Console.WriteLine("[D] - vypiš kalkulaci...");
    Console.WriteLine("[E] - nová cena...");
    Console.WriteLine("[F] - nový poplatek...");
    Console.WriteLine("[G] - vypiš ceny...");
    Console.WriteLine("[Q] - ukončit program...");
    Console.ResetColor();
}
