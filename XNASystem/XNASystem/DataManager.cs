using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

using XNASystem.QuizArch;

namespace XNASystem
{
    public enum CabinetMode
    {
        Open,
        Save,
    }

    [Serializable]
    public class NameWrapper
    {
        public List<string> BookletNames;

        public NameWrapper()
        {
            BookletNames = new List<string>();
        }
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

        //#region Operations

        /*public DataManager()
        {
            FindNameCabinet(0, CabinetMode.Open, "name_file.sys");
        }

        public void Close()
        {
            while (_operationPending)
            {
                ;
            }
            FindNameCabinet(0, CabinetMode.Save, "name_file.sys");
        }

		public List<Booklet> LoadBooklets(PlayerIndex playerIndex)
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

            //Hack for initialization with no booklets
            if(booklets.Count == 0)
            {
                Booklet defaultb = new Booklet("defualt Booklet");
                Booklet second = new Booklet("second Booklet");

                Quiz test1 = new Quiz("Test Quiz 1 default");
                Quiz test2 = new Quiz("Test Quiz 2 defualt");
                Quiz test3 = new Quiz("Test Quiz 1 second");
                Quiz test4 = new Quiz("Test Quiz 2 second");

                defaultb.AddItem(test1);
                defaultb.AddItem(test2);
                second.AddItem(test3);
                second.AddItem(test4);

                booklets.Add(defaultb);
                booklets.Add(second);
            }
		    return booklets;
		}

		public bool SaveBooklet(PlayerIndex playerIndex, Booklet booklet)
		{
			try
			{
			    _currentBooklet = booklet;
                if(!_nameWrapper.BookletNames.Contains(booklet.GetTitle()))
                {
                    _nameWrapper.BookletNames.Add(booklet.GetTitle());
                }
			    FindCabinet(playerIndex, CabinetMode.Save, booklet.GetTitle());
                this.Close();
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
            using (StorageContainer storageContainer = storageDevice.OpenContainer("Content"))
            {
                string filenamePath = Path.Combine(storageContainer.Path, filename);

                using (FileStream fileStream = File.Create(filenamePath))
                {
                    try
                    {
                    //    BinaryFormatter myBf = new BinaryFormatter();
                    //    myBf.Serialize(fileStream, booklet);
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                    finally
                    {
                        fileStream.Close();
                        _operationPending = false;
                        storageContainer.Dispose();
                    }
                }
            }
        }

        void SaveNameData(string filename, StorageDevice storageDevice, NameWrapper nameWrapper)
        {
            using (StorageContainer storageContainer = storageDevice.OpenContainer("Content"))
            {
                string filenamePath = Path.Combine(storageContainer.Path, filename);

                using (FileStream fileStream = File.Create(filenamePath))
                {
                    try
                    {
                     //   BinaryFormatter myBf = new BinaryFormatter();
                     //   myBf.Serialize(fileStream, nameWrapper);
                    }
                    finally
                    {
                        fileStream.Close();
                        _operationPending = false;
                        storageContainer.Dispose();
                    }
                }
            }
        }

		void LoadBookletData(string filename, StorageDevice storageDevice)
        {
            using (StorageContainer storageContainer = storageDevice.OpenContainer("Content"))
            {
                string filenamePath = Path.Combine(storageContainer.Path, filename);

                using (FileStream fileStream = File.OpenRead(filenamePath))
                {
                    try
                    {
                    //    BinaryFormatter myBf = new BinaryFormatter();
                        fileStream.Position = 0;
                     //   _currentBooklet = (Booklet) myBf.Deserialize(fileStream);
                    }
                    finally
                    {
                        fileStream.Close();
                        _operationPending = false;
                        storageContainer.Dispose();
                    }
                }
            }
        }

        void LoadNameData(string filename, StorageDevice storageDevice)
        {
            using (StorageContainer storageContainer = storageDevice.OpenContainer("Content"))
            {
                string filenamePath = Path.Combine(storageContainer.Path, filename);

                try
                {
                    using (FileStream fileStream = File.OpenRead(filenamePath))
                    {
                        try
                        {
                         //   BinaryFormatter myBf = new BinaryFormatter();
                            fileStream.Position = 0;
                        //    _nameWrapper = (NameWrapper) myBf.Deserialize(fileStream);
                        }
                        catch (Exception e)
                        {
                            _nameWrapper = new NameWrapper();
                        }
                        finally
                        {
                            fileStream.Close();
                            _operationPending = false;
                            storageContainer.Dispose();
                        }
                    }
                }
                catch (Exception e)
                {
                    _nameWrapper = new NameWrapper();
                    _operationPending = false;
                }
            }
        }
        #endregion*/
    }
}
