using System;
using System.Collections.Generic;
using System.Linq;

namespace If_risk
{
    public class PremiumCalculator
    {
        public static decimal CalculatePremium(DateTime validFrom, DateTime validTill, IList<Risk> insuredRisks)
        {
            decimal totalYearlyPremium = insuredRisks.Select(x => x.YearlyPrice).Sum();
            decimal totalMonthlyPremium = totalYearlyPremium / 12;
            int policyDurationInMonths = (validTill.Year - validFrom.Year) * 12 + (validTill.Month - validFrom.Month);
            decimal premiumForTotalDuration = policyDurationInMonths * totalMonthlyPremium;
            return Math.Round(premiumForTotalDuration);
        }
    }
}