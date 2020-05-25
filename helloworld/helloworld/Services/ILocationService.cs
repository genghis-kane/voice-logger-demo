//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using System.Threading.Tasks;

namespace helloworld
{
    public interface ILocationService
    {
        Task<bool> GetPermissionsAsync();

        Task<string> GetCurrentGPSCoordinatesAsync();

        void OnRequestPermissionsResult(bool isGranted);
    }
}
