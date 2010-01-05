using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
enum MenuAction
{
    ShowMain,
    ShowOptions,
    ShowQuiz,
    ShowGame,
    ShowScores
}
namespace XNASystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SystemMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        // initialize a _font to be used for text
        SpriteFont _font;

        // initilize a texture for the selection _box
        Texture2D _box;

        // stack of menus being drawn
        List<Menu> _menuList = new List<Menu>();

        int _up = 1;
        int _down = 1;
        int _choice = 0;
        private int _enter = 1;

        public SystemMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            _menuList.Add(new Menu("Welcome to the XNA Game System", new List<MenuItem>
                                                                         {
                                                                             new MenuItem("Take Quiz", MenuAction.ShowMain),
                                                                              new MenuItem("Change Options", MenuAction.ShowOptions),
                                                                              new MenuItem("View Scores", MenuAction.ShowMain)
                                                                         }));

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
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
            KeyboardState state = Keyboard.GetState();
            
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
                _choice = 0;
                var action = _menuList.Last().GetSelectedItem().Action;
                switch (action)
                {
                    case MenuAction.ShowMain:
                        RemoveAllButMain();
                        break;
                    case MenuAction.ShowGame:
                        break;
                    case MenuAction.ShowOptions:
                        _menuList.Add(new Menu("Options Screen (NYI)", new List<MenuItem>
                                                                         {
                                                                             new MenuItem("Some Sweet Option", MenuAction.ShowMain),
                                                                              new MenuItem("Color Jazz", MenuAction.ShowMain),
                                                                              new MenuItem("The Greatest Option in the World", MenuAction.ShowMain),
                                                                              new MenuItem("Return Home", MenuAction.ShowMain)
                                                                         }));
                        break;
                    case MenuAction.ShowQuiz:
                        break;
                    case MenuAction.ShowScores:
                        break;
                    default:
                        break;
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