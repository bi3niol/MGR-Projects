﻿using GK3D.Components.Game;
using Microsoft.Xna.Framework;

namespace GK3D.Components.Models
{
    public abstract class BaseModel : GameObject, IModel
    {
        public object Tag { get; set; }

        private Vector3 _rotation = new Vector3();
        public Vector3 Rotation
        {
            get => _rotation;
            set
            {
                if (_rotation == value) return;
                _rotation = value;
                UpdateWorldMatrix();
            }
        }

        private Vector3 _position = new Vector3();
        public Vector3 Position
        {
            get => _position;
            set
            {
                if (_position == value) return;
                _position = value;
                UpdateWorldMatrix();
            }
        } 

        private Vector3 _scale = Vector3.One;
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                if (_scale == value) return;
                _scale = value;
                UpdateWorldMatrix();
            }
        }

        private Matrix _world;
        public Matrix World
        {
            get => _world;
            private set => _world = value;
        }

        private void UpdateWorldMatrix()
        {
            World = Matrix.CreateScale(Scale) *
                Matrix.CreateRotationX(Rotation.X) *
                Matrix.CreateRotationY(Rotation.Y) *
                Matrix.CreateRotationZ(Rotation.Z) *
                Matrix.CreateTranslation(Position);
        }

        public BaseModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            UpdateWorldMatrix();
        }

        public abstract void Draw(Matrix view, Matrix projection);
    }
}
