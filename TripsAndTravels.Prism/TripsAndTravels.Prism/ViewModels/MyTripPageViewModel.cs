using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Prism.Views;

namespace TripsAndTravels.Prism.ViewModels
{
    public class MyTravelPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private TripResponse _trip;
        private DelegateCommand _newExpenseCommand;

        public MyTravelPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Detalles del viaje";
        }

        public DelegateCommand NewExpenseCommand => _newExpenseCommand ?? (_newExpenseCommand = new DelegateCommand(GoToNewExpensePage));

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
        }

        public async void GoToNewExpensePage()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", Trip }
            };

            await _navigationService.NavigateAsync(nameof(MyTripPage), parameters);
        }

    }

}