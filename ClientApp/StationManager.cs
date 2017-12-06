using System;
using System.Windows;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount
{
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        internal static void ShutDown(int exitCode)
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(exitCode);
        }
    }
}
