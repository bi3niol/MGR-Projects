using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.Components;
using GK3D.Components.Models;
using GK3D.Components.SceneObjects;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components.ParticlesSystems
{
    public class SteamParticlesSystem : IUpdateableComponent, IRenderableComponent
    {
        private List<BilboardModel> bilboards = new List<BilboardModel>();
        private List<BilboardModel> bilboardsToRemove = new List<BilboardModel>();

        private int ParticlesPerSecond = 40;
        public TimeSpan ParticleLiveTime { get; set; } = new TimeSpan(0, 0, 6);
        private int MaxCountOfBilboardToRemove;

        private SimpleEffect effect;
        private Texture texture;

        private Random random = new Random();
        public Vector3 Position;
        public Camera Camera { get; set; }
        public SteamParticlesSystem(int particlesPerSecond, SimpleEffect effect, Texture texture, Camera camera)
        {
            ParticlesPerSecond = particlesPerSecond;
            this.effect = effect;
            this.texture = texture;
            MaxCountOfBilboardToRemove = 3 * ParticlesPerSecond;
            Camera = camera;
        }

        public void Draw(Matrix view, Matrix projection)
        {
            bilboards.ForEach(b => b.Draw(view, projection));
        }

        public void Update(GameTime gameTime)
        {
            bilboards.RemoveAll(b => bilboardsToRemove.Contains(b));
            bilboardsToRemove.Clear();
            bilboards.ForEach(b => b.Update(gameTime));
            int numberOfParticleToCreate = (int)Math.Max(1, ParticlesPerSecond * gameTime.ElapsedGameTime.TotalSeconds);
            for (int i = 0; i < numberOfParticleToCreate; i++)
            {
                Vector3 moveDir = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                moveDir.Normalize();

                BilboardModel b = new BilboardModel(effect, texture);
                b.Position = Position;
                bool removed = false;
                b.AddComponent(new ParticleSystemElementController(b,
                    (bb, age, deltatime) =>
                    {
                        bb.Position = bb.Position + moveDir * (float)deltatime;
                        if (age > ParticleLiveTime.TotalSeconds)
                        {
                            if (!removed)
                                bilboardsToRemove.Add(bb);
                            removed = true;
                        }
                    }));
                bilboards.Add(b);
            }
            bilboards.Sort((b1, b2) =>
            {
                return Vector3.Distance(b2.Position, Camera.Position) - Vector3.Distance(b1.Position, Camera.Position) <= 0 ? -1 : 1;
            });
        }
    }
}
