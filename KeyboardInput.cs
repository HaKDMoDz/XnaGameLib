using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class KeyboardInput : IUpdatable
    {
        private KeyboardState _keyboardState;
        private KeyboardState _lastKeyboardState;

        public KeyboardInput()
        {
            _lastKeyboardState = Keyboard.GetState();
            _keyboardState = _lastKeyboardState;
        }

        public KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public void Update(GameTime gameTime)
		{
			_lastKeyboardState = _keyboardState;
			_keyboardState = Keyboard.GetState();
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
    }
}
