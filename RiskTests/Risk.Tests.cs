using If_risk;
using Xunit;

namespace RiskTests
{
    public class Risk_Tests
    {
        private string _name = "Fire";
        private decimal _yearlyPrice = 100;
        private Risk TestRisk;
        //public Risk TestRisk = new Risk("Fire", 100);

        public Risk_Tests()
        {
            TestRisk = new Risk(_name,_yearlyPrice);
        }
    }
}