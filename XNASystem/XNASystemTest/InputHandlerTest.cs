using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem.Utils;
using Rhino.Mocks;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using System;

namespace XNASystemTest
{
	[TestClass]
	public class InputHandlerTest
	{
		private InputHandler _handler;
		private MockRepository _mocks;
		[TestInitialize]
		public void SetUp()
		{

		}

		[TestMethod]
		public void TestHandlerCreation()
		{
			_handler = new InputHandler();
			Assert.IsNotNull(_handler);
		}
		[TestMethod]
		public void TestAliasCreationForButton()
		{
			var alias = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			Assert.IsNotNull(alias);
		}
		[TestMethod]
		public void TestAliasCreationForKey()
		{
			var alias = new ButtonAlias(Keys.A, -1, -1, "TestAlias");
			Assert.IsNotNull(alias);
		}
		[TestMethod]
		public void TestAddingToSuperButtonArray()
		{
			var handler = new InputHandler();
			//var keystate = new KeyboardState();
			var aliasAButton = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			var aliasAKey = new ButtonAlias(Keys.Up, -1, -1, "TestAlias");
			var dictionary = new Dictionary<ButtonAction, List<ButtonAlias>>();
			var listOfAliases = new List<ButtonAlias> { aliasAButton, aliasAKey };
			dictionary.Add(ButtonAction.MenuUp, listOfAliases);

			var buttonsetter = typeof(InputHandler).GetField("_superButton", BindingFlags.Static | BindingFlags.NonPublic);
			buttonsetter.SetValue(handler, dictionary);

			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(handler);

			var test = typeof(KeyboardState).GetField("currentState1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			test.SetValue(keystatev, UInt32.Parse("64"));

			keystate.SetValue(handler, keystatev);
			Assert.IsTrue(handler.IsButtonPressed(ButtonAction.MenuUp));
		}

		[TestMethod]
		public void TestButtonPressedTab()
		{
			var handler = new InputHandler();
			//var keystate = new KeyboardState();
			var aliasAButton = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			var aliasAKey = new ButtonAlias(Keys.Down, -1, -1, "TestAlias");
			var dictionary = new Dictionary<ButtonAction, List<ButtonAlias>>();
			var listOfAliases = new List<ButtonAlias> { aliasAButton, aliasAKey };
			dictionary.Add(ButtonAction.MenuUp, listOfAliases);

			var buttonsetter = typeof(InputHandler).GetField("_superButton", BindingFlags.Static | BindingFlags.NonPublic);
			buttonsetter.SetValue(handler, dictionary);

			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(handler);

			var test = typeof(KeyboardState).GetField("currentState0", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			// Tab Key
			test.SetValue(keystatev, UInt32.Parse("512"));

			keystate.SetValue(handler, keystatev);
			Assert.IsFalse(handler.IsButtonPressed(ButtonAction.MenuUp));
		}

		[TestMethod]
		public void TestButtonPressedUp()
		{
			var handler = new InputHandler();
			//var keystate = new KeyboardState();
			var aliasAButton = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			var aliasAKey = new ButtonAlias(Keys.Up, -1, -1, "TestAlias");
			var dictionary = new Dictionary<ButtonAction, List<ButtonAlias>>();
			var listOfAliases = new List<ButtonAlias> { aliasAButton, aliasAKey };
			dictionary.Add(ButtonAction.MenuUp, listOfAliases);

			var buttonsetter = typeof(InputHandler).GetField("_superButton", BindingFlags.Static | BindingFlags.NonPublic);
			buttonsetter.SetValue(handler, dictionary);

			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(handler);

			var test = typeof(KeyboardState).GetField("currentState1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			// Tab Key
			test.SetValue(keystatev, UInt32.Parse("64"));

			keystate.SetValue(handler, keystatev);
			Assert.IsTrue(handler.IsButtonPressed(ButtonAction.MenuUp));
		}

		[TestMethod]
		public void TestButtonPressedDown()
		{
			var handler = new InputHandler();
			//var keystate = new KeyboardState();
			var aliasAButton = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			var aliasAKey = new ButtonAlias(Keys.Down, -1, -1, "TestAlias");
			var dictionary = new Dictionary<ButtonAction, List<ButtonAlias>>();
			var listOfAliases = new List<ButtonAlias> { aliasAButton, aliasAKey };
			dictionary.Add(ButtonAction.MenuUp, listOfAliases);

			var buttonsetter = typeof(InputHandler).GetField("_superButton", BindingFlags.Static | BindingFlags.NonPublic);
			buttonsetter.SetValue(handler, dictionary);

			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(handler);

			var test = typeof(KeyboardState).GetField("currentState1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			// Tab Key
			test.SetValue(keystatev, UInt32.Parse("256"));

			keystate.SetValue(handler, keystatev);
			Assert.IsTrue(handler.IsButtonPressed(ButtonAction.MenuUp));
		}

		[TestMethod]
		public void TestButtonPressedDownButItsActuallyUp()
		{
			var handler = new InputHandler();
			//var keystate = new KeyboardState();
			var aliasAButton = new ButtonAlias(Buttons.A, -1, -1, "TestAlias");
			var aliasAKey = new ButtonAlias(Keys.Down, -1, -1, "TestAlias");
			var dictionary = new Dictionary<ButtonAction, List<ButtonAlias>>();
			var listOfAliases = new List<ButtonAlias> { aliasAButton, aliasAKey };
			dictionary.Add(ButtonAction.MenuDown, listOfAliases);

			var buttonsetter = typeof(InputHandler).GetField("_superButton", BindingFlags.Static | BindingFlags.NonPublic);
			buttonsetter.SetValue(handler, dictionary);

			var keystate = typeof(InputHandler).GetField("_keyState", BindingFlags.NonPublic | BindingFlags.Instance);
			var keystatev = keystate.GetValue(handler);

			var test = typeof(KeyboardState).GetField("currentState1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			// Tab Key
			test.SetValue(keystatev, UInt32.Parse("64"));

			keystate.SetValue(handler, keystatev);
			Assert.IsFalse(handler.IsButtonPressed(ButtonAction.MenuDown));
		}
	}
}
