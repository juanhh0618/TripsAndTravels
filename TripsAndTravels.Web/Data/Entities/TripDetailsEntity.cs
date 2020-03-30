﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripsAndTravels.Common.Enums;

namespace Viajes.Web.Data.Entities
{
    public class TripDetailsEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Origin { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Trip Type")]
        public TripType TripType { get; set; }

        public string Description { get; set; }

        public string ReceiptPath { get; set; }

        public TripEntity Trip { get; set; }
        
        public ICollection<ExpensesEntity> Costs { get; set; }

    }
}