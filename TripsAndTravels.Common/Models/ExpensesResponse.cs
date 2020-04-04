using System;
using System.Collections.Generic;
using System.Text;

namespace TripsAndTravels.Common.Models
{
    public class ExpensesResponse
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public string ExpensesType { get; set; }

    }
}