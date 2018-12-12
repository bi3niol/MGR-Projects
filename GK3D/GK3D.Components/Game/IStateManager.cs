using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Components.SceneObjects;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Game
{
    public interface IStateManager
    {
        void Draw(GameTime gameTime);
        void Draw(GameTime gameTime, Camera camera);
        IGameState CurrentState { get; }
        bool SetCurrentState(Enum stateId);
        bool SetState(Enum stateId, IGameState gameState);
        void Update(GameTime gameTime);
    }
}
