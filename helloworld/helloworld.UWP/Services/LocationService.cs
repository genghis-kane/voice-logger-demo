//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using System.Threading.Tasks;
using Xamarin.Essentials;

namespace helloworld.UWP.Services
{
    public class LocationService : ILocationService
    {
        public async Task<bool> GetPermissionsAsync()
        {
            return true;
        }

        public async Task<string> GetCurrentGPSCoordinatesAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            return (location != null) ? $"{location.Latitude}, {location.Longitude}" : string.Empty;
        }

        public void OnRequestPermissionsResult(bool isGranted)
        {
        }
    }
}
