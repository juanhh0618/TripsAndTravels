using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using TripsAndTravels.Common.Helpers;
using TripsAndTravels.Common.Services;
using TripsAndTravels.Prism.ViewModels;
using TripsAndTravels.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TripsAndTravels.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM0ODM1QDMxMzgyZTMxMmUzMGJaZFFCamlPZisxSmk4Z3UwNkh4bC9wTFlrQWFGTlh4WU1IYXdSUTNUTlk9");
            InitializeComponent();

            await NavigationService.NavigateAsync("/TripsAndTravelsMasterDetailPage/NavigationPage/HomePage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<TripsAndTravelsMasterDetailPage, TripsAndTravelsMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<NewTripPage, NewTripPageViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailsPage, TripDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<TripList, TripListViewModel>();
            containerRegistry.RegisterForNavigation<NewExpensePage, NewExpensePageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsPage, DetailsPageViewModel>();
        }
    }
}
