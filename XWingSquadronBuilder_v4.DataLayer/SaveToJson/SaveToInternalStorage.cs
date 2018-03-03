using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace XWingSquadronBuilder_v4.DataLayer.SaveToJson
{
    public class SaveToInternalStorage
    {
        public static Task<string> ReadFile(string filename)
        {
            return Task.Run(async () =>
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                var openedFileStream = await localFolder.OpenStreamForReadAsync(filename);
                StreamReader reader = new StreamReader(openedFileStream);
                return reader.ReadToEnd();
            });
        }

        public static Task<bool> SaveFile(string filename, string data)
        {
            return Task.Run(async () =>
            {
                try
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                    var savingFileStream = await localFolder.OpenStreamForWriteAsync(filename, CreationCollisionOption.ReplaceExisting);
                    StreamWriter writer = new StreamWriter(savingFileStream);
                    writer.Write(data);
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            });
        }

    }
}
