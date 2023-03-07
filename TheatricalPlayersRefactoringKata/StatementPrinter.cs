using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {

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
                int perfAmount = PerfAmountCalculate(play.Type, perf);

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PlayType.Comedy == play.Type)
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }

                // print line for this order
                //result.AppendLine(string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)", play.Name, Convert.ToDecimal(perfAmount / 100), perf.Audience));
                totalAmount += perfAmount;
            }

            result.AppendLine(string.Format(cultureInfo, "Amount owed is {0:C}", Convert.ToDecimal(totalAmount / 100)));
            result.AppendLine($"You earned {volumeCredits} credits");
            return result.ToString();
        }

        private int PerfAmountCalculate(PlayType playType, Performance perf)
        {
            int perfAmount = 0;

            switch (playType)
            {
                case PlayType.Tragedy:
                    perfAmount = 40000;

                    if (perf.Audience > 30)
                    {
                        perfAmount += 1000 * (perf.Audience - 30);
                    }

                    break;
                case PlayType.Comedy:
                    perfAmount = 30000;

                    if (perf.Audience > 20)
                    {
                        perfAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    perfAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + playType);
            }

            return perfAmount;
        }
    }
}
