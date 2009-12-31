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
    public class SystemMain : Microsoft.Xna.Framework.Game
    {
/*      Default XNA Stuff we may not need
 * 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
 */

        public SystemMain()
        {
/*          Default XNA stuff we may not need
 * 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
 */
            // Start off by displaying the main menu and save the user response
            int action = displayMenu();

            switch (action)
            {
                // run the options menu
                case 1:
                    runOptions();
                    break;
                
                // run quiz loop
                case 2:
                    takeQuiz();
                    break;

                // run the scores menu
                case 3:
                    displayScores();
                    break;
                default:
                    break;
            }

        }

        // this is supposed to display the initial menu with options to take a quiz, look at scores, or change options
        // returns an int that signifies wht option was chosen
        public int dispalyMenu()
        {
            //1 = options
            //2 = take quiz
            //3 = scores
            return 0;
        }

        // this is supposed to display the current options and allow changes to be made
        public void runOptions()
        {

        }

        // this is the start of the quiz game loop
        public void takeQuiz()
        {

        }

        // this is supposed to show the scores of the current user
        public void displayScores()
        {

        }

/*      Default XNA stuff probably dont need here      
 * 
 *      /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }*/
    }
}
