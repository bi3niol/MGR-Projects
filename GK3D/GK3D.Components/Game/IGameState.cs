﻿using Microsoft.Xna.Framework;

namespace GK3D.Components.Game
{
    public interface IGameState
    {
        IStateManager ParentManager { get; set; }
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}