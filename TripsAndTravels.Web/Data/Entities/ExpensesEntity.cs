using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripsAndTravels.Common.Enums;

namespace TripsAndTravels.Web.Data.Entities
{
    public class ExpensesEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public float Value { get; set; }

        public string ExpenseType { get; set; }

        public string BillPath { get; set; }
        
        public TripDetailsEntity TripDetails { get; set; }


    }
}
