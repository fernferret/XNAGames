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
// Enumeration that specifies the status of a quiz/booklet
enum Status
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
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        // initialize a _font to be used for text
        SpriteFont _font;

        // initilize a texture for the selection _box
        Texture2D _box;

        // stack of menus being drawn
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

        // System Constructor, performs initialization
        public SystemMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            _qLoad = new QuestionLoader();
            _booklet = _qLoad.PopulateSystem();
            _menuList.Add(new Menu("Welcome to the XNA Game System", new List<IMenuItem>
                                                                         {
                                                                             new NavItem("Take Quiz", MenuAction.ShowQuiz),
                                                                             new NavItem("Change Options", MenuAction.ShowOptions),
                                                                             new NavItem("View Scores", MenuAction.ShowMain)
                                                                         }));

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load _font with the _font arial
            _font = Content.Load<SpriteFont>("Fonts//Arial");

            //load _box with the _box texture
            _box = Content.Load<Texture2D>("Sprites//box");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var items = _menuList.Last().GetNum();
            var state = Keyboard.GetState();
            
            if(state.IsKeyDown(Keys.Up) && _up != 1)
            {
                _up = 1;
                _choice--;
            }
            if (state.IsKeyDown(Keys.Down) && _down != 1)
            {
                _down = 1;
                _choice++;
            }
            if (state.IsKeyUp(Keys.Up))
            {
                _up = 0;
            }
            if (state.IsKeyUp(Keys.Down))
            {
                _down = 0;
            }
            if (state.IsKeyDown(Keys.Enter) && _enter != 1)
            {
                _enter = 1;

                // Reinitialize choice upon menu change
                _choice = 0;
                var item = _menuList.Last().GetSelectedItem();
                
                if(item.GetType() == typeof(NavItem))
                {
                    var action = ((NavItem)item).GetAction();
                    switch (action)
                    {
                        case MenuAction.ShowMain:
                            RemoveAllButMain();
                            break;
                        case MenuAction.ShowGame:
                            ShowGameMenu();
                            break;
                        case MenuAction.ShowOptions:
                            ShowOptionsMenu();
                            break;
                        case MenuAction.ShowQuiz:
                            _booklet.Reset();
                            _menuList.Add(_booklet.AdvanceQuestion());
                            break;
                        case MenuAction.ShowScores:
                            ShowScoreMenu();
                            break;
                        case MenuAction.Return:
                            PopMenu();
                            break;
                        default:
                            break;
                    }
                }
                else if(item.GetType() == typeof(QuestionItem))
                {
                    ((QuestionItem)item).AnswerQuestion();
                    if (!_booklet.DoneWithQuiz())
                    {
                        _menuList.Add(_booklet.AdvanceQuestion());
                        _menuList.RemoveAt(_menuList.Count - 2);
                    }
                    else
                    {
                        if(_booklet.AdvanceQuiz())
                        {
                            _menuList.Add(_booklet.AdvanceQuestion());
                            _booklet.ResetQuizAdvance();
                            // Remove the prior Question
                            _menuList.RemoveAt(_menuList.Count - 2);
                        }
                        else
                        {
                            // Special case for leaving only main menu in
                            PopMenu();
                        }
                    }
                }
            }
            if (state.IsKeyUp(Keys.Enter))
            {
                _enter = 0;
            }
            if(_choice == -1)
            {
                _choice = items - 1;
            }
            if(_choice == items)
            {
                _choice = 0;
            }
            _menuList.Last().SetSelectedItem(_choice);
            base.Update(gameTime);
        }

        private void ShowGameMenu()
        {
            _menuList.Add(new Menu("Boom...Game! (NYI)", new List<IMenuItem>
                                                             {
                                                                 new NavItem("Return", MenuAction.Return)
                                                             }));
        }

        private void ShowScoreMenu()
        {
            _menuList.Add(new Menu("BooYaa...Scores! (NYI)", new List<IMenuItem>
                                                                 {
                                                                     new NavItem("Return",
                                                                                 MenuAction.Return)
                                                                 }));
        }

        private void PopMenu()
        {
            _menuList.RemoveAt(_menuList.Count - 1);
        }

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

        private void RemoveAllButMain()
        {
            _menuList.RemoveRange(1,_menuList.Count - 1);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
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
            base.Draw(gameTime);
        }
    }
}
