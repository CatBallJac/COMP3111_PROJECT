using System;

using NUnit.Framework;
using HKeInvestWebApplication.Code_File;

namespace HKeInvestWebApplication.Tests
{
    [TestFixture]
    public class HKeInvestCodeTest
    {
        HKeInvestCode myCode = new HKeInvestCode();
        decimal value = Decimal.Parse("200");
        [Test]
        public void ShoudConvertCurrency()
        {
            
            decimal result = myCode.convertCurrency("HKD","1.000" , "EUR", "8.488",value);
            decimal ForcastResult = Decimal.Parse("23.56");
            Assert.That(result, Is.EqualTo(ForcastResult));
        }
        [Test]
        public void convertCurrencyTest2()
        {
            decimal result = myCode.convertCurrency("HKD", "1.000", "GBP", "11.100", value);
            decimal ForcastResult = Decimal.Parse("18.01");
            Assert.AreEqual(result, ForcastResult);
        }
        [Test]
        public void convertCurrencyTest3()
        {
            decimal result = myCode.convertCurrency("HKD", "1.000", "JPY", "0.065", value);
            decimal ForcastResult = Decimal.Parse("3076.92");
            Assert.AreEqual(result, ForcastResult);
        }
        [Test]
        public void convertCurrencyTest4()
        {
            decimal result = myCode.convertCurrency("HKD", "1.000", "USD", "7.791", value);
            decimal ForcastResult = Decimal.Parse("25.67");
            Assert.AreEqual(result, ForcastResult);
        }
    }
}
