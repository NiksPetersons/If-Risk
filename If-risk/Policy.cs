using System;
using System.Collections.Generic;

namespace If_risk
{
    public class Policy : IPolicy
    {
        //private string _nameOfInsuredObject;
        //public string NameOfInsuredObject => _nameOfInsuredObject;
        public string NameOfInsuredObject { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTill { get; }
        public decimal Premium { get; }
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