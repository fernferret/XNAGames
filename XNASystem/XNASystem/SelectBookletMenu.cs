using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XNASystem
{
	class SelectBookletMenu : IScreen
	{
		#region variables

		protected int _up;
		protected int _down;
		protected int _enter;
		protected int _choice;
		protected Stack<IScreen> _menuStack;
		protected SystemMain _systemMain;
		protected EditorMainMenu _editor;

		#endregion

		#region constructor
		public SelectBookletMenu(Stack<IScreen> stack, SystemMain main, EditorMainMenu editor)
		{
			_up = 1;
			_down = 1;
			_enter = 1;
			_choice = 0;
			_menuStack = stack;
			_systemMain = main;
			_editor = editor;
		}
		#endregion

		#region update
		public void Update(KeyboardState state)
		{
			#region arrow controls
			// up arrow control
			if (state.IsKeyDown(Keys.Up) && _up != 1)
			{
				_up = 1;
				_choice--;
			}
			if (state.IsKeyUp(Keys.Up))
			{
				_up = 0;
			}

			//down arrow control
			if (state.IsKeyDown(Keys.Down) && _down != 1)
			{
				_down = 1;
				_choice++;
			}
			if (state.IsKeyUp(Keys.Down))
			{
				_down = 0;
			}
			#endregion

			#region enter controls

			//enter key controls
			if (state.IsKeyDown(Keys.Enter) && _enter != 1)
			{
				_enter = 1;
				_menuStack.Pop();
				// for all the booklet choices
				if (_choice < _systemMain.GetBookletList().Count)
				{
					_editor.SetCurrentBooklet(_choice);
					_systemMain.SetCurrentBooklet(_choice);
				}
				//for the craete a new booklet choice
				if (_choice == _systemMain.GetBookletList().Count)
				{
					_menuStack.Push(new CreateBookletMenu(_menuStack,_systemMain, _editor));
				}
				_systemMain.SetStack(_menuStack);
			}
			if (state.IsKeyUp(Keys.Enter))
			{
				_enter = 0;
			}

			#endregion

			#region set choice
			// make sure that choice is always on an actually menu choice
			if (_choice == -1)
			{
				_choice = _systemMain.GetBookletList().Count + 2;
			}
			if (_choice == _systemMain.GetBookletList().Count + 2)
			{
				_choice = 0;
			}

			#endregion
		}
		#endregion

		#region draw
		public void Draw(SpriteBatch spriteBatch, List<SpriteFont> fonts, List<Texture2D> textures)
		{
			spriteBatch.Begin();

			//draw the background
			spriteBatch.Draw(textures[1], new Rectangle(0, 0, 800, 600), Color.White);

			// draw the selection box
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + ((300 / (_systemMain.GetBookletList().Count + 1)) * _choice)), Color.White);

			//draw the title
			spriteBatch.DrawString(fonts[0], "Choose a booklet", new Vector2(250, 100), Color.Black);

			//draw the menu items
			int counter;
			for (counter = 0; counter < _systemMain.GetBookletList().Count; counter++)
			{
				spriteBatch.DrawString(fonts[0], _systemMain.GetBookletList()[counter].GetTitle(), new Vector2(100, 200 + ((300 / (_systemMain.GetBookletList().Count + 1)) * counter)), Color.Black);
			}
			spriteBatch.DrawString(fonts[0], "Create New Booklet", new Vector2(100, 200 + ((300 / (_systemMain.GetBookletList().Count + 1)) * counter)), Color.Black);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Black);

			spriteBatch.End();
		}
		#endregion
	}
}
