﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TripsAndTravels.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }

        public string IdTrip { get; set; }

        public DateTime StartDateTrip { get; set; }

        public DateTime StartDateLocal => StartDateTrip.ToLocalTime();

        public DateTime? EndDateTrip { get; set; }

        public DateTime? EndDateLocal => EndDateTrip?.ToLocalTime();

        public string DestinyCity { get; set; }


        public List<TripDetailsResponse> TripDetails { get; set; }
        public UserResponse User { get; set; }

        


    }
}