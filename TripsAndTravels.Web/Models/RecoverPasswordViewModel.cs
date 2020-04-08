using System.ComponentModel.DataAnnotations;

namespace TripsAndTravels.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
