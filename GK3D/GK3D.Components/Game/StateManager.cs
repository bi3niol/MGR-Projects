﻿using System;
using System.Collections.Generic;
using GK3D.Components.SceneObjects;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Game
{
    public class StateManager : IStateManager
    {
        protected Dictionary<Enum, IGameState> States = new Dictionary<Enum, IGameState>();
        private Enum _currrentStateId;

        public IGameState CurrentState => _currrentStateId == null ? null : States[_currrentStateId];

        public void Draw(GameTime gameTime)
        {
            CurrentState?.Draw(gameTime);
        }

        public void Draw(GameTime gameTime, Camera camera)
        {
            CurrentState?.Draw(gameTime, camera);
        }

        public bool SetCurrentState(Enum stateId)
        {
            if (States.ContainsKey(stateId))
            {
                _currrentStateId = stateId;
                return true;
            }

            return false;
        }

        public bool SetState(Enum stateId, IGameState gameState)
        {
            bool res = States.ContainsKey(stateId);

            States[stateId] = gameState;
            gameState.ParentManager = this;

            return res;
        }

        public void Update(GameTime gameTime)
        {
            CurrentState?.Update(gameTime);
        }
    }
}
