using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        public TripDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Trip Details";
        }
    }
}
