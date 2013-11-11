using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace XnaGameLib
{
    public class PlayerInput<T> : IUpdatable
    {
        private KeyboardInput _keyboard;
        private GamePadInput _gamePad;
        private MouseInput _mouse;
        private Dictionary<T, Keys> _actionKeys = new Dictionary<T, Keys>();
        private Dictionary<T, Buttons> _actionButtons = new Dictionary<T, Buttons>();
        private Dictionary<T, MouseButtons> _actionMouseButtons = new Dictionary<T, MouseButtons>();
        private delegate bool KeyTest(Keys key);

        private delegate bool ButtonTest(Buttons button,PlayerIndex index);

        private delegate bool MouseButtonTest(MouseButtons mouseButton);

        public PlayerInput(KeyboardInput keyboard, GamePadInput gamePad, MouseInput mouse)
        {
            _keyboard = keyboard;
            _gamePad = gamePad;
            _mouse = mouse;
        }

        public void SetKey(T action, Keys key)
        {
            _actionKeys[action] = key;
        }

        public void SetButton(T action, Buttons button)
        {
            _actionButtons[action] = button;
        }

        public void SetMouseButton(T action, MouseButtons mouseButtons)
        {
            _actionMouseButtons[action] = mouseButtons;
        }

        public void Update(GameTime gameTime)
        {
            _keyboard.Update(gameTime);
            _gamePad.Update(gameTime);
            _mouse.Update(gameTime);
        }

        public bool InputDown(T action, PlayerIndex index = PlayerIndex.One)
        {
            return InputTest(action, _keyboard.KeyDown, _gamePad.ButtonDown, _mouse.ButtonDown, index);
        }

        public bool InputUp(T action, PlayerIndex index = PlayerIndex.One)
        {
            return InputTest(action, _keyboard.KeyUp, _gamePad.ButtonUp, _mouse.ButtonUp, index);
        }

        public bool InputReleased(T action, PlayerIndex index = PlayerIndex.One)
        {
            return InputTest(action, _keyboard.KeyReleased, _gamePad.ButtonReleased, _mouse.ButtonReleased, index);
        }

        public bool InputPressed(T action, PlayerIndex index = PlayerIndex.One)
        {
            return InputTest(action, _keyboard.KeyPressed, _gamePad.ButtonPressed, _mouse.ButtonPressed, index);
        }

        private bool InputTest(T action, KeyTest keyTest, ButtonTest buttonTest,
                               MouseButtonTest mouseButtonTest, PlayerIndex index = PlayerIndex.One)
        {
            Keys key;
            if (_actionKeys.TryGetValue(action, out key) && keyTest(key))
            {
                return true;
            }

            Buttons button;
            if (_actionButtons.TryGetValue(action, out button) && buttonTest(button, index))
            {
                return true;
            }

            MouseButtons mouseButton;
            if (_actionMouseButtons.TryGetValue(action, out mouseButton) && mouseButtonTest(mouseButton))
            {
                return true;
            }

            return false;
        }
    }
}
