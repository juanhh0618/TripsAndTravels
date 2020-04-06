using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;

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
            Title = "Trip Details";
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
                    "Error",
                    "You must enter an Id Trip.",
                    "Accept");
                return;
            }

            Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(IdTrip))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "The Id Trip must start with three letters indicating the city and end with three numbers indicating the number of trip.",
                    "Accept");
                return;
            }

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetTripAsync(IdTrip, url, "api", "/Trips");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "The trip doesn't exist",
                    //response.Message,
                    "Accept");
                return;
            }

            Trip = (TripResponse)response.Result;
        }
    }
}
