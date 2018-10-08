using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GK3D.Components
{
    public interface IGameObject
    {
        bool IsActive { get; set; }
        void AddComponent(IComponet componet);
        TComponent GetComponet<TComponent>() where TComponent : IComponet;
        TComponent[] GetComponets<TComponent>() where TComponent : IComponet;
        void RemoveComponent(IComponet componet);
        void RemoveComponents<TComponent>() where TComponent : IComponet;
        void Update(GameTime gameTime);
    }
}