using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameLib
{
	public class PlayerInput<T> : IUpdatable
	{
		private KeyboardInput keyboard;
		private GamePadInput gamePad;
		private MouseInput mouse;
		private Dictionary<T, Keys> actionKeys = new Dictionary<T, Keys>();
		private Dictionary<T, Buttons> actionButtons = new Dictionary<T, Buttons>();
		private Dictionary<T, MouseButtons> actionMouseButtons = new Dictionary<T, MouseButtons>();

		private delegate bool KeyTest(Keys key);
		private delegate bool ButtonTest(Buttons button, PlayerIndex index);
		private delegate bool MouseButtonTest(MouseButtons mouseButton);

		public PlayerInput(KeyboardInput keyboard, GamePadInput gamePad, MouseInput mouse)
		{
			this.keyboard = keyboard;
			this.gamePad = gamePad;
			this.mouse = mouse;
		}

		public void SetKey(T action, Keys key)
		{
			actionKeys[action] = key;
		}

		public void SetButton(T action, Buttons button)
		{
			actionButtons[action] = button;
		}

		public void SetMouseButton(T action, MouseButtons mouseButtons)
		{
			actionMouseButtons[action] = mouseButtons;
		}

		public void Update(GameTime gameTime)
		{
			keyboard.Update(gameTime);
			gamePad.Update(gameTime);
			mouse.Update(gameTime);
		}

        public bool InputOn(T action, PlayerIndex index = PlayerIndex.One)
		{
			return InputTest(action, keyboard.KeyDown, gamePad.ButtonDown, mouse.ButtonDown, index);
        }

		public bool InputOff(T action, PlayerIndex index = PlayerIndex.One)
        {
			return InputTest(action, keyboard.KeyUp, gamePad.ButtonUp, mouse.ButtonUp, index);
        }

        public bool InputDeactivated(T action, PlayerIndex index = PlayerIndex.One)
        {
			return InputTest(action, keyboard.KeyReleased, gamePad.ButtonReleased, mouse.ButtonReleased, index);
        }

        public bool InputActivated(T action, PlayerIndex index = PlayerIndex.One)
        {
			return InputTest(action, keyboard.KeyPressed, gamePad.ButtonPressed, mouse.ButtonPressed, index);
        }

		private bool InputTest(T action, KeyTest keyTest, ButtonTest buttonTest,
		                       MouseButtonTest mouseButtonTest, PlayerIndex index = PlayerIndex.One)
		{
			Keys key;
			if (actionKeys.TryGetValue(action, out key)) {
				if (keyTest(key)) {
					return true;
				}
			}

			Buttons button;
			if (actionButtons.TryGetValue(action, out button)) {
				if (buttonTest(button, index)) {
					return true;
				}
			}

			MouseButtons mouseButton;
			if (actionMouseButtons.TryGetValue(action, out mouseButton)) {
				if (mouseButtonTest(mouseButton)) {
					return true;
				}
			}

			return false;
		}
	}
}

