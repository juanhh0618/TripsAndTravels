using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TripsAndTravels.Common.Helpers;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Prism.Views;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private List<TripItemViewModel> _trips;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _newTripCommand;

        public TripListViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "My Trips";
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Today;
            LoadTripsAsync();
        }
        public DelegateCommand NewTripCommand => _newTripCommand ?? (_newTripCommand = new DelegateCommand(GoToNewTripPage));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadTripsAsync));

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync()
        {
            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            MyTripsRequest request = new MyTripsRequest
            {
                UserId = user.Id
            };

            Response response = await _apiService.GetMyTrips(url, "api", "/Trips/GetMyTrips", "bearer", token.Token, request);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<TripResponse> trips = (List<TripResponse>)response.Result;
            Trips = trips.Select(t => new TripItemViewModel(_navigationService)
            {
                DestinyCity = t.DestinyCity,
                StartDateTrip = t.StartDateTrip,
                EndDateTrip = t.EndDateTrip,
                Id = t.Id,
                IdTrip = t.IdTrip,
                //TripDetails = t.TripDetails,
                User = t.User
            }).ToList();
        }

        public async void GoToNewTripPage()
        {
            await _navigationService.NavigateAsync(nameof(NewTripPage));
        }
    }
}
