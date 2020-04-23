using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TripsAndTravels.Common.Models
{
    public class TripRequest
    {
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [RegularExpression(@"^([A-Za-z]{3}\d{3})$", ErrorMessage = "The field {0} must have three characters for the city for example(Medellin - MED, " +
            "Bogota - BOG. And finally three number for the trip. " +
            "If this is your first trip 001,your second trip 002 and this will be the correct way")]
        [Display(Name = "Id Trip")]
        public string IdTrip { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime StartDateTrip { get; set; }

            

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime EndDateTrip { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string DestinyCity { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        public string Description { get; set; }
        public string Origin { get; set; }
    }
}
