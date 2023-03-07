using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class Invoice
    {
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }

        public Invoice(string customer, List<Performance> performance)
        {
            this.Customer = customer;
            this.Performances = performance;
        }

    }
}
