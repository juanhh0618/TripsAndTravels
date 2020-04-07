using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.Helpers;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TripResponse _trip;
        private bool _isRunning;
        private DelegateCommand _checkIdTripCommand;

        public TripDetailsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.TripDetails;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public string IdTrip { get; set; }

        public DelegateCommand CheckIdTripCommand => _checkIdTripCommand ?? (_checkIdTripCommand = new DelegateCommand(CheckIdTripAsync));

        private async void CheckIdTripAsync()
        {
            if (string.IsNullOrEmpty(IdTrip))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IdTripError1,
                    Languages.Accept);

                return;
            }

            Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(IdTrip))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IdTripError2,
                    Languages.Accept);

                return;
            }

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept
);
                return;
            }

            Response response = await _apiService.GetTripAsync(IdTrip, url, "api", "/Trips");
            IsRunning = false;


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The trip doesn't exist",
                    Languages.Accept );
                return;
            }

            Trip = (TripResponse)response.Result;
        }
    }
}
