using System;
using System.IO;

namespace DirectoryFileCount.Tools
{
    static class StaticResources
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static readonly string ClientDirPath = Path.Combine(AppData, "DirectoryFileCount");
        internal static readonly string ClientLogDirPath = Path.Combine(ClientDirPath, "Logs");
    }
}
