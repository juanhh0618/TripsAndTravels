using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Web.Data.Entities;

namespace TripsAndTravels.Web.Helpers
{
    public interface IConverterHelper
    {

        TripResponse ToTripResponse(TripEntity tripEntity);


    }
}