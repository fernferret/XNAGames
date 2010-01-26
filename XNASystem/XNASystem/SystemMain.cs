using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Enumeration that specifies various Menu Actions
enum MenuAction
{
	ShowMain,
	ShowOptions,
	ShowQuiz,
	ShowGame,
	ShowScores,
	Return
}

public enum ActivityType
{
	Game,
	Quiz
}
// Enumeration that specifies the status of a quiz/booklet
public enum Status
{
	NotStarted,
	InProgress,
	Completed,
	Error
}

namespace XNASystem
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class SystemMain : Game
	{
		#region variable creation

		private Score _score;
		// graphics variables
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		// initialize a list of fonts
		private List<SpriteFont> _fontPackage;

		// initilize a list of textures
		private List<Texture2D> _texturePackage;

		// initialize the menustack
		private Stack<IScreen> _menuStack;

		// initialie a new list of questions
		private List<Question> _newQuestions;

		#region old possibly unneed variables
		/*        // stack of menus being drawn
        readonly List<Menu> _menuList = new List<Menu>();

        // Set of variables to initialize button debounce
        // these are required so when a user preses enter
        // only one menu choice gets pressed
        private int _up = 1;
        private int _down = 1;
        private int _enter = 1;

        // Current Choice in a menu, gets cached for users
        private int _choice;
*/
        // Question loader that will preload all quizzes in
        // a booklet
        private QuestionLoader _qLoad;

        // The target booklet to dump data into
        private Booklet _booklet;

		#endregion

		#endregion

		#region constructor
		// System Constructor, performs initialization
		public SystemMain()
		{
			// graphics initializer
			_graphics = new GraphicsDeviceManager(this);

			//content location
			Content.RootDirectory = "Content";

			// initialize font package and texture package
			_fontPackage = new List<SpriteFont>();
			_texturePackage = new List<Texture2D>();

			// create the stack
			_menuStack = new Stack<IScreen>();

			// create a list of question for the editor
			_newQuestions = new List<Question>();

			/*            
						//create the static main menu
						_menuList.Add(new Menu("Welcome to the XNA Game System", new List<IMenuItem>
																					 {
																						 new NavItem("Take Quiz", MenuAction.ShowQuiz),
																						 new NavItem("Change Options", MenuAction.ShowOptions),
																						 new NavItem("View Scores", MenuAction.ShowMain),
																						 new NavItem("Edit Questions", MenuAction.ShowEditorMain)
																					 }));
			*/
		}
		#endregion

		#region Load and Unload
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to Draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// load the fonts
			_fontPackage.Add(Content.Load<SpriteFont>("Fonts//Arial"));

			//load texture package
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//box"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//XNA"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//grey box"));

			// give the stack the main menu
			_menuStack.Push(new MainMenu(_menuStack, this));
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		#endregion

		#region update
		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// the state the keyboard is in right now
			var state = Keyboard.GetState();

			// use the update method from the current menu
			_menuStack.Peek().Update(state);
		}
		#endregion

		#region Draw
		/// <summary>
		/// This is called when the game should Draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_menuStack.Peek().Draw(_spriteBatch, _fontPackage, _texturePackage);

			base.Draw(gameTime);
		}
		#endregion

		public void SetStack(Stack<IScreen> stack)
		{
			_menuStack = stack;
		}

		public void AddQuestion(Question question)
		{
			_newQuestions.Add(question);
		}

		public void ReportScore(Score s)
		{
			_score = s;
		}
	}
}
