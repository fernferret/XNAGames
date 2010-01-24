using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.SystemMenus;

#region unneed enums

// Enumeration that specifies various Menu Actions
enum MenuAction
{
    ShowMain,
    ShowOptions,
    ShowQuiz,
    ShowGame,
    ShowScores,
    Return,
    ShowEditor,
    DoNothing,
    ShowEditorMain
}
// Enumeration that specifies the status of a quiz/booklet
public enum Status
{
    NotStarted,
    InProgress,
    Completed,
    Error
}

#endregion

namespace XNASystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SystemMain : Game
    {
        #region variable creation

        // graphics variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // initialize a list of fonts
        private readonly List<SpriteFont> _fontPackage;

        // initilize a list of textures
        private readonly List<Texture2D> _texturePackage;

        // initialize the menustack
        private Stack<IScreen> _menuStack;

		//initialize a new list of booklets
    	private readonly List<Booklet> _booklets;

		//initializes holders for the current booklets and quizzes
    	private Booklet _currentBooklet;
    	private Quiz _currentQuiz;

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

        // Question loader that will preload all quizzes in
        // a booklet
        private readonly QuestionLoader _qLoad;

        // The target booklet to dump data into
        private readonly Booklet _booklet;
*/
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

			// create a list of booklets the system can run off of
			_booklets = new List<Booklet>();

			////////////////////////////////////////////////delete all this once xml works///////////////////////////////////////////////////////////////////////////
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

			_booklets.Add(defaultb);
			_booklets.Add(second);
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        	// initialize the currents to jsut the first boklet and the first quiz in that booklet
        	_currentBooklet = _booklets[0];
        	_currentQuiz = _booklets[0].GetQuizList()[0];

/*            _qLoad = new QuestionLoader();

            //populate a booklet from xml files
            _booklet = _qLoad.PopulateSystem();

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
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//paddle"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//wall"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//block"));

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
            // the keyState the keyboard is in right now
            var keyState = Keyboard.GetState();
        	var padState = GamePad.GetState(PlayerIndex.One);
            // use the update method from the current menu
            _menuStack.Peek().Update(keyState, padState);
        }
        #endregion

        #region menu administration
/*
        // this is the ation the kiks off a game after the quiz
        private void ShowGameMenu()
        {
            _menuList.Add(new Menu("Boom...Game! (NYI)", new List<IMenuItem>
                                                             {
                                                                 new NavItem("Return", MenuAction.Return)
                                                             }));
        }

        //this adds a menu to the stack which shows scores for particular users
        private void ShowScoreMenu()
        {
            _menuList.Add(new Menu("BooYaa...Scores! (NYI)", new List<IMenuItem>
                                                                 {
                                                                     new NavItem("Return",
                                                                                 MenuAction.Return)
                                                                 }));
        }

        //this adds a menu to the stack which kicks of the question editor menu sequence
        private void ShowEditorMainMenu()
        {
            _menuList.Add(new Menu("Question Editor", new List<IMenuItem>
                                                           {
                                                               new NavItem("Change Booklet (NYI)", MenuAction.DoNothing),
                                                               new NavItem("Change Quiz (NYI)", MenuAction.DoNothing),
                                                               new NavItem("Write new Question", MenuAction.ShowEditor)
                                                           }));
        }
        private void ShowEditorMenu()
        {
            _menuList.Add(new Menu("New Question", new List<IMenuItem>
                                                       {
                                                           new NavItem("Type your")
                                                       }));
        }

        // this adds a menu to the stack that shows the option and allows them to be changed
        private void ShowOptionsMenu()
        {
            _menuList.Add(new Menu("Options Screen (NYI)", new List<IMenuItem>
                                                               {
                                                                   new NavItem("Some Sweet Option", MenuAction.ShowMain),
                                                                   new NavItem("Color Jazz", MenuAction.ShowMain),
                                                                   new NavItem("The Greatest Option in the World", MenuAction.ShowMain),
                                                                   new NavItem("Return Home", MenuAction.ShowMain)
                                                               }));
        }

        #region menu removal
        // this removes a menu from the menu stack
        private void PopMenu()
        {
            _menuList.RemoveAt(_menuList.Count - 1);
        }

        //this removes all the menus except for he main menu from the stack
        private void RemoveAllButMain()
        {
            _menuList.RemoveRange(1,_menuList.Count - 1);
        }
        #endregion
*/
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
/*
            var items = 300 / _menuList.Last().GetNum();
            var counter = 0;

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, _menuList.Last().GetTitle(), new Vector2(250, 100), Color.Black);

            foreach (var item in _menuList.Last().GetItems())
            {
                if (item == _menuList.Last().GetSelectedItem())
                {
                    _spriteBatch.DrawString(_font, item.GetTitle(), new Vector2(100, 200 + items*counter), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(_font, item.GetTitle(), new Vector2(100, 200 + items * counter), Color.Black);
                }
                counter++;
            }

            _spriteBatch.End();
 */
            base.Draw(gameTime);
        }
        #endregion

        public void SetStack(Stack<IScreen> stack)
        {
            _menuStack = stack;
        }

		public List<Booklet> GetBookletList()
		{
			return _booklets;
		}

		public List<Quiz> GetQuizList()
		{
			return _currentBooklet.GetQuizList();
		}

		public int GetCurrentBooklet()
		{
			return _booklets.IndexOf(_currentBooklet);
		}

		public int GetCurrentQuiz()
		{
			return _currentBooklet.GetQuizList().IndexOf(_currentQuiz);
		}

		public void SetCurrentBooklet(int index)
		{
			_currentBooklet = _booklets[index];
			if (_currentBooklet.GetQuizList().Count == 0)
			{
				_currentQuiz = null;
			}
			else
			{
				_currentQuiz = _currentBooklet.GetQuizList()[0];

			}
		}

		public void SetCurrentQuiz(int index)
		{
			_currentQuiz = _currentBooklet.GetQuizList()[index];
		}

		public void CreateBooklet(string name)
		{
			_booklets.Add(new Booklet(name));
		}

		public void CreateQuiz(int bookletIndex, string name)
		{
			_booklets[bookletIndex].AddItem(new Quiz(name));
		}

		public void CreateQuestion(int bookletIndex, int quizIndex, string question, List<Answer> answers)
		{
			_booklets[bookletIndex].AddQuestionToQuiz(quizIndex, new Question(question, answers));
		}
    }
}