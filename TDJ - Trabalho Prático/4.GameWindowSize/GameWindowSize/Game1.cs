using BookExample;
using GameWindowSize.GraphicsSupport;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameWindowSize
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public SpriteBatch sSpriteBatch; // Drawing support
        static public ContentManager sContent; // Loading textures
        static public GraphicsDeviceManager sGraphics; // Current display size

        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;
        // Prefer window size
        const int kWindowWidth = 800;
        const int kWindowHeight = 600;
        const int kNumObjects = 4;
        // Work with the TexturedPrimitive class
        TexturedPrimitive[] mGraphicsObjects; // An array of objects
        int mCurrentIndex = 0;

        static public Random sRan;

        TexturedPrimitive mUWBLogo;
        SoccerBall mBall;
        Vector2 mSoccerPosition = new Vector2(50, 50);
        float mSoccerBallRadius = 3f;

        MyGame mTheGame;

        public Game1()
        {
            // Content resource loading support
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            // Create graphics device to access window size
            Game1.sGraphics = new GraphicsDeviceManager(this);
            // set prefer window size
            Game1.sGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.sGraphics.PreferredBackBufferHeight = kWindowHeight;

            Game1.sRan = new Random();//Gera um numero random
        }

        /// <summary>
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
            Game1.sSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(10f, 20f), 100f);

            // Create the primitives.
            mGraphicsObjects = new TexturedPrimitive[kNumObjects];
            mGraphicsObjects[0] = new TexturedPrimitive(
            "Soccer", // Image file name
            new Vector2(15f, 25f), // Position to draw
            new Vector2(10f, 10f)); // Size to draw
            mGraphicsObjects[1] = new TexturedPrimitive(
            "UWB-JPG",
            new Vector2(35f, 60f),
            new Vector2(50f, 50f));
            mGraphicsObjects[2] = new TexturedPrimitive(
            "UWB-PNG",
            new Vector2(105f, 25f),
            new Vector2(10f, 10f));
            mGraphicsObjects[3] = new TexturedPrimitive(
            "UWB-PNG",
            new Vector2(90f, 60f),
            new Vector2(35f, 35f));
            // NOTE: Since the creation of TexturedPrimitive involves loading of textures,
            // the creation should occur in or after LoadContent()


            mUWBLogo = new TexturedPrimitive("UWB-PNG", new Vector2(30, 30), new Vector2(20, 20));
            mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);

            mTheGame = new MyGame();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (InputWrapper.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mUWBLogo.Update(InputWrapper.ThumbSticks.Left, Vector2.Zero);
            mBall.Update();
            mBall.Update(Vector2.Zero, InputWrapper.ThumbSticks.Right);

            // "A" to toggle to full-screen mode
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);

                if (!Game1.sGraphics.IsFullScreen)
                {
                    Game1.sGraphics.IsFullScreen = true;
                    Game1.sGraphics.ApplyChanges();
                }
            }

            // "B" toggles back to windowed mode
            if (InputWrapper.Buttons.B == ButtonState.Pressed)
            {
                if (Game1.sGraphics.IsFullScreen)
                {
                    Game1.sGraphics.IsFullScreen = false;
                    Game1.sGraphics.ApplyChanges();
                }
            }

            // Button X to select the next object to work with
            if (InputWrapper.Buttons.X == ButtonState.Pressed)
                mCurrentIndex = (mCurrentIndex + 1) % kNumObjects;
            // Update currently working object with thumb sticks.
            mGraphicsObjects[mCurrentIndex].Update(
            InputWrapper.ThumbSticks.Left,
            InputWrapper.ThumbSticks.Right);
            base.Update(gameTime);
            base.Update(gameTime);

            mTheGame.UpdateGame(gameTime);
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
                mTheGame = new MyGame();
        }

       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear to background color
            GraphicsDevice.Clear(Color.CornflowerBlue);
          Game1.sSpriteBatch.Begin(); // Initialize drawing support
                                        // Loop over and draw each primitive
            //mUWBLogo.Draw();
            //mBall.Draw();


            mTheGame.DrawGame();
            /*    foreach (TexturedPrimitive p in mGraphicsObjects)
                {
                    p.Draw();
                }*/
            Game1.sSpriteBatch.End(); // Inform graphics system we are done drawing 

            // Print out text message to echo status
            /*           FontSupport.PrintStatus("Selected object is:" + mCurrentIndex +
                       " Location=" + mGraphicsObjects[mCurrentIndex].Position, null);
                       FontSupport.PrintStatusAt(mGraphicsObjects[mCurrentIndex].Position, "Selected",
                       Color.Red);*/
      /*      FontSupport.PrintStatus("Ball Position:" + mBall.Position, null);
            FontSupport.PrintStatusAt(mUWBLogo.Position,
            mUWBLogo.Position.ToString(), Color.White);
            FontSupport.PrintStatusAt(mBall.Position, "Radius" + mBall.Radius, Color.Red);*/
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
