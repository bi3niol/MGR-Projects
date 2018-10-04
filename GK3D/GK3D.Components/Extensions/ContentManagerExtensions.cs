using GK3D.Components.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components
{
    public static class ContentManagerExtensions
    {
        public static XnaModel LoadXnaModel(this ContentManager manager, string assetName)
        {
            Model asset = manager.Load<Model>(assetName);
            if (asset == null) return null;
            return new XnaModel(asset);
        }
    }
}
