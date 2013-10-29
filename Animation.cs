using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XnaGameLib
{
	public class Animation : IUpdatable
	{
		public AnimationState State { get; protected set; }

		private static readonly Random _Random = new Random();

		private AnimationState _lastState;
		private List<KeyFrame> _keyFrames;
		private UpdateTimer _timer;
		private long _ticksToNextFrame;
		private int _index;

		public Animation(List<KeyFrame> keyFrames)
		{
			Debug.Assert(keyFrames != null);
			Debug.Assert(keyFrames.Count > 0);
			_keyFrames = keyFrames;
			State = AnimationState.Stopped;
			_ticksToNextFrame = 0;
			_index = 0;
		}

		public void Play(bool randomStartFrame = false)
		{
			_index = (randomStartFrame ? _Random.Next(_keyFrames.Count) : 0);
			_timer = new UpdateTimer(_keyFrames[_index].Duration, UpdateTimer.Type.Once, TimerFired);
			_ticksToNextFrame = _keyFrames[_index].Duration.Ticks;
			State = AnimationState.Playing;
		}

		public void Loop(bool randomStartFrame)
		{
			Play(randomStartFrame);
			State = AnimationState.Looping;
		}

		public void Resume()
		{
			if (State == AnimationState.Paused)
			{
				State = _lastState;
			}
		}

		public void Pause()
		{
			if (State == AnimationState.Playing || State == AnimationState.Looping)
			{
				_lastState = State;
				State = AnimationState.Paused;
			}
		}

		public void Stop()
		{
			State = AnimationState.Stopped;
			_ticksToNextFrame = _keyFrames[0].Duration.Ticks;
			_index = 0;
		}

		public void Update(GameTime gameTime)
		{
			if (State == AnimationState.Playing || State == AnimationState.Looping)
			{
				_timer.Update(gameTime);
			}
		}

		public KeyFrame GetCurrentKeyFrame()
		{
			return _keyFrames[_index];
		}

		public Color[,] FrameData()
		{
			return _keyFrames[_index].FrameData();
		}

		private void TimerFired(object sender, TimerEventArgs args)
		{
			long ticksElapsed = args.ElapsedTime.Ticks;
			if (State == AnimationState.Looping)
			{
				LoopToNextFrame(ticksElapsed);
			}
			else
			{
				PlayToNextFrame(ticksElapsed);
			}
		}

		private void LoopToNextFrame(long ticksElapsed)
		{
			Debug.Assert(ticksElapsed >= 0);
			while (ticksElapsed >= _ticksToNextFrame)
			{
				ticksElapsed -= _ticksToNextFrame;
				_index = (_index + 1) % _keyFrames.Count;
				_ticksToNextFrame = _keyFrames[_index].Duration.Ticks;
			}

			_ticksToNextFrame -= ticksElapsed;
			_timer = new UpdateTimer(_ticksToNextFrame, UpdateTimer.Type.Once, TimerFired);
		}

		private void PlayToNextFrame(long ticksElapsed)
		{
			Debug.Assert(ticksElapsed >= 0);
			while (ticksElapsed >= _ticksToNextFrame && _index < _keyFrames.Count)
			{
				ticksElapsed -= _ticksToNextFrame;

				if (++_index < _keyFrames.Count)
				{
					_ticksToNextFrame = _keyFrames[_index].Duration.Ticks;
				}
			}

			if (_index < _keyFrames.Count)
			{
				_ticksToNextFrame -= ticksElapsed;
				_timer = new UpdateTimer(_ticksToNextFrame, UpdateTimer.Type.Once, TimerFired);
			}
			else
			{
				Stop();
			}
		}

		public enum AnimationState
		{
			Stopped,
			Paused,
			Playing,
			Looping
		}
	}
}

