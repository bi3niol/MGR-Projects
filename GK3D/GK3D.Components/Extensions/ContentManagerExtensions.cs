﻿using GK3D.Components.Models;
using GK3D.Components.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Components
{
    public static class ContentManagerExtensions
    {
        public static XnaModel LoadXnaModel(this ContentManager manager, string assetName, SimpleEffect effect)
        {
            Model asset = manager.Load<Model>(assetName);
            if (asset == null) return null;
            return new XnaModel(effect, asset);
        }//EnvMappingModel

        public static EnvMappingModel LoadEnvMappingModel(this ContentManager manager, string assetName, SimpleEffect effect)
        {
            Model asset = manager.Load<Model>(assetName);
            if (asset == null) return null;
            return new EnvMappingModel(asset, effect);
        }//EnvMappingModel
    }
}
