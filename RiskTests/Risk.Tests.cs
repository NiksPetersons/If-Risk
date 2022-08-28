using FluentAssertions;
using If_risk;
using Xunit;

namespace RiskTests
{
    public class Risk_Tests
    {
        private string _name = "Fire";
        private decimal _yearlyPrice = 100;
        private Risk TestRisk;

        public Risk_Tests()
        {
            TestRisk = new Risk(_name,_yearlyPrice);
        }

        [Fact]
        public void Risk_RiskCreation_ShouldBeAbleToGetParameters()
        {
            TestRisk.Name.Should().Be(_name);
            TestRisk.YearlyPrice.Should().Be(_yearlyPrice);
        }
    }
}