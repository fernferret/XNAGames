
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNASystem.Interfaces;
using XNASystem.QuizArch;
using XNASystem.SystemMenus;
using XNASystem.Utils;
using Microsoft.Xna.Framework.Audio;

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
		public static SpriteBatch GameSpriteBatch;
		public static int Game;
		public static int Height { get; set; }
		public static int Width { get; set; }
		private Score _score;
		// graphics variables
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

        // initialize a list of fonts
        public static Dictionary<String, SpriteFont> FontPackage;

        // initilize a list of textures
        public static Dictionary<String, Texture2D> TexturePackage;
        
		// initialize the Global GameTime Variable
		public static GameTime CurrentGameTime;

		// initialize the menustack
		private Stack<IScreen> _menuStack;

        // initialize the DataManager
        private DataManager _dataManager;

		public static SoundEffect SoundShoot;
		public static SoundEffectInstance SoundShootInstance;

		public static SoundEffect SoundBoom;
		public static SoundEffectInstance SoundBoomInstance;

		public static List<SoundEffect> SoundsBackground;
		public static List<SoundEffectInstance> SoundsBackgroundInstance;

		public static SoundEffect SoundBackground;
		public static SoundEffectInstance SoundBackgroundInstance;

		public static SoundEffect SoundOuch;
		public static SoundEffectInstance SoundOuchInstance;

		public static SoundEffect SoundOof;
		public static SoundEffectInstance SoundOofInstance;

		public static SoundEffect SoundNoooo;
		public static SoundEffectInstance SoundNooooInstance;

		public static SoundEffect SoundQuizBg;
		public static SoundEffectInstance SoundQuizBgInstance;

		public static SoundEffect SoundShootEnemy;
		
		public static SoundEffectInstance SoundShootEnemyInstance;
		//initialize a new list of booklets
    	public static List<Booklet> Booklets;

		//initializes holders for the current booklets and quizzes
    	private Booklet _currentBooklet;
    	private Quiz _currentQuiz;

		private static InputHandler _handler;
		public static DrawHelper DrawHelper;
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
			Height = 720; Width = 1280;
			//Height = 600; Width = 800;
			_handler = new InputHandler();
			
			// graphics initializer Also initialize the height and width to 720p
			//_graphics = new GraphicsDeviceManager(this);
			_graphics = new GraphicsDeviceManager(this) {PreferredBackBufferWidth = Width, PreferredBackBufferHeight = Height};
			//content location
			Content.RootDirectory = "Content";

            GamerServicesDispatcher.Initialize(Services);

			// initialize font package and texture package
			FontPackage = new Dictionary<String, SpriteFont>();
			TexturePackage = new Dictionary<String, Texture2D>();

			// create the stack
			_menuStack = new Stack<IScreen>();

            // create the DataManager and load name list
            _dataManager = new DataManager();

			// create a list of booklets the system can run off of
            Booklets = _dataManager.LoadBooklets(0);
        }

		public static int SelectedBooklet;

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
			DrawHelper = new DrawHelper(_spriteBatch);

			SoundShoot = Content.Load<SoundEffect>("Audio\\Waves\\shoot");
			SoundShootInstance = SoundShoot.CreateInstance();

			SoundBoom = Content.Load<SoundEffect>("Audio\\Waves\\baboom");
			SoundBoomInstance = SoundBoom.CreateInstance();

			SoundShootEnemy = Content.Load<SoundEffect>("Audio\\Waves\\shoot_enemy");
			SoundShootEnemyInstance = SoundShootEnemy.CreateInstance();

			SoundOuch = Content.Load<SoundEffect>("Audio\\Waves\\ouch");
			SoundOuchInstance = SoundOuch.CreateInstance();

			SoundOof = Content.Load<SoundEffect>("Audio\\Waves\\oof");
			SoundOofInstance = SoundOof.CreateInstance();

			SoundNoooo = Content.Load<SoundEffect>("Audio\\Waves\\no");
			SoundNooooInstance = SoundNoooo.CreateInstance();

			SoundQuizBg = Content.Load<SoundEffect>("Audio\\Waves\\glow");
			SoundQuizBgInstance = SoundQuizBg.CreateInstance();

			SoundsBackground = new List<SoundEffect>();
			SoundsBackgroundInstance = new List<SoundEffectInstance>();
			for (var i = 1; i <= 12; i++)
			{
				var path = "Audio\\Waves\\groove" + i;
				SoundsBackground.Add(Content.Load<SoundEffect>(path));
				SoundsBackgroundInstance.Add(SoundsBackground[i-1].CreateInstance()); 
			}
			for (var i = 1; i <= 4; i++)
			{
				var path = "Audio\\Waves\\jgroove" + i;
				SoundsBackground.Add(Content.Load<SoundEffect>(path));
				SoundsBackgroundInstance.Add(SoundsBackground[i+12-1].CreateInstance()); 
			}

				// load the fonts
				FontPackage.Add(Content.Load<SpriteFont>("Fonts//Arial"));
			FontPackage.Add(Content.Load<SpriteFont>("Fonts//Main"));
			FontPackage.Add(Content.Load<SpriteFont>("Fonts//Title"));

            //load texture package
            _texturePackage.Add(Content.Load<Texture2D>("Sprites//Hilight_center"));
            _texturePackage.Add(Content.Load<Texture2D>("Sprites//xnaGamesBackground_"+_graphics.PreferredBackBufferWidth+"_"+_graphics.PreferredBackBufferHeight));
            _texturePackage.Add(Content.Load<Texture2D>("Sprites//BreakoutGame//Breakout_Block_Meta_newl"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//BreakoutGame//Breakout_Paddle"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//wall"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//BreakoutGame//Breakout_Block_Template"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//BreakoutGame//BreakoutBall"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ceiling"));

			//Shooter Game Textures
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ship"));//8
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//ship_alternate"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_1"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_2"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_ship_3"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterenemybasic")); //13
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//shooterenemybasic_alternate"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_1b"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_2b"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//ShooterGame//beginexplosion_3b"));
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
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//Hilight_left"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//Hilight_right"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//UI//UIBorder"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//UI//UIFill"));
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//UI//UICorner"));//68
			_texturePackage.Add(Content.Load<Texture2D>("Sprites//BreakoutGame//Breakout_BallBlock"));


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

            _menuStack.Peek().Draw(_spriteBatch, FontPackage, TexturePackage);
            base.Draw(gameTime);
        }
        #endregion

        public void SetStack(Stack<IScreen> stack)
        {
            _menuStack = stack;
        }

		public List<Booklet> GetBookletList()
		{
			return Booklets;
		}

		public List<Quiz> GetQuizList()
		{
			return _currentBooklet.GetAsList();
		}

		public int GetCurrentBooklet()
		{
			return Booklets.IndexOf(_currentBooklet);
		}

		public int GetCurrentQuiz()
		{
			return _currentBooklet.GetAsList().IndexOf(_currentQuiz);
		}

		public void SetCurrentBooklet(int index)
		{
			_currentBooklet = Booklets[index];
			_currentQuiz = _currentBooklet.GetAsList().Count == 0 ? null : _currentBooklet.GetSpecificQuiz(0);
		}

		public void SetCurrentQuiz(int index)
		{
			_currentQuiz = _currentBooklet.GetSpecificQuiz(index);
		}

		public void CreateBooklet(string name)
		{
			Booklets.Add(new Booklet(name));
		}

		public void CreateQuiz(int bookletIndex, string name)
		{
			Booklets[bookletIndex].AddItem(new Quiz(name));
		}

		public void CreateQuestion(int bookletIndex, int quizIndex, string question, List<Answer> answers)
		{
			Booklets[bookletIndex].GetSpecificQuiz(quizIndex).AddItem(new Question(question, answers));
			//_booklets[bookletIndex].AddQuestionToQuiz(quizIndex, new Question(question, answers));
		}

		public void ReportScore(Score s)
		{
			_score = s;
		}

        public void Close()
        {
            foreach (Booklet booklet in Booklets)
            {
                _dataManager.SaveBooklet(0, booklet);
            }
            this.Exit();
        }
	}
}

