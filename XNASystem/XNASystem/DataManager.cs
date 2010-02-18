﻿using System;
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
			for (index = 0; index < BookletNames.Count; index++)
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
			if (booklets.Count == 0)
			{
				Booklet math = new Booklet("Math");
				//Booklet history = new Booklet("History");

				Quiz mqz1 = new Quiz("Test Quiz 1");
				Quiz mqz2 = new Quiz("Test Quiz 2");
				Quiz mqz3 = new Quiz("Test Quiz 3");
				Quiz mqz4 = new Quiz("Test Quiz 4");
				Quiz mqz5 = new Quiz("Test Quiz 5");
				Quiz mqz6 = new Quiz("Test Quiz 6");
				Quiz mqz7 = new Quiz("Test Quiz 7");
				//Quiz hqz1 = new Quiz("Test Quiz 1 second");
				//Quiz hqz2 = new Quiz("Test Quiz 2 second");

				Question mq11 = new Question("What is 1+1?", new List<Answer> { new Answer("2", true), new Answer("1", false), new Answer("4", false), new Answer("3", false) });
				Question mq12 = new Question("What is 1+5?", new List<Answer> { new Answer("6", true), new Answer("5", false), new Answer("12", false), new Answer("7", false) });
				Question mq13 = new Question("What is 13-2?", new List<Answer> { new Answer("11", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq21 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq22 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq23 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq31 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq32 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq33 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq41 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq42 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq43 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq51 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq52 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq53 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq61 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq62 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq63 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				Question mq71 = new Question("What is 1*1?", new List<Answer> { new Answer("1", true), new Answer("2", false), new Answer("4", false), new Answer("3", false) });
				Question mq72 = new Question("What is 1*5?", new List<Answer> { new Answer("6", false), new Answer("5", true), new Answer("12", false), new Answer("7", false) });
				Question mq73 = new Question("What is 14/2?", new List<Answer> { new Answer("7", true), new Answer("12", false), new Answer("10", false), new Answer("3", false) });

				mqz1.AddItem(mq11);
				mqz1.AddItem(mq12);
				mqz1.AddItem(mq13);

				mqz2.AddItem(mq21);
				mqz2.AddItem(mq22);
				mqz2.AddItem(mq23);

				mqz3.AddItem(mq31);
				mqz3.AddItem(mq32);
				mqz3.AddItem(mq33);

				mqz4.AddItem(mq41);
				mqz4.AddItem(mq42);
				mqz4.AddItem(mq43);

				mqz5.AddItem(mq51);
				mqz5.AddItem(mq52);
				mqz5.AddItem(mq53);

				mqz6.AddItem(mq61);
				mqz6.AddItem(mq62);
				mqz6.AddItem(mq63);

				mqz7.AddItem(mq71);
				mqz7.AddItem(mq72);
				mqz7.AddItem(mq73);


				math.AddItem(mqz1);
				math.AddItem(mqz2);
				math.AddItem(mqz3);
				math.AddItem(mqz4);
				math.AddItem(mqz5);
				math.AddItem(mqz6);
				math.AddItem(mqz7);
				//history.AddItem(hqz1);
				//history.AddItem(hqz2);

				booklets.Add(math);
				//booklets.Add(history);
			}
			return booklets;
		}

		public bool SaveBooklet(PlayerIndex playerIndex, Booklet booklet)
		{
			this.WaitOnOperation();
			try
			{
				_currentBooklet = booklet;
				if (!_nameWrapper.BookletNames.Contains(booklet.GetTitle()))
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
			if (!Guide.IsVisible && !_operationPending)
			{
				_operationPending = true;

				if (cabinetMode == CabinetMode.Open)
					Guide.BeginShowStorageDeviceSelector(playerIndex,
						FindStorageDevice, "loadRequest:" + sFileName);
				if (cabinetMode == CabinetMode.Save)
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

			if (storageDevice != null)
			{
				string value = (string)result.AsyncState;
				string[] splitStrings = value.Split(':');

				if (splitStrings[0] == "saveRequest")
					SaveBookletData(splitStrings[1], storageDevice, _currentBooklet);
				if (splitStrings[0] == "loadRequest")
					LoadBookletData(splitStrings[1], storageDevice);

				while (!result.IsCompleted)
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
			int stringEnd = bytes[position] * byte.MaxValue + bytes[position + 1] + 2 + position;
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
			int lengthOfSection = bytes[position] * byte.MaxValue + bytes[position + 1];
			position += 2;
			return lengthOfSection;
		}

		#endregion
	}
}