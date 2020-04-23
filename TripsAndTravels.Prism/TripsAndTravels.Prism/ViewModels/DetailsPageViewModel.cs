using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Prism.Views;

namespace TripsAndTravels.Prism.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TripResponse _trip;
        private TripDetailsResponse _tripDetails;
        private List<ExpensesResponse> _expenses;
        private bool _isRunning;
        private DelegateCommand _checkIdTripCommand;
        private DelegateCommand _newExpenseCommand;

        public DetailsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.TripDetails;
            TripDetails = new TripDetailsResponse();
            Expenses = new List<ExpensesResponse>();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<ExpensesResponse> Expenses
        {
            get => _expenses;
            set => SetProperty(ref _expenses, value);
        }

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public TripDetailsResponse TripDetails
        {
            get => _tripDetails;
            set => SetProperty(ref _tripDetails, value);
        }

        public int TripDetailsId { get; set; }


        public string IdTrip { get; set; }

        public DelegateCommand CheckIdTripCommand => _checkIdTripCommand ?? (_checkIdTripCommand = new DelegateCommand(CheckIdTripAsync));
        public DelegateCommand NewExpenseCommand => _newExpenseCommand ?? (_newExpenseCommand = new DelegateCommand(GoToNewExpensePage));



        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");

            Expenses = Trip.TripDetails[0].Expenses;
        }

        public async void GoToNewExpensePage()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", Trip }
            };

            await _navigationService.NavigateAsync(nameof(NewExpensePage), parameters);
        }
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
                    Languages.Accept);
                return;
            }




            Trip = (TripResponse)response.Result;
            List<TripDetailsResponse> tripDetailsList = Trip.TripDetails;

            if (tripDetailsList.Count > 0)
            {
                TripDetails = tripDetailsList[0];
            }


            /*int a = 1;
            int b = 1;
            int c = 1;*/
        }

    }
}