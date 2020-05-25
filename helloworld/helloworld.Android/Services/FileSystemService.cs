//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using System.IO;
using System.Resources;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using helloworld.Services;
using File = Java.IO.File;

namespace helloworld.Droid.Services
{
    public class FileSystemService : IFileSystemService
    {
        private File _cacheFile;

        public string GetWakeWordModelPath(string filename)
        {
            var context = MainActivity.Instance;
            _cacheFile = new File(context.CacheDir, filename);

            if (!_cacheFile.Exists())
            {
                using (var br = new BinaryReader(Application.Context.Assets.Open(filename)))
                {
                    using (var bw = new BinaryWriter(new FileStream(_cacheFile.AbsolutePath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
            return _cacheFile.AbsolutePath;
        }

//        public async Task<string> GetWakeWordModelPath()
//        {
//            //            const string keywordFilePath = "Computer.table";
//            //            var file = Android.App.Application.Context.GetFileStreamPath(keywordFilePath);
//            //            return file.Path;
//            //
//            //            AssetManager assets = Android.App.Application.Context.Assets;
//
//            //            const string keywordFilePath = @"file:///android_asset/Computer.table";
//            //            var file = Android.App.Application.Context.GetFileStreamPath(keywordFilePath);
//            //            return file.Path;
//
//            //            const string keywordFilePath = @"file:///android_asset/Computer.table";
//            //            var file = Android.App.Application.Context.OpenFileOutput(keywordFilePath, FileCreationMode.Append);
//            //            return file.Path;
//
//            //            return @"D:/Dev/ASM/Computer.table";
//
//            AssetManager assets = Android.App.Application.Context.Assets;
//            
//            //SEMI-WORKING VERSION
//            const string keywordFilePath = @"Computer.table";
//            var createdFile = Android.App.Application.Context.OpenFileOutput(keywordFilePath, FileCreationMode.Append);
//            var file = Android.App.Application.Context.GetFileStreamPath(keywordFilePath);
//            return file.Path;
//        }
    }
}
