﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using commutr.Droid.Configuration;

namespace commutr.Droid
{
    [Activity(Label = "commutr", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var container = SimpleInjectorConfiguration.Configure();
            Xamarin.FormsMaps.Init(this, savedInstanceState); //TODO: create API Key for google maps
            LoadApplication(new App(container));
        }
    }
}