using System;
using GK3D.Components.Components;
using GK3D.Components.Game;
using Microsoft.Xna.Framework;

namespace GK3D.Components.SceneObjects
{
    public class Camera : GameObject
    {
        private Vector3 _position;
        public Vector3 Position
        {
            get => _position;
            set
            {
                if (_position == value)
                    return;
                _position = value;
                UpdateViewMatrix();
            }
        }

        private Matrix _coordinateMatrix = new Matrix()
        {
            Up = Vector3.Up,
            Right = Vector3.Right,
            Forward = Vector3.Forward
        };

        public Matrix CoordinateMatrix
        {
            get => _coordinateMatrix;
            private set
            {
                if (_coordinateMatrix == value) return;
                _coordinateMatrix = value;
                UpdateViewMatrix();
            }
        }

        public Vector3 Up => CoordinateMatrix.Up;
        public Vector3 Forward => CoordinateMatrix.Forward;
        public Vector3 Right => CoordinateMatrix.Right;


        private Vector3 _lookAt
        {
            get
            {
                return Position + Forward;
            }
        }


        private Matrix viewMAtrix;
        public Matrix ViewMatrix { get => viewMAtrix; set => viewMAtrix = value; }

        public Camera(bool addControllers = true)
        {
            if (addControllers)
            {
                AddComponent(new CameraControllerComponent(this));
                AddComponent(new CameraRotationControllerComponent(this));
            }
        }

        private void UpdateViewMatrix()
        {
            ViewMatrix = Matrix.CreateLookAt(_position, _lookAt, Up);
        }

        internal void Rotate(Vector3 rotation)
        {
            if (rotation == Vector3.Zero) return;
            rotation = Vector3.Transform(rotation, CoordinateMatrix);
            var rot = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
            CoordinateMatrix *= rot;
        }

        internal void MoveForward(Vector3 step)
        {
            if (step == Vector3.Zero) return;
            Position += Vector3.Transform(step, CoordinateMatrix);
        }
    }
}
