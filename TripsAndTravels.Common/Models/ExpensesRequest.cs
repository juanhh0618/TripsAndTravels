using System;
using System.Collections.Generic;
using System.Text;

namespace TripsAndTravels.Common.Models
{
    public class ExpensesRequest
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public string ExpensesType { get; set; }
        //public string BillPath { get; set; }
        //public byte[] PictureArray { get; set; }
    }
}
