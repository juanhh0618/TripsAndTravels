using TripsAndTravels.Prism.Interfaces;
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

        public static string Login => Resource.LogIn;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string LoginError => Resource.LoginError;

        public static string Logout => Resource.Logout;



        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string RegisterAs => Resource.RegisterAs;

        public static string RegisterAsError => Resource.RegisterAsError;

        public static string RegisterAsPlaceHolder => Resource.RegisterAsPlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string User => Resource.User;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string Ok => Resource.Ok;

    }
}
