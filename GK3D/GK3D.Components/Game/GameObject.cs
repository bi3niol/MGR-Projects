using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Game
{
    public class GameObject : IGameObject
    {
        protected List<IComponet> componets = new List<IComponet>();

        public bool IsActive { get; set; } = true;

        public void AddComponent(IComponet componet)
        {
            componets.Add(componet);
        }

        public TComponent GetComponet<TComponent>() where TComponent : IComponet
        {
            return componets.OfType<TComponent>().FirstOrDefault();
        }

        public TComponent[] GetComponets<TComponent>() where TComponent : IComponet
        {
            return componets.OfType<TComponent>().ToArray();
        }

        public void RemoveComponent(IComponet componet)
        {
            componets.Remove(componet);
        }

        public void RemoveComponents<TComponent>() where TComponent : IComponet
        {
            var comps = GetComponets<TComponent>();
            foreach (var c in comps)
                RemoveComponent(c);
        }
        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            foreach (var component in componets.OfType<IUpdateableComponent>())
                component.Update(gameTime);
        }
    }
}
