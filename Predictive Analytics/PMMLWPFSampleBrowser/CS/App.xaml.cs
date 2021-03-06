#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Syncfusion.Licensing;
using System.IO;
using System.Reflection;
using System.Text;

namespace PMMLWPFSampleBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      public App()
        {
            SyncfusionLicenseProvider.RegisterLicense(DemoCommon.FindLicenseKey());
        }
    }
    public static class DemoCommon
    {
        /// <summary>
        /// Helper method to find a syncfusion license key from the Common folder
        /// </summary>
        /// <param name="fileName">File name of the syncfusion license key</param>
        /// <returns></returns>
        public static string FindLicenseKey()
        {
            int levelsToCheck = 12;
            string filePath = @"Common\SyncfusionLicense.txt";
    
            // here condition checking for the file path for the network sharing issue

            string rootPath = string.Empty;

            if (Assembly.GetEntryAssembly().CodeBase.Contains(@"file:///"))
            {
                rootPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase.Replace(@"file:///", ""));
            }
            else
            {
                rootPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase.Replace(@"file://", ""));
            }


            // below line is already existing line now this is replaced as above
            //string rootPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().CodeBase.Replace(@"file:///", ""));
    
            for (int n = 0; n < levelsToCheck; n++)
            {
                string fileDataPath = System.IO.Path.Combine(rootPath, filePath);
                if (System.IO.File.Exists(fileDataPath))
                    return File.ReadAllText(fileDataPath, Encoding.UTF8);
                rootPath = Directory.GetParent(rootPath).FullName;
            }
            return string.Empty;
        }
    }
}
