using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using TripsAndTravels.Prism.Views;
using TripsAndTravels.Common.Services;

namespace TripsAndTravels.Prism.ViewModels
{
    public class TripsAndTravelsMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;
        private DelegateCommand _modifyUserCommand;
        private readonly IApiService _apiService;
        private static TripsAndTravelsMasterDetailPageViewModel _instance;


        public TripsAndTravelsMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _apiService = apiService;
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }

        public DelegateCommand ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new DelegateCommand(ModifyUserAsync));

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

        private async void ModifyUserAsync()
        {
            await _navigationService.NavigateAsync($"/TripsAndTravelsMasterDetailPage/NavigationPage/{nameof(ModifyUserPage)}");
        }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public static TripsAndTravelsMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }


        private void LoadMenus()
        {
            if (Settings.IsLogin)
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
                    Icon = "",
                    PageName = "TripList",
                    Title = "My Trips"
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

            else
            {

                List<Menu> menus2 = new List<Menu>
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
                    PageName = "LoginPage",
                    Title = Languages.AddNewTrip
                },
                new Menu
                {
                    Icon = "ic_details",
                    PageName = "TripList",
                    Title = "My Trips"
                },
                new Menu
                {
                    Icon = "ic_details",
                    PageName = "LoginPage",
                    Title = Languages.SeeTripDetails
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "LoginPage",
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
                    menus2.Select(m => new MenuItemViewModel(_navigationService)
                    {
                        Icon = m.Icon,
                        PageName = m.PageName,
                        Title = m.Title
                    }).ToList());
            }
        }
    }
}
