using Microsoft.Xna.Framework;

namespace GK3D.Components
{
    public interface IUpdateableComponent : IComponet
    {
        void Update(GameTime gameTime);
    }
}