using System;
using If_risk;
using Xunit;

namespace RiskTests
{
    public class UnitTest1
    {
        public InsuranceCompany _sut = new InsuranceCompany("TestCompany");

        //public void RiskTests()
        //{
        //    _sut = new InsuranceCompany("TestCompany");
        //}

        [Fact]
        public void ShouldBeAbleToGetCompanyName()
        {
            Assert.Equal("TestCompany",_sut.Name);
        }
    }
}
