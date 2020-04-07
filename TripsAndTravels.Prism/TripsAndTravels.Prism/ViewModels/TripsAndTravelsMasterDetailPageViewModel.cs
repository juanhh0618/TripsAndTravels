using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripsAndTravelsMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public TripsAndTravelsMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_home",
                    PageName = "HomePage",
                    Title = Languages.Home
                },
                new Menu
                {
                    Icon = "ic_control_point",
                    PageName = "NewTripPage",
                    Title = Languages.AddNewTrip
                },
                new Menu
                {
                    Icon = "ic_details",
                    PageName = "TripDetailsPage",
                    Title = Languages.SeeTripDetails
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Languages.LogIn
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
