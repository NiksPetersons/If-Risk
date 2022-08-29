using System;
using System.Collections.Generic;
using FluentAssertions;
using If_risk;
using If_risk.Exceptions;
using Xunit;

namespace RiskTests
{
    public class InsuranceCompany_Tests
    {
        public InsuranceCompany _sut;
        public IList<Risk> insuredRisks;

        public InsuranceCompany_Tests()
        {
            _sut = new InsuranceCompany("TestCompany");
            _sut.AvailableRisks.Add(new Risk("Fire", 100));
            _sut.AvailableRisks.Add(new Risk("Flood", 50));
            _sut.AvailableRisks.Add(new Risk("Storm", 75));
            insuredRisks = new List<Risk>();
            insuredRisks.Add(new Risk("Fire", 100));
            insuredRisks.Add(new Risk("Flood", 50));
        }

        [Fact]
        public void InsuranceCompany_CompanyCreation_ShouldBeAbleToGetCompanyName()
        {
            _sut.Name.Should().Be("TestCompany");
        }

        [Fact]
        public void InsuranceCompany_ShouldBeAbleToGetAvailableRiskListInReadableFormat()
        {
            _sut.GetAvailableRisks().Should().Be("Risk: Fire, Yearly Rate: 100€ |" +
                                                 " Risk: Flood, Yearly Rate: 50€ |" +
                                                 " Risk: Storm, Yearly Rate: 75€");
        }

        [Fact]
        public void InsuranceCompany_SellPolicy_ShouldThrowInvalidDateTimeException()
        {
            var name = "Test Object";
            var validFromDateTime = new DateTime(2022, 01, 01);
            short validMonths = -1;
            Action action = () => _sut.SellPolicy(name, validFromDateTime, validMonths, insuredRisks);
            action.Should().Throw<InvalidDateTimeException>()
                .WithMessage("Policy start date cannot be earlier or equal to its end date");
        }

        [Fact]
        public void InsuranceCompany_SellPolicy_ShouldBeAbleToSellPoliciesSuccessfully()
        {
            
            Action action = () => _sut.SellPolicy("name", new DateTime(2022,01,01),
                6, insuredRisks);
            action.Should().NotThrow();
        }

        [Fact]
        public void InsuranceCompany_SellPolicy_ShouldThrowInvalidRiskException()
        {
            IList<Risk> wrongInsuredRisks = new List<Risk>();
            wrongInsuredRisks.Add(new Risk("FIRE", 100));
            wrongInsuredRisks.Add(new Risk("Flood", 500));
            Action action = () => _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, wrongInsuredRisks);
            action.Should().Throw<InvalidRisksException>().WithMessage("Invalid risk selection");
        }

        [Fact]
        public void InsuranceCompany_FindPolicy_ShouldBeAbleToReturnPolicySuccessfully()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            _sut.GetPolicy("name", new DateTime(2022, 01, 01))
                .Should().BeOfType<Policy>();
        }

        [Fact]
        public void InsuranceCompany_FindPolicy_ShouldThrowInvalidNameException()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action =() => _sut.GetPolicy("NaMe", new DateTime(2022, 01, 01));
            action.Should().Throw<InvalidNameException>()
                .WithMessage("Policy with chosen name does not exist");
        }

        [Fact]
        public void InsuranceCompany_FindPolicy_ShouldThrowInvalidDateTimeException()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action = () => _sut.GetPolicy("name", new DateTime(2022, 02, 01));
            action.Should().Throw<InvalidDateTimeException>()
                .WithMessage("No valid policy under chosen name at that date");
        }

        [Fact]
        public void InsuranceCompany_AddRisk_ShouldThrowInvalidNameException()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action = () => _sut.AddRisk("NaMe", new Risk("Storm", 75),
                new DateTime(2022, 01, 01));
            action.Should().Throw<InvalidNameException>()
                .WithMessage("Policy with chosen name does not exist");
        }

        [Fact]
        public void InsuranceCompany_AddRisk_ShouldThrowInvalidDateTimeException()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action = () => _sut.AddRisk("name", new Risk("Storm", 75),
                new DateTime(2023,01,01));
            action.Should().Throw<InvalidDateTimeException>()
                .WithMessage("Date outside of the range of policy duration");
        }

        [Fact]
        public void InsuranceCompany_AddRisk_ShouldThrowInvalidRisksException()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action = () => _sut.AddRisk("name", new Risk("World War III", 500),
                new DateTime(2022, 03, 01));
            action.Should().Throw<InvalidRisksException>().WithMessage("Chosen risk not supported");
        }

        [Fact]
        public void InsuranceCompany_AddRisk_ShouldBeAbleToAddRiskSuccessfully()
        {
            _sut.SellPolicy("name", new DateTime(2022, 01, 01),
                6, insuredRisks);
            Action action = () => _sut.AddRisk("name", new Risk("Storm", 75),
                new DateTime(2022, 03, 01));
            action.Should().NotThrow();
            _sut.PolicyList[0].InsuredRisks.Count.Should().Be(3);
            _sut.PolicyList[0].Premium.Should().Be(113);
        }

        [Fact]
        public void InsuranceCompany_AddSupportedRisk_ShouldThrowInvalidRiskException()
        {
            Action action = () => _sut.AddSupportedRisk("Storm", 75);
            action.Should().Throw<InvalidRisksException>().WithMessage("Risk with given name is already supported");
        }

        [Fact]
        public void InsuranceCompany_AddSupportedRisk_ShouldBeAbleToAddSupportedRiskSuccessfully()
        {
            Action action = () => _sut.AddSupportedRisk("Sharknado", 250);
            action.Should().NotThrow();
            _sut.AvailableRisks.Count.Should().Be(4);
            _sut.AvailableRisks[3].Name.Should().Be("Sharknado");
        }
    }
}
