using System;
using System.Collections.Generic;

namespace If_risk
{
    public class Policy : IPolicy
    {

        public string NameOfInsuredObject => throw new NotImplementedException();

        public DateTime ValidFrom => throw new NotImplementedException();

        public DateTime ValidTill => throw new NotImplementedException();

        public decimal Premium => throw new NotImplementedException();

        public IList<Risk> InsuredRisks => throw new NotImplementedException();
    }
}