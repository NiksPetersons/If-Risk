using If_risk.Exceptions;
using System;
using System.Collections.Generic;

namespace If_risk
{
    public class Policy : IPolicy
    {
        public string NameOfInsuredObject { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTill { get; }
        public decimal Premium { get; set; }
        public IList<Risk> InsuredRisks { get; }

        public Policy(string nameOfInsuredObject, DateTime validFrom, DateTime validTill, IList<Risk> insuredRisks) 
        {
            NameOfInsuredObject = nameOfInsuredObject;
            ValidFrom = validFrom;
            ValidTill = validTill;
            InsuredRisks = insuredRisks;
            Premium = PremiumCalculator.CalculatePremium(validFrom, validTill, insuredRisks);
        }
    }
}