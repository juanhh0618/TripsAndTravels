using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Web.Data.Entities
{
    public class TripEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Trip Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime StartDateTrip { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Trip End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime EndDateTrip { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "The {0} field must have {1} characters")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string DestinyCity { get; set; }

        public UserEntity User { get; set; }
        public ICollection<TripDetailsEntity> TripDetails { get; set; }

    }
}