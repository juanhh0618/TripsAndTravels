using System.Collections.Generic;
using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Prism.Helpers
{
    public static class CombosHelper
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                //new Role { Id = 0, Name = "Administrador" },
                new Role { Id = 1, Name = Languages.Employee }
            };
        }
    }
}
