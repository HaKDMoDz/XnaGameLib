using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class MouseInput : IUpdatable
    {
		private MouseState _mouseState;
		private MouseState _lastMouseState;

        public MouseInput()
        {
			_lastMouseState = Mouse.GetState();
			_mouseState = _lastMouseState;
        }

        public MouseState MouseState
        {
            get { return _mouseState; }
        }

        public MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }

        public void Update(GameTime gameTime)
		{
			_lastMouseState = _mouseState;
			_mouseState = Mouse.GetState();
		}

		public bool ButtonUp(MouseButtons button)
        {
			return ButtonIs(button, ButtonState.Released);
        }

		public bool ButtonDown(MouseButtons button)
        {
			return ButtonIs(button, ButtonState.Pressed);
        }

        public bool ButtonReleased(MouseButtons button)
        {
			return ButtonIs(button, ButtonState.Pressed, ButtonState.Released);
        }

        public bool ButtonPressed(MouseButtons button)
        {
            return ButtonIs(button, ButtonState.Released, ButtonState.Pressed);
        }

		public void Reset()
        {
			_lastMouseState = _mouseState;
        }

		private bool ButtonIs(MouseButtons button, ButtonState state)
		{
			switch (button)
			{
			case MouseButtons.LeftButton:
				return _mouseState.LeftButton == state;
			case MouseButtons.RightButton:
				return _mouseState.RightButton == state;
			case MouseButtons.MiddleButton:
				return _mouseState.MiddleButton == state;
			default:
				return false;
			}
		}

		private bool ButtonIs(MouseButtons button, ButtonState lastState, ButtonState state)
		{
			switch (button)
			{
			case MouseButtons.LeftButton:
				return _lastMouseState.LeftButton == lastState && _mouseState.LeftButton == state;
			case MouseButtons.RightButton:
				return _lastMouseState.RightButton == lastState && _mouseState.RightButton == state;
			case MouseButtons.MiddleButton:
				return _lastMouseState.MiddleButton == lastState && _mouseState.MiddleButton == state;
			default:
				return false;
			}
		}
    }
}
