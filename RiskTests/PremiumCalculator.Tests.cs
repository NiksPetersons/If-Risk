using System;
using System.Collections.Generic;
using If_risk;
using Xunit;

namespace RiskTests
{
    public class PremiumCalculator_Tests
    {
        private DateTime _validFrom = new DateTime(2022, 1, 1);
        private DateTime _validTill = new DateTime(2023, 1, 1);
        private IList<Risk> _insuredRisks = new List<Risk>();

        [Theory]
        [InlineData(120, 120, 240)]
        [InlineData(12, 12, 24)]
        [InlineData(1000, 150, 1150)]
        public void ShouldReturnCorrectPremiumWithYearLongPolicy(decimal val1, decimal val2, decimal expected)
        {
            _insuredRisks.Add(new Risk("Fire", val1));
            _insuredRisks.Add(new Risk("Flood", val2));
            decimal premium = PremiumCalculator.CalculatePremium(_validFrom,_validTill,_insuredRisks);
            Assert.Equal(expected,premium);
        }

        [Theory]
        [InlineData(120, 120, 120)]
        [InlineData(12, 12, 12)]
        [InlineData(1000, 150, 575)]
        public void ShouldReturnCorrectPremiumWithSixMonthLongPolicy(decimal val1, decimal val2, decimal expected)
        {
            _validFrom = new DateTime(2022, 1, 1);
            _validTill = new DateTime(2022, 7, 1);
            _insuredRisks.Add(new Risk("Fire", val1));
            _insuredRisks.Add(new Risk("Flood", val2));
            decimal premium = PremiumCalculator.CalculatePremium(_validFrom, _validTill, _insuredRisks);
            Assert.Equal(expected, premium);
        }
    }
}