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
        
        public static async void store(Settings settings)
        {
            
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            XmlSerializer ser = new XmlSerializer(typeof(Settings));
            StringWriter stringWriter = new StringWriter();
            ser.Serialize(stringWriter, settings);

            StorageFile sampleFile = await localFolder.CreateFileAsync("settings.txt", CreationCollisionOption.ReplaceExisting);
            
            await FileIO.WriteTextAsync(sampleFile, stringWriter.ToString());
            
        }

        public static async Task<Settings> load()
        {
           
            try
            {
                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                StorageFile sampleFile = await localFolder.GetFileAsync("settings.txt");
                string text = await FileIO.ReadTextAsync(sampleFile);

                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                StringReader stringReader = new StringReader(text);
                Settings settings = (Settings)ser.Deserialize(stringReader);
                
                return settings;
            } 
            catch (Exception e)
            {
                return new Settings();
            }
        }

    }
}
