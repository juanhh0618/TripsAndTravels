﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using Syncfusion.SfBusyIndicator.XForms.Droid;
using Xamarin.Forms;

namespace TripsAndTravels.Prism.Droid
{
    [Activity(Label = "Trips And Travels", 
        Icon = "@mipmap/ic_launcher", 
        Theme = "@style/MainTheme",
        MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Forms.SetFlags("CollectionView_Experimental");

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            new SfBusyIndicatorRenderer();
            LoadApplication(new App(new AndroidInitializer()));
            //global::Xamarin.Forms.Forms.Init(this, bundle);

        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

