using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using If_risk;
using Xunit;
using If_risk.Exceptions;

namespace RiskTests
{
    public class Policy_Tests
    {
        private Policy testPolicy;
        private string name = "TestObject";
        private DateTime validFrom = new DateTime(2022, 1, 1);
        private DateTime validTo = new DateTime(2022, 7, 1);
        private IList<Risk> insuredRisks = new List<Risk>();

        public Policy_Tests()
        {
            insuredRisks.Add(new Risk("Fire", 100));
            insuredRisks.Add(new Risk("Flood", 50));
            testPolicy = new Policy(name, validFrom, validTo, insuredRisks);
        }

        [Fact]
        public void Policy_PolicyCreation_ShouldBeAbleToGetParameters()
        {
            testPolicy.NameOfInsuredObject.Should().Be(name);
            testPolicy.ValidFrom.Should().Be(validFrom);
            testPolicy.ValidTill.Should().Be(validTo);
            insuredRisks.Should().BeEquivalentTo(testPolicy.InsuredRisks);
        }

        [Fact]
        public void Policy_PolicyCreation_ShouldBeAbleToGetValidPremium()
        {
            testPolicy.Premium.Should().Be(75);
        }
    }
}