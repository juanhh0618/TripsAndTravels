using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripsAndTravels.Common.Enums;

namespace Viajes.Web.Data.Entities
{
    public class ExpensesEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public float Value { get; set; }

        public ExpenseType ExpenseType { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreatedDate { get; set; }

        public DateTime CreatedDateLocal => CreatedDate.ToLocalTime();
        public TripDetailsEntity TripDetail { get; set; }


    }
}
