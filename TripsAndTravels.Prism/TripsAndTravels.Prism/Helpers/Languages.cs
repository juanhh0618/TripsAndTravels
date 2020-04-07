﻿using TripsAndTravels.Prism.Interfaces;
using TripsAndTravels.Prism.Resources;
using Xamarin.Forms;

namespace TripsAndTravels.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string IdTripError1 => Resource.IdTripError1;

        public static string IdTripError2 => Resource.IdTripError2;

        public static string TripDetails => Resource.TripDetails;

        public static string IdTrip => Resource.IdTrip;

        public static string IdTripPlaceHolder => Resource.IdTripPlaceHolder;

        public static string CheckIdTrip => Resource.CheckIdTrip;

        public static string StarDateTrip => Resource.StarDateTrip;

        public static string EndDateTrip => Resource.EndDateTrip;

        public static string Employee => Resource.Employee;

        public static string Origin => Resource.Origin;

        public static string Description => Resource.Description;

        public static string Loading => Resource.Loading;

        public static string Home => Resource.Home;

        public static string AddNewTrip => Resource.AddNewTrip;

        public static string SeeTripDetails => Resource.SeeTripDetails;

        public static string ModifyUser => Resource.ModifyUser;

        public static string LogIn => Resource.LogIn;


    }
}