using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GK3D.GUI
{
    public enum MouseButtons
    {
        Left,
        Middle,
        Right
    }
    public sealed class InputHandler
    {
        public sealed class InputHandlerUpdate : IDisposable
        {
            public void Dispose()
            {
                End();
            }
        }
        private static void Begin()
        {

        }

        private static void End()
        {
            foreach (var item in keyClicked.Values)
                item.End();
        }

        public static readonly InputHandlerUpdate HandlerUpdate = new InputHandlerUpdate();
        [ThreadStatic]
        public static Predicate<Point> IsPointInGameWindow = (p) => true;
        static Dictionary<Enum, ClickKeyState> keyClicked = new Dictionary<Enum, ClickKeyState>();
        public static bool CheckKeyClick(Enum key)
        {
            if (!keyClicked.ContainsKey(key))
                keyClicked[key] = GetClickKeyState(key);
            return keyClicked[key].IsKeyClick();
        }

        private static ClickKeyState GetClickKeyState(Enum key)
        {
            Type keyType = key.GetType();
            if (keyType == typeof(Keys))
                return new ClickKeyState(key, (k) => Keyboard.GetState().IsKeyDown((Keys)k));
            if (keyType == typeof(MouseButtons))
                return new ClickKeyState(key, (k) => IsMouseButtonDown((MouseButtons)k));
            throw new ArgumentException($"{keyType} not supported");
        }

        private static bool IsMouseButtonDown(MouseButtons button)
        {
            MouseState mouseState = Mouse.GetState();
            if (IsPointInGameWindow?.Invoke(mouseState.Position) == false)
                return false;

            switch (button)
            {
                case MouseButtons.Left:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Middle:
                    return mouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.Right:
                    return mouseState.RightButton == ButtonState.Pressed;
                default:
                    break;
            }
            return false;
        }

        private class ClickKeyState
        {
            private bool _clicked = false;
            private bool newVal = false;
            private bool result;
            private Enum key;
            private Predicate<Enum> IsKeyDown;
            public ClickKeyState(Enum k, Predicate<Enum> isKeyDown)
            {
                key = k;
                IsKeyDown = isKeyDown;
            }
            public bool IsKeyClick()
            {
                if (newVal)
                    return result;

                bool res = _clicked;
                _clicked = IsKeyDown(key);
                newVal = true;
                result = res && !_clicked;
                return result;
            }
            public void End() { newVal = false; }
        }
    }
}
