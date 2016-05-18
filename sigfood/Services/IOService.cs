using sigfood.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace sigfood.Services
{
    class IOService
    {
        public static async void store(Collection<Day> day)
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            XmlSerializer ser = new XmlSerializer(typeof(Day));
            StringWriter stringWriter = new StringWriter();
            ser.Serialize(stringWriter, day[0]);

            StorageFile sampleFile = await localFolder.CreateFileAsync("cache.txt", CreationCollisionOption.ReplaceExisting);
            
            await FileIO.WriteTextAsync(sampleFile, stringWriter.ToString());
        }

        public static async void load()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            StorageFile sampleFile = await localFolder.GetFileAsync("cache.txt");
            string text = await FileIO.ReadTextAsync(sampleFile);
        }

    }
}
