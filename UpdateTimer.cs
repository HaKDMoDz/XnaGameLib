using System;
using Microsoft.Xna.Framework;

namespace XnaGameLib
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

		private long _ticksElapsed;
		private long _ticksUntilFire;
		private long _ticksInPeriod;
		private UpdateTimer.Type _type;
		private bool _isRunning;

		public UpdateTimer(long ticks, UpdateTimer.Type type, TimerEventHandler timeEventHandler = null)
		{
			_ticksElapsed = 0;
			_ticksUntilFire = ticks;
			_ticksInPeriod = ticks;
			_type = type;
			_isRunning = true;

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
				_ticksElapsed += gameTime.ElapsedGameTime.Ticks;
				_ticksUntilFire -= gameTime.ElapsedGameTime.Ticks;

				if (_ticksUntilFire <= 0)
				{
					if (_type == UpdateTimer.Type.Repeating)
					{
						_ticksUntilFire += _ticksInPeriod;
					} else
					{
						_isRunning = false;
					}

					OnTimerFired();
				}
			}
		}

		public bool IsRunning()
		{
			return _isRunning;
		}

		public bool IsRunningSlowly()
		{
			return IsRunning() && _ticksUntilFire <= 0;
		}

		private void OnTimerFired()
		{
			if (TimerFired != null)
			{
				TimerFired(this, new TimerEventArgs(new TimeSpan(_ticksElapsed)));
			}
		}
	}
}

