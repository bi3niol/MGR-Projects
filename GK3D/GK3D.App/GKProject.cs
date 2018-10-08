using GK3D.Components;
using GK3D.Components.Game;
using GK3D.Components.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GK3D.App
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GKProject : Game
    {
        enum States
        {
            Main
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager manager;

        public GKProject()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            manager = new GameManager(this, new StateManager());
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            manager.StateManager.SetState(States.Main, new ProjectSceneState
            {
                Camera = new Components.SceneObjects.Camera()
                {
                    Position = new Vector3(20, 0, 60),
                },
                Models = new System.Collections.Generic.List<IModel>
                {
                    new Sphere(graphics, new BasicEffect(graphics.GraphicsDevice), Color.DarkOrange, 10, 10, 10),
                    new Cube(graphics, new BasicEffect(graphics.GraphicsDevice), 7, Color.Red)
                    {
                        Position = new Vector3(30,15,0),
                        Rotation = new Vector3(0,45,0)
                    },
                    new  Cylinder(graphics, new BasicEffect(graphics.GraphicsDevice), Color.Wheat, 11, 11, 15),
                    Content.LoadXnaModel("Ship2"),
                },
                Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.GraphicsDevice.Viewport.AspectRatio, 1, 1000)
            });
            manager.StateManager.SetCurrentState(States.Main);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            manager.Update(gameTime);

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

            manager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
