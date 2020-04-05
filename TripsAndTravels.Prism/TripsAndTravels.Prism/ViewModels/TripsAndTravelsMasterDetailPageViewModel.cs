using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    Title = "Home"
                },
                new Menu
                {
                    Icon = "ic_control_point",
                    PageName = "NewTripPage",
                    Title = "Add new trip"
                },
                new Menu
                {
                    Icon = "ic_details",
                    PageName = "TripDetailsPage",
                    Title = "See trip details"
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Log in"
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
