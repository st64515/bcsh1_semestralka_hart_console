namespace EnergyConsumptionLibrary
{
    public class EnergyConsumptionValidator
    {
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Metoda zjistí, zda v dané databázi cen existuje cena pro dny zadané v intervalu.
        /// Při zjištění chyby uloží do vlastnosti Message první nalezené datum bez ceny.
        /// </summary>
        /// <param name="prices">Databáze cen</param>
        /// <param name="intervalBegin">Počátek intervalu</param>
        /// <param name="intervalEnd">Konec intervalu</param>
        /// <returns>True, pokud pro všechny dny v intervalu existuje cena. Jinak false.</returns>
        public bool ExistsPriceBetween(DatabaseOfPrices prices, DateOnly intervalBegin, DateOnly intervalEnd)
        {
            Message = string.Empty;
            DateOnly iteration = intervalBegin;
            bool found;
            while (iteration < intervalEnd)
            {
                found = false;
                foreach (PriceTag priceTag in prices)
                {
                    if ((iteration >= priceTag.StartDate.AddDays(-1)) && (iteration < priceTag.EndDate))
                    {
                        iteration = priceTag.EndDate;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Message = iteration.AddDays(1).ToString();
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Metoda zjistí, zda v listu cen, není již uložená nějaká cena,
        /// která by se překrývala s tou, jež chce uživatel přidat.
        /// </summary>
        /// <param name="pricesList"></param>
        /// <param name="newDateStart"></param>
        /// <param name="newDateEnd"></param>
        /// <returns></returns>
        public bool Overlaps(List<PriceTag> pricesList, DateOnly newDateStart, DateOnly newDateEnd)
        {
            bool overlaps = false;

            List<PriceTag> copiedPrices_list = new(pricesList);

            copiedPrices_list.Add(new(newDateStart, newDateEnd, 0));
            copiedPrices_list.Sort(new PriceTag.PriceTagIntervalComparer());

            for (int i = 0; i < (pricesList.Count - 1); i++)
            {
                if (pricesList[i].EndDate >= pricesList[i + 1].StartDate)
                {
                    overlaps = true;
                    break;
                }
            }
            
            return overlaps;
        
        }
    }
}