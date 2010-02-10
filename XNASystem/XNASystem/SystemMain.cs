using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Displays;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.SystemMenus;
using XNASystem.Utils;

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
        private readonly List<SpriteFont> _fontPackage;

        // initilize a list of textures
        private readonly List<Texture2D> _texturePackage;

		// initialize the menustack
		private Stack<IScreen> _menuStack;

        // initialize the DataManager
        private DataManager _dataManager;

		//initialize a new list of booklets
    	private readonly List<Booklet> _booklets;

		//initializes holders for the current booklets and quizzes
    	private Booklet _currentBooklet;
    	private Quiz _currentQuiz;

		private static InputHandler _handler;
		//public static List<ButtonAlias> PressedButtons = new List<ButtonAlias>();
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
			_handler = new InputHandler();
			// graphics initializer
			_graphics = new GraphicsDeviceManager(this);

			//content location
			Content.RootDirectory = "Content";

            GamerServicesDispatcher.Initialize(this.Services);

			// initialize font package and texture package
			_fontPackage = new List<SpriteFont>();
			_texturePackage = new List<Texture2D>();

			// create the stack
			_menuStack = new Stack<IScreen>();

            // create the DataManager and load name list
            _dataManager = new DataManager();

			// create a list of booklets the system can run off of
            _booklets = _dataManager.LoadBooklets(0);

			/*///////////////////////////////////////////////delete all this once xml works///////////////////////////////////////////////////////////////////////////
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
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/

        	// initialize the currents to jsut the first boklet and the first quiz in that booklet
        	_currentBooklet = _booklets[0];
        	_currentQuiz = _booklets[0].GetSpecificQuiz(0);

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
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ball"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ceiling"));

			//Shooter Game Textures
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ship"));//8
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ship_alternate"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_1"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_2"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_3"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterenemybasic")); //13
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterenemybasic_alternate"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_enemy_1"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_enemy_2"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_enemy_3"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_1"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_2"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_3"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_4"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_5"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//dead_ship"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//projectile"));//24
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterenemybasic_pain"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterboss"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterboss_alternative"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterboss_pain"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_6"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_7"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//explosion_8"));
			//boss explosion
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_1_a"));//32
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_3_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_4_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_5_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_6_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_7_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_8_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_9_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_10_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_11_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_12_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_13_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_14_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_15_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_16_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_17_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_18_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_19_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_20_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_21_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_22_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_23_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_24_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_25_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_26_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_27_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_28_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_29_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_30_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_31_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_32_a"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ShooterBoss_explosion_33_a")); //32-63



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
        	_handler.SetInputs(keyState, padState);
            // use the update method from the current menu
			//_menuStack.Peek().Update(keyState, padState);
			_menuStack.Peek().Update(_handler, gameTime);
			//_sysDis.Update2(_handler);
            
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
			return _currentBooklet.GetAsList();
		}

		public int GetCurrentBooklet()
		{
			return _booklets.IndexOf(_currentBooklet);
		}

		public int GetCurrentQuiz()
		{
			return _currentBooklet.GetAsList().IndexOf(_currentQuiz);
		}

		public void SetCurrentBooklet(int index)
		{
			_currentBooklet = _booklets[index];
			if (_currentBooklet.GetAsList().Count == 0)
			{
				_currentQuiz = null;
			}
			else
			{
				_currentQuiz = _currentBooklet.GetSpecificQuiz(0);

			}
		}

		public void SetCurrentQuiz(int index)
		{
			_currentQuiz = _currentBooklet.GetSpecificQuiz(index);
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
			_booklets[bookletIndex].GetSpecificQuiz(quizIndex).AddItem(new Question(question, answers));
			//_booklets[bookletIndex].AddQuestionToQuiz(quizIndex, new Question(question, answers));
		}

		public void ReportScore(Score s)
		{
			_score = s;
		}

        public void Close()
        {
            foreach (Booklet booklet in _booklets)
            {
                _dataManager.SaveBooklet(0, booklet);
            }
            this.Exit();
        }
	}
}

