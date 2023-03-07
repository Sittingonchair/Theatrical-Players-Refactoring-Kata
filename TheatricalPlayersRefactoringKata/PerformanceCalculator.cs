using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class PerformanceCalculator
    {
        private const int TRAGEDY_BASE_AMOUNT = 40000;
        private const int TRAGEDY_AUDIENCE_THRESHOLD = 30;
        private const int TRAGEDY_AUDIENCE_MULTIPLIER = 1000;
        private const int COMEDY_BASE_AMOUNT = 30000;
        private const int COMEDY_AUDIENCE_THRESHOLD = 20;
        private const int COMEDY_AUDIENCE_MULTIPLIER = 500;
        private const int COMEDY_AUDIENCE_BASE_AMOUNT = 10000;
        private const int COMEDY_AUDIENCE_EXTRA_MULTIPLIER = 300;
        public int PerfAmountCalculate(PlayType playType, Performance perf)
        {
            int perfAmount = 0;

            switch (playType)
            {
                case PlayType.Tragedy:
                    perfAmount = TRAGEDY_BASE_AMOUNT;

                    if (perf.Audience > TRAGEDY_AUDIENCE_THRESHOLD)
                    {
                        perfAmount += TRAGEDY_AUDIENCE_MULTIPLIER * (perf.Audience - TRAGEDY_AUDIENCE_THRESHOLD);
                    }

                    break;
                case PlayType.Comedy:
                    perfAmount = COMEDY_BASE_AMOUNT;

                    if (perf.Audience > COMEDY_AUDIENCE_THRESHOLD)
                    {
                        perfAmount += COMEDY_AUDIENCE_BASE_AMOUNT + COMEDY_AUDIENCE_MULTIPLIER * (perf.Audience - COMEDY_AUDIENCE_THRESHOLD);
                    }

                    perfAmount += COMEDY_AUDIENCE_EXTRA_MULTIPLIER * perf.Audience;
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
