//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using helloworld.Droid.Services;
using helloworld.Services;

namespace helloworld.Droid
{
    [Activity(Label = "helloworld", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int RECORD_AUDIO = 1;
        private const int REQUEST_FINE_LOCATION = 2;

        private IMicrophoneService _microphoneService;
        private ILocationService _locationService;

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Xamarin.Forms.DependencyService.Register<IMicrophoneService, MicrophoneService>();
            Xamarin.Forms.DependencyService.Register<IFileSystemService, FileSystemService>();
            Xamarin.Forms.DependencyService.Register<ILocationService, LocationService>();

            _microphoneService = Xamarin.Forms.DependencyService.Get<IMicrophoneService>();
            _locationService = Xamarin.Forms.DependencyService.Get<ILocationService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case RECORD_AUDIO:
                {
                    if (grantResults[0] == Permission.Granted)
                    {
                        _microphoneService.OnRequestPermissionsResult(true);
                    }
                    else
                    {
                        _microphoneService.OnRequestPermissionsResult(false);
                    }
                    break;
                }
                case REQUEST_FINE_LOCATION:
                {
                    if (grantResults[0] == Permission.Granted)
                    {
                        _locationService.OnRequestPermissionsResult(true);
                    }
                    else
                    {
                        _locationService.OnRequestPermissionsResult(false);
                    }
                    break;
                }
            }
        }
    }
}
