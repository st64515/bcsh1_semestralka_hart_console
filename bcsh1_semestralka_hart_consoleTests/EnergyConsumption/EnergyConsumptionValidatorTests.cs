using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnergyConsumptionLibrary.Tests
{
    [TestClass()]
    public class EnergyConsumptionValidatorTests
    {
        [TestMethod()]
        public void ExistsPriceBetweenTestOnePrice()
        {
            EnergyConsumptionValidator v = new();
            DatabaseOfPrices prices = new();
            DateOnly a, b;

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 12, 31);
            prices.Add(a, b, 10.0);

            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 6, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 6, 1);
            b = new DateOnly(2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));
        }

        [TestMethod()]
        public void ExistsPriceBetweenTestMorePrices()
        {
            EnergyConsumptionValidator v = new();
            DatabaseOfPrices prices = new();
            DateOnly a, b;

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 12, 31);
            prices.Add(a, b, 10.0);

            a = new DateOnly(2001, 1, 1);
            b = new DateOnly(2001, 12, 31);
            prices.Add(a, b, 20.0);

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2001, 1, 1);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 12, 31);
            b = new DateOnly(2001, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 11, 30);
            b = new DateOnly(2001, 2, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2000, 12, 31);
            b = new DateOnly(2000, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2001, 1, 1);
            b = new DateOnly(2001, 1, 1);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2001, 12, 31);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.ExistsPriceBetween(prices, a, b));


            a = new DateOnly(1999, 12, 31);
            b = new DateOnly(2000, 2, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(1999, 12, 31);
            b = new DateOnly(2000, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2001, 11, 1);
            b = new DateOnly(2002, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));

            a = new DateOnly(2001, 12, 31);
            b = new DateOnly(2002, 1, 1);
            Assert.IsFalse(v.ExistsPriceBetween(prices, a, b));


        }

        [TestMethod()]
        public void OverlapsTest()
        {
            EnergyConsumptionValidator v = new();
            List<PriceTag> prices = new();
            DateOnly a, b;

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 12, 31);
            prices.Add(new PriceTag(a, b, 10));
            a = new DateOnly(2001, 1, 1);
            b = new DateOnly(2001, 12, 31);
            prices.Add(new PriceTag(a, b, 10));

            a = new DateOnly(1999, 12, 31);
            b = new DateOnly(1999, 12, 31);
            Assert.IsFalse(v.Overlaps(prices, a, b));

            a = new DateOnly(1999, 1, 1);
            b = new DateOnly(1999, 12, 31);
            Assert.IsFalse(v.Overlaps(prices, a, b));

            a = new DateOnly(2002, 1, 1);
            b = new DateOnly(2002, 1, 1);
            Assert.IsFalse(v.Overlaps(prices, a, b));
            
            a = new DateOnly(2002, 1, 1);
            b = new DateOnly(2002, 12, 31);
            Assert.IsFalse(v.Overlaps(prices, a, b));

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2000, 12, 31);
            Assert.IsTrue(v.Overlaps(prices, a, b));

            a = new DateOnly(2001, 1, 1);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.Overlaps(prices, a, b));

            a = new DateOnly(2000, 1, 1);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.Overlaps(prices, a, b));

            a = new DateOnly(2000, 12, 31);
            b = new DateOnly(2001, 1, 1);
            Assert.IsTrue(v.Overlaps(prices, a, b));

            a = new DateOnly(2001, 12, 31);
            b = new DateOnly(2001, 12, 31);
            Assert.IsTrue(v.Overlaps(prices, a, b));

        }
    }
}