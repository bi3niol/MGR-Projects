using GK3D.Components;
using GK3D.Components.Game;
using GK3D.Components.Models;
using GK3D.Components.Shaders;
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

        SimpleEffect effect;

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
            effect = new SimpleEffect(graphics, Content.Load<Effect>("Shaders/Simple"));
            effect.AddLight(new GK3D.Components.SceneObjects.Light
            {
                Color = Color.White,
                Type = GK3D.Components.SceneObjects.LightType.Directional,
                Direction = new Vector3(1,1,0),
                KDiffuse = 0.8f,
                Power = 1f,
                KSpecular = 0.5f
            });
            var spaceship1 = Content.LoadXnaModel("Rocket_Ship_v1_L3.123c485c9e1d-6d02-47cf-b751-9606e55c8fa1/10475_Rocket_Ship_v1_L3", effect);
            var spaceship2 = Content.LoadXnaModel("Rocket_Ship_v1_L3.123c485c9e1d-6d02-47cf-b751-9606e55c8fa1/10475_Rocket_Ship_v1_L3", effect);
            var satelite2 = Content.LoadXnaModel("Satellite", effect);
            var satelite3 = Content.LoadXnaModel("Satellite", effect);
            spaceship1.Effect = effect;
            spaceship1.Scale = new Vector3(0.02f);
            spaceship1.Position = new Vector3(25,20,0);
            spaceship2.Effect = effect;
            spaceship2.Scale = new Vector3(0.02f);
            spaceship2.Position = new Vector3(-25, 20, 0);
            satelite2.Effect = effect;
            satelite2.Scale = new Vector3(5, 5, 5);
            satelite2.Position = new Vector3(-25, 25, 0);
            satelite3.Effect = effect;
            satelite3.Scale = new Vector3(5, 5, 5);
            satelite3.Position = new Vector3(0, 0, 26);
            manager.StateManager.SetState(States.Main, new ProjectSceneState
            {
                Effect = effect,
                Camera = new Components.SceneObjects.Camera()
                {
                    Position = new Vector3(0, 0, 60),
                },
                Models = new System.Collections.Generic.List<IModel>
                {
                    new Sphere(graphics, effect, Color.LightGray, 20, 40, 40), //planet
                    new Sphere(graphics, effect, Color.White, 3,10,10)
                    {
                        Position = new Vector3(0,20,0),
                    },
                    new  Cylinder(graphics, effect, Color.Wheat, 1, 6, 14){
                        Position = new Vector3(2,20,0),
                        Rotation = new Vector3(0,0,90)
                    },
                    spaceship1,
                    spaceship2,
                    satelite2,
                    satelite3
                },
                Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.GraphicsDevice.Viewport.AspectRatio, 1, 500)
            });
            manager.StateManager.SetCurrentState(States.Main);
            Components.Add(manager);
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
        }
    }
}
