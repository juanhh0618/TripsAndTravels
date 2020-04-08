using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TripsAndTravels.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboRoles();
    }
}
