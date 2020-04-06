using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripsAndTravels.Common.Models
{
    public class TripDetailsResponse
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string BillPath { get; set; }

        public List<ExpensesResponse> Expenses { get; set; }

        public float? TotalExpenses => Expenses?.Sum(e => e.Value);
    }
}