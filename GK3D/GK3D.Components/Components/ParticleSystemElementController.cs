using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.Models;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Components
{
    public class ParticleSystemElementController : IUpdateableComponent
    {
        private BilboardModel bilboard;
        private double age = 0;

        private Action<BilboardModel, double, double> modifyParticleElement;

        public ParticleSystemElementController(BilboardModel bilboard, Action<BilboardModel, double, double> modifyParticleElement)
        {
            this.bilboard = bilboard;
            this.modifyParticleElement = modifyParticleElement;
        }

        public void Update(GameTime gameTime)
        {
            modifyParticleElement?.Invoke(bilboard, age, gameTime.ElapsedGameTime.TotalSeconds);
            age += gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
