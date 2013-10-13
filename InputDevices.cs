using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class InputDevices : IUpdatable
    {
        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;
		private MouseState mouseState;
		private MouseState lastMouseState;
        private GamePadState[] gamePadStates;
        private GamePadState[] lastGamePadStates;

        public InputDevices()
        {
            keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();
            gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                gamePadStates[(int)index] = GamePad.GetState(index);
        }

        public KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        public MouseState MouseState
        {
            get { return mouseState; }
        }

        public MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        public GamePadState[] GamePadStates
        {
            get { return gamePadStates; }
        }

        public GamePadState[] LastGamePadStates
        {
            get { return lastGamePadStates; }
        }

        public void Update(GameTime gameTime)
		{
			lastKeyboardState = keyboardState;
			keyboardState = Keyboard.GetState();

			lastMouseState = mouseState;
			mouseState = Mouse.GetState();

			lastGamePadStates = (GamePadState[])gamePadStates.Clone();
			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex))) {
				gamePadStates [(int)index] = GamePad.GetState(index);
			}
        }

        public bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

		public bool KeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public bool KeyReleased(Keys key)
        {
			return lastKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
        }

        public bool KeyPressed(Keys key)
        {
			return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

		public bool ButtonUp(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonUp(button);
        }

		public bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button);
        }

        public bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonUp(button) &&
                lastGamePadStates[(int)index].IsButtonDown(button);
        }

        public bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button) &&
                lastGamePadStates[(int)index].IsButtonUp(button);
        }

		public bool MouseButtonUp(MouseButtons button)
        {
			return MouseButtonIs(button, ButtonState.Released);
        }

		public bool MouseButtonDown(MouseButtons button)
        {
			return MouseButtonIs(button, ButtonState.Pressed);
        }

        public bool MouseButtonReleased(MouseButtons button)
        {
			return MouseButtonIs(button, ButtonState.Pressed, ButtonState.Released);
        }

        public bool MouseButtonPressed(MouseButtons button)
        {
            return MouseButtonIs(button, ButtonState.Released, ButtonState.Pressed);
        }

		public void Reset()
        {
            lastKeyboardState = keyboardState;
			lastMouseState = mouseState;
			lastGamePadStates = (GamePadState[]) gamePadStates.Clone();
        }

		private bool MouseButtonIs(MouseButtons button, ButtonState state)
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

		private bool MouseButtonIs(MouseButtons button, ButtonState lastState, ButtonState state)
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
