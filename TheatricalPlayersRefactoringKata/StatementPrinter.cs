using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly PerformanceCalculator _performanceCalculator;
        public StatementPrinter()
        {
            _performanceCalculator = new PerformanceCalculator();
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            StringBuilder result = new();
            result.AppendLine($"Statement for {invoice.Customer}");
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                Play play = plays[perf.PlayID];
                int perfAmount = _performanceCalculator.PerfAmountCalculate(play.Type, perf);

                volumeCredits += _performanceCalculator.CalculateVolumeCredits(perf, play);

                // print line for this order
                result.AppendLine(string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)", play.Name, Convert.ToDecimal(perfAmount / 100), perf.Audience));
                totalAmount += perfAmount;
            }

            result.AppendLine(string.Format(cultureInfo, "Amount owed is {0:C}", Convert.ToDecimal(totalAmount / 100)));
            result.AppendLine($"You earned {volumeCredits} credits");
            return result.ToString();
        }
    }
}
