using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Helpers;
using Newtonsoft.Json;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripsAndTravelsMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;


        public TripsAndTravelsMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
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
                    Title = Settings.IsLogin ? Languages.Logout : Languages.Login
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
