using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace XnaGameLib
{
	public delegate void TimerEventHandler(object sender, TimerEventArgs e);

	public class UpdateTimer : IUpdatable
	{
		public event TimerEventHandler TimerFired;

		private UpdateTimer.Type _type;
		private long _ticksElapsed;
		private long _ticksUntilFire;
		private long _ticksInPeriod;
		private bool _isRunning;

		public UpdateTimer(long ticks, UpdateTimer.Type type, TimerEventHandler timeEventHandler = null)
		{
			Debug.Assert(ticks >= 0);
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
						Stop();
					}

					OnTimerFired();
				}
			}
		}

		public void Stop()
		{
			_isRunning = false;
			_ticksUntilFire = 0;
		}

		public bool IsRunning()
		{
			return _isRunning;
		}

		public bool IsRunningSlowly()
		{
			return IsRunning() && _ticksUntilFire <= 0;
		}

		public TimeSpan TimeLeft()
		{
			return new TimeSpan(_ticksUntilFire);
		}

		public long TicksLeft()
		{
			return _ticksUntilFire;
		}

		private void OnTimerFired()
		{
			if (TimerFired != null)
			{
				TimerFired(this, new TimerEventArgs(new TimeSpan(_ticksElapsed)));
			}
		}

		public enum Type
		{
			Once,
			Repeating
		}
	}

	public class TimerEventArgs : EventArgs
	{
		public TimeSpan ElapsedTime { get; protected set; }

		public TimerEventArgs(TimeSpan elapsedTime)
		{
			ElapsedTime = elapsedTime;
		}
	}
}
