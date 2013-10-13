using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class MouseInput : IUpdatable
    {
		private MouseState mouseState;
		private MouseState lastMouseState;

        public MouseInput()
        {
			lastMouseState = Mouse.GetState();
			mouseState = lastMouseState;
        }

        public MouseState MouseState
        {
            get { return mouseState; }
        }

        public MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        public void Update(GameTime gameTime)
		{
			lastMouseState = mouseState;
			mouseState = Mouse.GetState();
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
			lastMouseState = mouseState;
        }

		private bool ButtonIs(MouseButtons button, ButtonState state)
		{
			switch (button)
			{
			case MouseButtons.LeftButton:
				return mouseState.LeftButton == state;
			case MouseButtons.RightButton:
				return mouseState.RightButton == state;
			case MouseButtons.MiddleButton:
				return mouseState.MiddleButton == state;
			default:
				return false;
			}
		}

		private bool ButtonIs(MouseButtons button, ButtonState lastState, ButtonState state)
		{
			switch (button)
			{
			case MouseButtons.LeftButton:
				return lastMouseState.LeftButton == lastState && mouseState.LeftButton == state;
			case MouseButtons.RightButton:
				return lastMouseState.RightButton == lastState && mouseState.RightButton == state;
			case MouseButtons.MiddleButton:
				return lastMouseState.MiddleButton == lastState && mouseState.MiddleButton == state;
			default:
				return false;
			}
		}
    }
}
