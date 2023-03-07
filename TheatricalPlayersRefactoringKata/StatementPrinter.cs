using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly PerformanceCalculater performanceCalculater;
        public StatementPrinter()
        {
            performanceCalculater = new PerformanceCalculater();
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
                int perfAmount = performanceCalculater.PerfAmountCalculate(play.Type, perf);

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PlayType.Comedy == play.Type)
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }

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
