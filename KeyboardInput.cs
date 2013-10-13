using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class KeyboardInput : IUpdatable
    {
        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;

        public KeyboardInput()
        {
            lastKeyboardState = Keyboard.GetState();
            keyboardState = lastKeyboardState;
        }

        public KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        public void Update(GameTime gameTime)
		{
			lastKeyboardState = keyboardState;
			keyboardState = Keyboard.GetState();
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
    }
}
