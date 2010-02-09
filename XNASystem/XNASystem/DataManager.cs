using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
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

        public NameWrapper(byte[] bytes)
        {
            BookletNames = new List<string>();

            int position = 0;

            //Find how many strings to deserialize
            int num = DataManager.ReadLengthForNextSection(bytes, ref position);

            int nameIndex = 0;
            for (nameIndex = 0; nameIndex < num; nameIndex++)
            {
                //Construct and add the next name
                BookletNames.Add(DataManager.ReadStringFromByteArray(bytes, ref position));
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> bytes = new List<byte>();

            //Set number of strings
            bytes.Add((byte)(BookletNames.Count / byte.MaxValue));
            bytes.Add((byte)(BookletNames.Count % byte.MaxValue));

            //Serialize each string
            int index = 0;
            for (index = 0; index < BookletNames.Count; index++ )
            {
                string name = BookletNames[index];
                //Set the length header of the current name
                bytes.Add((byte)(name.Length / byte.MaxValue));
                bytes.Add((byte)(name.Length % byte.MaxValue));

                //Serialize the name
                foreach (char c in name)
                {
                    bytes.Add((byte)c);
                }
            }
            
            return bytes.ToArray();
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

        #region Operations

        public DataManager()
        {
            FindNameCabinet(0, CabinetMode.Open, "name_file.sys");
        }

        public void Close()
        {
            this.WaitOnOperation();
            FindNameCabinet(0, CabinetMode.Save, "name_file.sys");
        }

		public List<Booklet> LoadBooklets(PlayerIndex playerIndex)
		{
            this.WaitOnOperation();
		    List<Booklet> booklets = new List<Booklet>();
		    foreach (string bookletName in _nameWrapper.BookletNames)
		    {
		        FindCabinet(playerIndex, CabinetMode.Open, bookletName);
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
            this.WaitOnOperation();
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
                    BinaryWriter myBw = new BinaryWriter(fileStream);
                    try
                    {
                        myBw.Write(booklet.ToByteArray());
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                    finally
                    {
                        myBw.Flush();
                        myBw.Close();
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
                    BinaryWriter myBw = new BinaryWriter(fileStream);
                    try
                    {
                        myBw.Write(_nameWrapper.ToByteArray());
                    }
                    finally
                    {
                        myBw.Flush();
                        myBw.Close();
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
                    BinaryReader myBr = new BinaryReader(fileStream);
                    try
                    {
                        fileStream.Position = 0;
                        _currentBooklet = new Booklet(myBr.ReadBytes((int)fileStream.Length));
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
                        BinaryReader myBr = new BinaryReader(fileStream);
                        try
                        {
                            fileStream.Position = 0;
                            _nameWrapper = new NameWrapper(myBr.ReadBytes((int)fileStream.Length));
                        }
                        catch (Exception e)
                        {
                            _nameWrapper = new NameWrapper();
                        }
                        finally
                        {
                            myBr.Close();
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

        private void WaitOnOperation()
        {
            while (this._operationPending)
            {
                ;
            }
        }

        public static string ReadStringFromByteArray(byte[] bytes, ref int position)
        {
            //Find the end of the string using the first two bytes
            int stringEnd = bytes[position]*byte.MaxValue + bytes[position + 1] + 2 + position;
            position += 2;
            string returnString = "";

            while (position < stringEnd)
            {
                returnString += (char)bytes[position];
                position++;
            }

            return returnString;
        }

        public static bool ReadBooleanFromByteArray(byte[] bytes, ref int position)
        {
            bool b = (bytes[position] == 1 ? true : false);
            position++;
            return b;
        }

        public static int ReadLengthForNextSection(byte[] bytes, ref int position)
        {
            int lengthOfSection = bytes[position]*byte.MaxValue + bytes[position + 1];
            position += 2;
            return lengthOfSection;
        }

        #endregion
    }
}
