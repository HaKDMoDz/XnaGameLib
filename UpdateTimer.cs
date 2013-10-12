using System;
using Microsoft.Xna.Framework;

namespace GameLib
{
	public class TimerEventArgs : EventArgs
	{
		public TimeSpan ElapsedTime { get; protected set; }

		public TimerEventArgs(TimeSpan elapsedTime)
		{
			ElapsedTime = elapsedTime;
		}
	}

	public delegate void TimerEventHandler(object sender, TimerEventArgs e);

	public class UpdateTimer : IUpdatable
	{
		public enum Type
		{
			Once,
			Repeating
		}

		public event TimerEventHandler TimerFired;

		private long ticksElapsed;
		private long ticksUntilFire;
		private long ticksInPeriod;
		private UpdateTimer.Type type;
		private bool isRunning;

		public UpdateTimer(long ticks, UpdateTimer.Type type, TimerEventHandler timeEventHandler = null)
		{
			ticksElapsed = 0;
			ticksUntilFire = ticks;
			ticksInPeriod = ticks;
			this.type = type;
			isRunning = true;

			if (timeEventHandler != null)
			{
				TimerFired += timeEventHandler;
			}
		}

		public UpdateTimer(TimeSpan period, UpdateTimer.Type type, TimerEventHandler timeEventHandler = null)
			: this(period.Ticks, type, timeEventHandler)
		{
		}

		public void Update(GameTime gameTime)
		{
			if (IsRunning())
			{
				ticksElapsed += gameTime.ElapsedGameTime.Ticks;
				ticksUntilFire -= gameTime.ElapsedGameTime.Ticks;

				if (ticksUntilFire <= 0)
				{
					if (type == UpdateTimer.Type.Repeating)
					{
						ticksUntilFire += ticksInPeriod;
					} else
					{
						isRunning = false;
					}

					OnTimerFired();
				}
			}
		}

		public bool IsRunning()
		{
			return isRunning;
		}

		public bool IsRunningSlowly()
		{
			return IsRunning() && ticksUntilFire <= 0;
		}

		private void OnTimerFired()
		{
			if (TimerFired != null)
			{
				TimerFired(this, new TimerEventArgs(new TimeSpan(ticksElapsed)));
			}
		}
	}
}

