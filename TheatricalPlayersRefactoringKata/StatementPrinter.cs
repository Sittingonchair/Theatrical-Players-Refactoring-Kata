using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private string CalculateResult(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            StringBuilder result = new();

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var perfAmount = 0;
                switch (play.Type)
                {
                    case "tragedy":
                        perfAmount = 40000;
                        if (perf.Audience > 30)
                        {
                            perfAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        perfAmount = 30000;
                        if (perf.Audience > 20)
                        {
                            perfAmount += 10000 + 500 * (perf.Audience - 20);
                        }
                        perfAmount += 300 * perf.Audience;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                //result.AppendLine(string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)", play.Name, Convert.ToDecimal(perfAmount / 100), perf.Audience));
                totalAmount += perfAmount;
            }

            // todo: return a result
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            StringBuilder result = new();
            result.AppendLine($"Statement for {invoice.Customer}");
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                var perfAmount = 0;
                switch (play.Type) 
                {
                    case "tragedy":
                        perfAmount = 40000;
                        if (perf.Audience > 30) {
                            perfAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        perfAmount = 30000;
                        if (perf.Audience > 20) {
                            perfAmount += 10000 + 500 * (perf.Audience - 20);
                        }
                        perfAmount += 300 * perf.Audience;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

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
