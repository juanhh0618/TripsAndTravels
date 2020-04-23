/*using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Prism.ViewModels
{
    public class NewExpensePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private TripResponse _trip;
        public NewExpensePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add New Expense";
        }
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

    }
}
*/

using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripsAndTravels.Common.Helpers;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.Helpers;
using TripsAndTravels.Prism.Views;
using Xamarin.Forms;

namespace TripsAndTravels.Prism.ViewModels
{
    public class NewExpensePageViewModel : ViewModelBase
    {

        private bool _isRunning;
        private bool _isEnabled;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private MediaFile _file;
        private ImageSource _image;
        private TripResponse _trip;
        private TripDetailsResponse _tripDetailsId;
        private DelegateCommand _saveExpense;
        private DelegateCommand _changeImageCommand;

        public NewExpensePageViewModel(INavigationService navigationService, IApiService apiService, IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            _trip = new TripResponse();
            _tripDetailsId = new TripDetailsResponse();
            Title = "Add New Expense";
            IsEnabled = true;
            IsRunning = false;
            Image = App.Current.Resources["UrlNoImage"].ToString();

        }

        public DelegateCommand SaveExpense => _saveExpense ?? (_saveExpense = new DelegateCommand(SaveNewExpense));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public int Value { get; set; }

        

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public TripDetailsResponse TripDetailsId
        {
            get => _tripDetailsId;
            set => SetProperty(ref _tripDetailsId, value);
        }

        public string ExpenseType { get; set; }

        public int id;

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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

      /*  public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
        }
        */
        private async void SaveNewExpense()
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
                await App.Current.MainPage.DisplayAlert("Error", "No hay conexión a Internet", "Aceptar");
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            ExpensesRequest expensesRequest = new ExpensesRequest
            {

                Id = Trip.TripDetails[0].Id,
                Value = Value,
                ExpensesType = ExpenseType,
                PictureArray = imageArray
            };

            

            

        /*var expenseRequest = new ExpensesRequest
            {
                Value = Value,
                ExpensesType = ExpenseType,
                PictureArray = imageArray,
                Id = TripDetailsId.Id

            };
            */
            var response = await _apiService.AddNewExpense(url, "/api", "/Trips/AddNewExpense", "bearer", token.Token, expensesRequest);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Nuevo gasto agregado correctamente", "Aceptar");
            await _navigationService.NavigateAsync(nameof(TripDetailsPage));

        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                "Obtener imagen de:",
                "Cancelar",
                null,
                "Galeria",
                "Camara");

            if (source == "Cancelar")
            {
                _file = null;
                return;
            }

            if (source == "Camara")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Value.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor el valor del gasto", "Aceptar");
                return false;
            }

            if (ExpenseType == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor escriba el tipo de gasto", "Aceptar");
                return false;
            }

            return true;
        }

    }
}