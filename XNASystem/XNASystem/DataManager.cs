using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace XNASystem
{
    public enum CabinetMode
    {
        Open,
        Save,
    }

    public class NameWrapper
    {
        public List<string> BookletNames;
    }

    /* The DataManager class is responsible for the loading and saving of data required to persist through sessions */
    class DataManager
    {
        #region Fields

        private bool _operationPending;
        private NameWrapper _nameWrapper;
        private Booklet _currentBooklet;

        #endregion

        #region Properties

        #endregion

        #region Operations

        public DataManager()
        {
            FindNameCabinet(0, CabinetMode.Open, "name_file.sys");
        }

		public Booklet[] LoadBooklets(PlayerIndex playerIndex)
		{
		    List<Booklet> booklets = new List<Booklet>();
		    foreach (string bookletName in _nameWrapper.BookletNames)
		    {
		        FindCabinet(playerIndex, CabinetMode.Open, bookletName);
		        while (_operationPending)
		        {
		            ;
		        }
		        booklets.Add(_currentBooklet);
		    }
		    return booklets.ToArray();
		}

		public bool SaveBooklet(PlayerIndex playerIndex, Booklet booklet)
		{
			try
			{
			    _currentBooklet = booklet;
			    FindCabinet(playerIndex, CabinetMode.Save, booklet.GetTitle());
			    return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
        
        private void FindCabinet(PlayerIndex playerIndex, CabinetMode cabinetMode, string sFileName)
        {
            if(!Guide.IsVisible && !_operationPending)
            {
                _operationPending = true;

                if(cabinetMode == CabinetMode.Open)
                    Guide.BeginShowStorageDeviceSelector(playerIndex,
                        FindStorageDevice, "loadRequest:" + sFileName);
                if(cabinetMode == CabinetMode.Save)
                    Guide.BeginShowStorageDeviceSelector(playerIndex,
                        FindStorageDevice, "saveRequest:" + sFileName);
            }
        }

        private void FindNameCabinet(PlayerIndex playerIndex, CabinetMode cabinetMode, string sFileName)
        {
            if (!Guide.IsVisible && !_operationPending)
            {
                _operationPending = true;

                if (cabinetMode == CabinetMode.Open)
                    Guide.BeginShowStorageDeviceSelector(playerIndex,
                        FindNameStorageDevice, "loadRequest:" + sFileName);
                if (cabinetMode == CabinetMode.Save)
                    Guide.BeginShowStorageDeviceSelector(playerIndex,
                        FindNameStorageDevice, "saveRequest:" + sFileName);
            }
        }

		private void FindStorageDevice(IAsyncResult result)
        {
			StorageDevice storageDevice = Guide.EndShowStorageDeviceSelector(result); 
 
            if(storageDevice != null)
            {
                string value = (string)result.AsyncState;
                string[] splitStrings = value.Split(':');

                if(splitStrings[0] == "saveRequest")
                    SaveBookletData(splitStrings[1], storageDevice, _currentBooklet);
                if(splitStrings[0] == "loadRequest")
                    LoadBookletData(splitStrings[1], storageDevice);

                while(!result.IsCompleted)
                {
                }
            }
        }

        private void FindNameStorageDevice(IAsyncResult result)
        {
            StorageDevice storageDevice = Guide.EndShowStorageDeviceSelector(result);

            if (storageDevice != null)
            {
                string value = (string)result.AsyncState;
                string[] splitStrings = value.Split(':');

                if (splitStrings[0] == "saveRequest")
                    SaveNameData(splitStrings[1], storageDevice, _nameWrapper);
                if (splitStrings[0] == "loadRequest")
                    LoadNameData(splitStrings[1], storageDevice);

                while (!result.IsCompleted)
                {
                }
            }
        }

		void SaveBookletData(string filename, StorageDevice storageDevice, Booklet booklet)
        {
            StorageContainer storageContainer = storageDevice.OpenContainer("Content");
            string filenamePath = Path.Combine(StorageContainer.TitleLocation, filename);
            
            FileStream fileStream = File.Create(filenamePath);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Booklet));
                xmlSerializer.Serialize(fileStream, booklet);
            }
            finally
            {
                fileStream.Close();
                _operationPending = false;
                storageContainer.Dispose();
            }
        }

        void SaveNameData(string filename, StorageDevice storageDevice, NameWrapper nameWrapper)
        {
            StorageContainer storageContainer = storageDevice.OpenContainer("Content");
            string filenamePath = Path.Combine(StorageContainer.TitleLocation, filename);

            FileStream fileStream = File.Create(filenamePath);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(NameWrapper));
                xmlSerializer.Serialize(fileStream, nameWrapper);
            }
            finally
            {
                fileStream.Close();
                _operationPending = false;
                storageContainer.Dispose();
            }
        }

		void LoadBookletData(string filename, StorageDevice storageDevice)
        {
            StorageContainer storageContainer = storageDevice.OpenContainer("Content");
            string filenamePath = Path.Combine(StorageContainer.TitleLocation, filename);

            FileStream fileStream = File.OpenRead(filenamePath);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Booklet));
                _currentBooklet = (Booklet)xmlSerializer.Deserialize(fileStream);
            }
            finally
            {
                fileStream.Close();
                _operationPending = false;
                storageContainer.Dispose();
            }
        }

        void LoadNameData(string filename, StorageDevice storageDevice)
        {
            StorageContainer storageContainer = storageDevice.OpenContainer("Content");
            string filenamePath = Path.Combine(StorageContainer.TitleLocation, filename);

            FileStream fileStream = File.OpenRead(filenamePath);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Booklet));
                _nameWrapper = (NameWrapper)xmlSerializer.Deserialize(fileStream);
            }
            finally
            {
                fileStream.Close();
                _operationPending = false;
                storageContainer.Dispose();
            }
        }
        #endregion
    }
}
