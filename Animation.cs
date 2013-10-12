using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameLib
{
	public class Animation : IUpdatable
	{
		private static readonly Random random = new Random();
		public enum AnimationState
		{
			Stopped,
			Paused,
			Playing,
			Looping
		}

		public AnimationState State { get; protected set; }
		private AnimationState lastState;
		private List<KeyFrame> keyFrames;
		private UpdateTimer timer;
		private long ticksToNextFrame;
		private int index;

		public Animation(List<KeyFrame> keyFrames)
		{
			Debug.Assert(keyFrames != null);
			Debug.Assert(keyFrames.Count > 0);
			this.keyFrames = keyFrames;
			State = AnimationState.Stopped;
			ticksToNextFrame = 0;
			index = 0;
		}

		public void Play(bool randomStartFrame = false)
		{
			index = (randomStartFrame ? random.Next(keyFrames.Count) : 0);
			timer = new UpdateTimer(keyFrames[index].Duration, UpdateTimer.Type.Once, TimerFired);
			ticksToNextFrame = keyFrames[index].Duration.Ticks;
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
				State = lastState;
			}
		}

		public void Pause()
		{
			if (State == AnimationState.Playing || State == AnimationState.Looping)
			{
				lastState = State;
				State = AnimationState.Paused;
			}
		}

		public void Stop()
		{
			State = AnimationState.Stopped;
			ticksToNextFrame = keyFrames[0].Duration.Ticks;
			index = 0;
		}

		public void Update(GameTime gameTime)
		{
			if (State == AnimationState.Playing || State == AnimationState.Looping)
			{
				timer.Update(gameTime);
			}
		}

		public KeyFrame GetCurrentKeyFrame()
		{
			return keyFrames[index];
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
			while (ticksElapsed >= ticksToNextFrame)
			{
				ticksElapsed -= ticksToNextFrame;
				index = (index + 1) % keyFrames.Count;
				ticksToNextFrame = keyFrames[index].Duration.Ticks;
			}

			ticksToNextFrame -= ticksElapsed;
			timer = new UpdateTimer(ticksToNextFrame, UpdateTimer.Type.Once, TimerFired);
		}

		private void PlayToNextFrame(long ticksElapsed)
		{
			Debug.Assert(ticksElapsed >= 0);
			while (ticksElapsed >= ticksToNextFrame && index < keyFrames.Count)
			{
				ticksElapsed -= ticksToNextFrame;

				if (++index < keyFrames.Count)
				{
					ticksToNextFrame = keyFrames[index].Duration.Ticks;
				}
			}

			if (index < keyFrames.Count)
			{
				ticksToNextFrame -= ticksElapsed;
				timer = new UpdateTimer(ticksToNextFrame, UpdateTimer.Type.Once, TimerFired);
			}
			else
			{
				Stop();
			}
		}
	}
}

