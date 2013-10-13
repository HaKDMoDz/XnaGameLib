using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaGameLib
{
    public class GamePadInput : IUpdatable
    {
        private GamePadState[] gamePadStates;
        private GamePadState[] lastGamePadStates;

        public GamePadInput()
        {
            lastGamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                lastGamePadStates[(int)index] = GamePad.GetState(index);
			gamePadStates = (GamePadState[]) lastGamePadStates.Clone();
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
			lastGamePadStates = (GamePadState[]) gamePadStates.Clone();
			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex))) {
				gamePadStates [(int)index] = GamePad.GetState(index);
			}
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
    }
}
