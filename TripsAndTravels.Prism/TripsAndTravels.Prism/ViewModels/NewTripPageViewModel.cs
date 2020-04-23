using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using TripsAndTravels.Common.Helpers;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Prism.Views;

namespace TripsAndTravels.Prism.ViewModels
{
    public class NewTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private TripRequest _trip;
        private DelegateCommand _saveTravel;


        public NewTripPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _trip = new TripRequest();
            Title = "Agregar Nuevo Viaje";
            StartDateTrip = DateTime.Now;
            EndDateTrip = DateTime.Now;
        }

        public DelegateCommand SaveTripCommand => _saveTravel ?? (_saveTravel = new DelegateCommand(SaveNewTrip));
        public string IdTrip { get; set; }
        public string DestinyCity { get; set; }

        public DateTime StartDateTrip { get; set; }

        public DateTime EndDateTrip { get; set; }

        public string Origin { get; set; }
        public string Description { get; set; }
        public TripRequest Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }



        private async void SaveNewTrip()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);


            var tripRequest = new TripRequest
            {
                IdTrip = IdTrip.ToUpper(),
                UserId = Guid.Parse(user.Id),
                DestinyCity = DestinyCity,
                StartDateTrip = StartDateTrip,
                EndDateTrip = EndDateTrip,
                Origin = Origin,
                Description = Description
            };

            var response = await _apiService.AddNewTripAsync(url, "/api", "/Trips", "bearer", token.Token, tripRequest);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Nuevo viaje agregado correctamente", "Aceptar");
            await _navigationService.NavigateAsync(nameof(TripList));


        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(IdTrip))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese el id del viaje", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(DestinyCity))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese una ciudad de destino", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(StartDateTrip.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese la fecha de partida", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(EndDateTrip.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese la fecha de llegada", "Aceptar");
                return false;
            }

            return true;
        }
    }
}