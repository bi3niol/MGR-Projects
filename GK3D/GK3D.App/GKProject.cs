using GK3D.Components;
using GK3D.Components.Components;
using GK3D.Components.Game;
using GK3D.Components.Models;
using GK3D.Components.SceneObjects;
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
            var light = new Light
            {
                Color = new Color(255, 255, 255, 255),
                Type = GK3D.Components.SceneObjects.LightType.Directional,
                Direction = new Vector3(-1, -1, 0),
                KDiffuse = 0.8f,
                Power = 1f,
                KSpecular = 0.5f
            };
            effect.AddLight(light);
            var shipTexture = Content.Load<Texture2D>("Rocket_Ship_v1_L3.123c485c9e1d-6d02-47cf-b751-9606e55c8fa1/10475_Rocket_Ship_v1_Diffuse");
            var spaceship1 = Content.LoadXnaModel("Rocket_Ship_v1_L3.123c485c9e1d-6d02-47cf-b751-9606e55c8fa1/10475_Rocket_Ship_v1_L3", effect);
            var spaceship2 = Content.LoadXnaModel("Rocket_Ship_v1_L3.123c485c9e1d-6d02-47cf-b751-9606e55c8fa1/10475_Rocket_Ship_v1_L3", effect);
            var satelite2 = Content.LoadXnaModel("Satellite", effect);
            var satelite3 = Content.LoadXnaModel("Satellite", effect);
            spaceship1.Texture = shipTexture;
            spaceship2.Texture = shipTexture;


            var planet = new Sphere(graphics, effect, new Color(26,86,216,255), 20, 40, 40);
            Cube cube1 = new Cube(graphics, effect, 3, Color.DeepPink);
            cube1.Position = new Vector3(0, 0, planet.Radius);
            cube1.Scale = new Vector3(0.5f);
            cube1.Rotation = new Vector3(30f);

            Cylinder cylinder1 = new Cylinder(graphics, effect, new Color(75, 198, 13, 255), 1.5f, 4, 6);
            cylinder1.Rotation = new Vector3(90, 0, 30);
            var pos = new Vector3(60, 15, -15);
            pos.Normalize();
            pos *= planet.Radius;
            cylinder1.Position = pos;

            spaceship1.Effect = effect;
            spaceship1.Scale = new Vector3(0.02f);
            spaceship1.Position = new Vector3(6, -(planet.Radius + 10), -8);

            spaceship2.Effect = effect;
            spaceship2.Scale = new Vector3(0.02f);
            spaceship2.Position = new Vector3(-(planet.Radius + 6), planet.Radius, 10);

            satelite2.Effect = effect;
            satelite2.Scale = new Vector3(5, 5, 5);
            satelite2.Position = new Vector3(planet.Radius + 6, planet.Radius, 0);

            satelite3.Effect = effect;
            satelite3.Scale = new Vector3(5, 5, 5);
            satelite3.Position = new Vector3(10, -6, planet.Radius + 10);

            var connector = new Cylinder(graphics, effect, Color.Red, 1, 6, 14)
            {
                Position = new Vector3(0, 20, 0),
                Rotation = new Vector3(0, 0, 1.6f)
            };

            var dir = (connector.Position - satelite2.Position);
            dir.Normalize();
            Light lightSat2 = new Light()
            {
                Color = new Color(100, 50, 200, 255),
                Type = GK3D.Components.SceneObjects.LightType.Spot,
                Position = satelite2.Position,
                Direction = dir,
                KDiffuse = 0.8f,
                Power = 1f,
                KSpecular = 0.5f
            };

            var dir2 = (cube1.Position - satelite3.Position);
            dir2.Normalize();
            Light lightSat3 = new Light()
            {
                Color = new Color(175,216,26,255),
                Type = GK3D.Components.SceneObjects.LightType.Spot,
                Position = satelite3.Position,
                Direction = dir2,
                KDiffuse = 0.8f,
                Power = 1f,
                KSpecular = 0.5f
            };

            effect.AddLight(lightSat2);
            effect.AddLight(lightSat3);
            Camera camera = new Camera()
            {
                Position = new Vector3(0, 0, 60),
            };

            lightSat2.AddComponent(new LightAnimatorCommponent(lightSat2, effect));

            manager.StateManager.SetState(States.Main, new ProjectSceneState
            {
                Effect = effect,
                Camera = camera,
                GameObjects = new System.Collections.Generic.List<IGameObject>
                {
                    light,
                    cube1,
                    cylinder1,
                    lightSat2,
                    planet, //planet
                    new Sphere(graphics, effect, Color.White, 3,10,10)
                    {
                        Position = new Vector3(5,19,0),
                    },
                    connector,
                    new Sphere(graphics, effect, Color.White, 3,10,10)
                    {
                        Position = new Vector3(-5,19,0),
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
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
