using Prism;
using Prism.Ioc;
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
            InitializeComponent();

            await NavigationService.NavigateAsync("/TripsAndTravelsMasterDetailPage/NavigationPage/HomePage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<TripsAndTravelsMasterDetailPage, TripsAndTravelsMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<NewTripPage, NewTripPageViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailsPage, TripDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
