using Prism.Commands;
using Prism.Navigation;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Prism.Views;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripItemViewModel : TripResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTripCommand;
        private DelegateCommand _selectTrip2Command;
        private DelegateCommand _selectIdTripCommand;

        public TripItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTripCommand => _selectTripCommand ?? (_selectTripCommand = new DelegateCommand(SelectTripAsync));

        public DelegateCommand SelectTrip2Command => _selectTrip2Command ?? (_selectTrip2Command = new DelegateCommand(SelectTrip2Async));

        public DelegateCommand SelectIdTripCommand => _selectIdTripCommand ?? (_selectIdTripCommand = new DelegateCommand(NavigateToNewExpense));

        private async void NavigateToNewExpense()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", this }
            };

            await _navigationService.NavigateAsync(nameof(NewExpensePage), parameters);
        }
        private async void SelectTripAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", this }
            };

            await _navigationService.NavigateAsync(nameof(MyTripsPage), parameters);
        }

        private async void SelectTrip2Async()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", this }
            };

            await _navigationService.NavigateAsync(nameof(MyTripPage), parameters);
        }


    }
}