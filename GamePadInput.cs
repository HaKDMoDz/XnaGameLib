using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class GamePadInput : IUpdatable
    {
        private GamePadState[] _gamePadStates;
        private GamePadState[] _lastGamePadStates;

        public GamePadInput()
        {
            _lastGamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                _lastGamePadStates[(int)index] = GamePad.GetState(index);
            _gamePadStates = (GamePadState[])_lastGamePadStates.Clone();
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
            _lastGamePadStates = (GamePadState[])_gamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                _gamePadStates[(int)index] = GamePad.GetState(index);
            }
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
    }
}
