//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using System;
using System.Threading.Tasks;
using Windows.Storage;
using helloworld.Services;

namespace helloworld.UWP.Services
{
    public class FileSystemService : IFileSystemService
    {
        public string GetWakeWordModelPath(string filename)
        {
            Task<string> task = Task.Run(async () => await GetAssetPathAsync(filename));
            return task.Result;
        }

        private async Task<string> GetAssetPathAsync(string filename)
        {
            string kwsModel = "ms-appx:///Assets/" + filename;
            StorageFile kwsModelfile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(kwsModel));
            return kwsModelfile.Path;
        }

//        public async Task<string> GetWakeWordModelPath()
//        {
//            const string keywordFilePath = "ms-appx:///WakeWord/Computer.table";
//            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(keywordFilePath));
//            return file.Path;
//        }
    }
}
