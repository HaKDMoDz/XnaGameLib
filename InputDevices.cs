using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class InputDevices : IUpdatable
    {
        private KeyboardState _keyboardState;
        private KeyboardState _lastKeyboardState;
		private MouseState _mouseState;
		private MouseState _lastMouseState;
        private GamePadState[] _gamePadStates;
        private GamePadState[] _lastGamePadStates;

        public InputDevices()
        {
            _keyboardState = Keyboard.GetState();
			_mouseState = Mouse.GetState();
            _gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                _gamePadStates[(int)index] = GamePad.GetState(index);
        }

        public KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public MouseState MouseState
        {
            get { return _mouseState; }
        }

        public MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }

        public GamePadState[] GamePadStates
        {
            get { return _gamePadStates; }
        }

        public GamePadState[] LastGamePadStates
        {
            get { return _lastGamePadStates; }
        }

        public void Update(GameTime gameTime)
		{
			_lastKeyboardState = _keyboardState;
			_keyboardState = Keyboard.GetState();

			_lastMouseState = _mouseState;
			_mouseState = Mouse.GetState();

			_lastGamePadStates = (GamePadState[])_gamePadStates.Clone();
			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex))) {
				_gamePadStates [(int)index] = GamePad.GetState(index);
			}
        }

        public bool KeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }

		public bool KeyUp(Keys key)
        {
            return _keyboardState.IsKeyUp(key);
        }

        public bool KeyReleased(Keys key)
        {
			return _lastKeyboardState.IsKeyDown(key) && _keyboardState.IsKeyUp(key);
        }

        public bool KeyPressed(Keys key)
        {
			return _lastKeyboardState.IsKeyUp(key) && _keyboardState.IsKeyDown(key);
        }

		public bool ButtonUp(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonUp(button);
        }

		public bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonDown(button);
        }

        public bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonUp(button) &&
                _lastGamePadStates[(int)index].IsButtonDown(button);
        }

        public bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return _gamePadStates[(int)index].IsButtonDown(button) &&
                _lastGamePadStates[(int)index].IsButtonUp(button);
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
            _lastKeyboardState = _keyboardState;
			_lastMouseState = _mouseState;
			_lastGamePadStates = (GamePadState[]) _gamePadStates.Clone();
        }

		private bool MouseButtonIs(MouseButtons button, ButtonState state)
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

		private bool MouseButtonIs(MouseButtons button, ButtonState lastState, ButtonState state)
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
