using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DirectoryFileCount.Tools
{
    internal static class SerializeManager
    {
        private static string CheckAndCreatePath(string filename)
        {
            if (!Directory.Exists(StaticResources.ClientDirPath))
                Directory.CreateDirectory(StaticResources.ClientDirPath);

            return Path.Combine(StaticResources.ClientDirPath, filename);
        }

        public static void Serialize<TObject>(TObject obj, string fileName)
        {
            try
            {
                var formatter = new BinaryFormatter();
                var filename = CheckAndCreatePath(fileName);

                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, obj);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to serialize object", ex);
            }
        }
        public static TObject Deserialize<TObject>(string filename)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    return (TObject) formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to deserialize object", ex);
                throw;
            }
        }
    }
}
