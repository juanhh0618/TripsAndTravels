﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TripsAndTravels.Common.Enums;

namespace TripsAndTravels.Web.Data.Entities
{
    public class TripDetailsEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Origin { get; set; }

        public string Description { get; set; }

        //public string BillPath { get; set; }

        public TripEntity Trip { get; set; }
        
        public ICollection<ExpensesEntity> Expenses { get; set; }

        public float? TotalExpenses => Expenses?.Sum(e => e.Value);
    }
}