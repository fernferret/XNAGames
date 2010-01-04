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

namespace XNASystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SystemInstance : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // initialize a font to be used for text
        SpriteFont font;

        // initilize a texture for the selection box
        Texture2D box;

        // main variable to keep game state
        int systemState = 0;

        // variables to keep track of where the icon box should be drawn
        int iconPosition = 1;
        int optionsIconPosition = 1;
        int up = 0;
        int down = 0;

        public SystemInstance()
        {
            graphics = new GraphicsDeviceManager(this);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load font with the font arial
            font = Content.Load<SpriteFont>("Fonts//Arial");

            //load box with the box texture
            box = Content.Load<Texture2D>("Sprites//box");
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
            // initialize the keyboard state
            KeyboardState state = Keyboard.GetState();

            // check for up and down key strokes.
            if (state.IsKeyUp(Keys.Up))
            {
                if (up == 1)
                {
                    up--;
                    iconPosition--;
                }
            }
            if (state.IsKeyUp(Keys.Down))
            {
                if (down == 1)
                {
                    down--;
                    iconPosition++;
                }
            }
            if (state.IsKeyDown(Keys.Up))
            {
                up = 1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                down = 1;
            }

            if (state.IsKeyDown(Keys.Enter))
            {
                switch (iconPosition)
                {
                    case 1:
                        using (SystemOptions options = new SystemOptions())
                        {
                            this.SuppressDraw();
                            options.Run();
                        }
                        break;
                    case 2:
                        using (QuizGameLoop loop = new QuizGameLoop())
                        {
                            loop.Run();
                        }
                        break;
                    case 3:
                        using (ScoreViewer view = new ScoreViewer())
                        {
                            view.Run();
                        }
                        break;
                    default:
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // begin drawing to screen
            spriteBatch.Begin();

            switch (gameState)
            {
                // case 0 is the main menu and gives the user the choice of whether to change the options,
                // check scores or start the quiz game loop
                case 0:
                    // draw the welcome header
                    spriteBatch.DrawString(font, "Welcome to the XNA Game System.", new Vector2(250, 100), Color.Black);

                    switch (iconPosition)
                    {
                        case 0:
                            iconPosition = 3;
                            spriteBatch.Draw(box, new Vector2(180, 380), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(box, new Vector2(180, 180), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(box, new Vector2(180, 280), Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(box, new Vector2(180, 380), Color.White);
                            break;
                        default:
                            iconPosition = 1;
                            spriteBatch.Draw(box, new Vector2(180, 180), Color.White);
                            break;
                    }

                    //draw the three option strings
                    spriteBatch.DrawString(font, "Options", new Vector2(200, 200), Color.Black);
                    spriteBatch.DrawString(font, "Take a Quiz", new Vector2(200, 300), Color.Black);
                    spriteBatch.DrawString(font, "View Scores", new Vector2(200, 400), Color.Black);

                    break;
                case 1:
                    // draw the welcome header
                    spriteBatch.DrawString(font, "Options", new Vector2(400, 100), Color.Black);

                    switch (optionsIconPosition)
                    {
                        case 0:
                            optionsIconPosition = 2;
                            spriteBatch.Draw(box, new Vector2(180, 330), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(box, new Vector2(180, 230), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(box, new Vector2(180, 330), Color.White);
                            break;
                        default:
                            optionsIconPosition = 1;
                            spriteBatch.Draw(box, new Vector2(180, 230), Color.White);
                            break;
                    }

                    //draw the three option strings
                    spriteBatch.DrawString(font, "Quiz", new Vector2(200, 250), Color.Black);
                    spriteBatch.DrawString(font, "Game", new Vector2(200, 400), Color.Black);

                    break;
                default:
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
