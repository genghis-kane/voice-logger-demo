//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Xamarin.Essentials;

namespace helloworld.Droid
{
    class LocationService : ILocationService
    {
        public const int REQUEST_FINE_LOCATION = 2;

        private readonly string[] _permissions = { Manifest.Permission.AccessFineLocation };
        private TaskCompletionSource<bool> _tcsPermissions;

        public Task<bool> GetPermissionsAsync()
        {
            _tcsPermissions = new TaskCompletionSource<bool>();

            // Permissions are required only for Marshmallow and up
            if ((int)Build.VERSION.SdkInt < 23)
            {
                _tcsPermissions.TrySetResult(true);
            }
            else
            {
                var currentActivity = MainActivity.Instance;
                if (ActivityCompat.CheckSelfPermission(currentActivity, Manifest.Permission.AccessFineLocation) != (int)Android.Content.PM.Permission.Granted)
                {
                    RequestLocationPermission();
                }
                else
                {
                    _tcsPermissions.TrySetResult(true);
                }
            }
            return _tcsPermissions.Task;
        }

        public async Task<string> GetCurrentGPSCoordinatesAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            return (location != null) ? $"{location.Latitude}, {location.Longitude}" : string.Empty;
        }

        private void RequestLocationPermission()
        {
            var currentActivity = MainActivity.Instance;
            if (ActivityCompat.ShouldShowRequestPermissionRationale(currentActivity, Manifest.Permission.AccessFineLocation))
            {
                Snackbar.Make(currentActivity.FindViewById((Android.Resource.Id.Content)), "App requires location permission.", Snackbar.LengthIndefinite).SetAction("Ok", v =>
                {
                    ((Activity)currentActivity).RequestPermissions(_permissions, REQUEST_FINE_LOCATION);

                }).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(((Activity)currentActivity), _permissions, REQUEST_FINE_LOCATION);
            }
        }

        public void OnRequestPermissionsResult(bool isGranted)
        {
            _tcsPermissions.TrySetResult(isGranted);
        }
    }
}
