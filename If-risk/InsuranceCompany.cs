using If_risk.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace If_risk
{
    public class InsuranceCompany :IInsuranceCompany
    {
        public string Name { get; }
        public IList<Risk> AvailableRisks { get; set; }
        public IList<Policy> PolicyList { get; set; }

        public InsuranceCompany(string name)
        {
            Name = name;
            AvailableRisks = new List<Risk>();
            PolicyList = new List<Policy>();
        }

        public InsuranceCompany(string name, List<Risk> availableRisks)
        {
            Name = name;
            AvailableRisks = availableRisks;
            PolicyList = new List<Policy>();
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            DateTime validTill = validFrom.AddMonths(validMonths);

            if (validFrom >= validTill)
            {
                throw new InvalidDateTimeException("Policy start date cannot be earlier or equal to its end date");
            }

            if (selectedRisks.Intersect(AvailableRisks).Count() != selectedRisks.Count())
            {
                throw new InvalidRisksException();
            }

            var policy = new Policy(nameOfInsuredObject, validFrom, validTill, selectedRisks);
            PolicyList.Add(policy);
            return policy;
        }

        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            if (!PolicyList.Any(x => x.NameOfInsuredObject == nameOfInsuredObject))
            {
                throw new InvalidNameException("Policy with chosen name does not exist");
            }

            if (!AvailableRisks.Contains(risk))
            {
                throw new InvalidRisksException("Chosen risk not supported");
            }

            Policy policyInQuestion = PolicyList.First(x => x.NameOfInsuredObject == nameOfInsuredObject);

            if (policyInQuestion.ValidFrom > validFrom || policyInQuestion.ValidTill < validFrom)
            {
                throw new InvalidDateTimeException("Date outside of the range of policy duration");
            }

            policyInQuestion.InsuredRisks.Add(risk);
            policyInQuestion.Premium = PremiumCalculator.CalculatePremium(policyInQuestion.ValidFrom, 
                policyInQuestion.ValidTill,policyInQuestion.InsuredRisks);
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            if (!PolicyList.Any(x => x.NameOfInsuredObject == nameOfInsuredObject))
            {
                throw new InvalidNameException("Policy with chosen name does not exist");
            }

            if (!PolicyList.Any(x => x.ValidFrom == effectiveDate))
            {
                throw new InvalidDateTimeException("No valid policy under chosen name at that date");
            }

            return PolicyList.First(x => x.NameOfInsuredObject == nameOfInsuredObject
                                          && x.ValidFrom == effectiveDate);
        }

        public Risk AddSupportedRisk(string name, decimal yearlyPrice)
        {
            if (AvailableRisks.Any(x => x.Name == name))
            {
                throw new InvalidRisksException("Risk with given name is already supported");
            }

            Risk newRisk = new Risk(name, yearlyPrice);
            AvailableRisks.Add(newRisk);
            return newRisk;
        }

        public string GetAvailableRisks()
        {
            return string.Join(" | ", AvailableRisks);
        }
    }
}