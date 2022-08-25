using System;
using System.Collections.Generic;

namespace If_risk
{
    public class InsuranceCompany :IInsuranceCompany
    {
        //private string _name;
        public string Name { get; }
        public IList<Risk> AvailableRisks { get ; set ; }

        public InsuranceCompany(string name)
        {
            Name = name;
        }

        public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        {
            throw new NotImplementedException();
        }

        public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        {
            throw new NotImplementedException();
        }

        public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        {
            throw new NotImplementedException();
        }
    }
}