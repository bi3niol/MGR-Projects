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

        private Vector3 _up = Vector3.Up;
        public Vector3 Up
        {
            get => _up;
            set
            {
                if (_up == value)
                    return;
                _up = value;
                UpdateViewMatrix();
            }
        }

        private Vector3 _lookAt;
        public Vector3 LookAt
        {
            get => _lookAt;
            set
            {
                if (_lookAt == value)
                    return;
                _lookAt = value;
                UpdateViewMatrix();
            }
        }

        private Matrix viewMAtrix;
        public Matrix ViewMatrix { get => viewMAtrix; set => viewMAtrix = value; }

        public Camera()
        {
            AddComponent(new CameraControllerComponent(this));
        }

        private void UpdateViewMatrix()
        {
            ViewMatrix = Matrix.CreateLookAt(_position, _lookAt, _up);
        }
    }
}
