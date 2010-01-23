using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;

namespace XNASystem.MaterialEditor
{
	class SelectQuizMenu : IScreen
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
		public SelectQuizMenu(Stack<IScreen> stack, SystemMain main, EditorMainMenu editor)
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
		public void Update(KeyboardState keyState, GamePadState padState)
		{
			#region arrow controls
			// up arrow control
			if (keyState.IsKeyDown(Keys.Up) && _up != 1)
			{
				_up = 1;
				_choice--;
			}
			if (keyState.IsKeyUp(Keys.Up))
			{
				_up = 0;
			}

			//down arrow control
			if (keyState.IsKeyDown(Keys.Down) && _down != 1)
			{
				_down = 1;
				_choice++;
			}
			if (keyState.IsKeyUp(Keys.Down))
			{
				_down = 0;
			}
			#endregion

			#region enter controls

			//enter key controls
			if (keyState.IsKeyDown(Keys.Enter) && _enter != 1)
			{
				_enter = 1;
				_menuStack.Pop();
				if (_choice < _systemMain.GetQuizList().Count)
				{
					_editor.SetCurrentQuiz(_choice);
					_systemMain.SetCurrentQuiz(_choice);
				}
				if (_choice == _systemMain.GetQuizList().Count)
				{
					_menuStack.Push(new CreateQuizMenu(_menuStack, _systemMain, _editor));
				}
				_systemMain.SetStack(_menuStack);
			}
			if (keyState.IsKeyUp(Keys.Enter))
			{
				_enter = 0;
			}

			#endregion

			#region set choice
			// make sure that choice is always on an actually menu choice
			if (_choice == -1)
			{
				_choice = _systemMain.GetQuizList().Count + 2;
			}
			if (_choice == _systemMain.GetQuizList().Count + 2)
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
			spriteBatch.Draw(textures[0], new Vector2(75, 175 + ((300 / (_systemMain.GetQuizList().Count + 1)) * _choice)), Color.White);

			//draw the title
			spriteBatch.DrawString(fonts[0], "Choose a Quiz", new Vector2(250, 100), Color.Black);

			//draw the menu items
			int counter;
			for (counter = 0; counter < _systemMain.GetQuizList().Count; counter++)
			{
				spriteBatch.DrawString(fonts[0], _systemMain.GetQuizList()[counter].GetTitle(), new Vector2(100, 200 + ((300 / (_systemMain.GetQuizList().Count + 1)) * counter)), Color.Black);
			}
			spriteBatch.DrawString(fonts[0], "Create New Quiz (NYI)", new Vector2(100, 200 + ((300 / (_systemMain.GetQuizList().Count + 1)) * counter)), Color.Red);
			spriteBatch.DrawString(fonts[0], "Back", new Vector2(100, 500), Color.Red);

			spriteBatch.End();
		}
		#endregion
	}
}