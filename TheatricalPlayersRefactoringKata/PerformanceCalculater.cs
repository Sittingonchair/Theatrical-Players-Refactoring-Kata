using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class PerformanceCalculater
    {
        public int PerfAmountCalculate(PlayType playType, Performance perf)
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

        public int CalculateVolumeCredits(Performance perf, Play play)
        {
            var credits = Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if (PlayType.Comedy == play.Type)
            {
                credits += (int)Math.Floor((decimal)perf.Audience / 5);
            }

            return credits;
        }
    }
}
