using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace XnaGameLib
{
	public class AnimatedSprite<T> : PhysicsObject, IDrawable
	{
		public SpriteEffects Effects { get; set; }
		public Vector2 TextureOrigin { get; set; }
		public Vector2 Scale { get; set; }
		public float Depth { get; set; }
		private Dictionary<T, Animation> animations;
		private Animation currentAnimation;

		public AnimatedSprite(Dictionary<T, Animation> animations, T defaultAnimationKey)
		{
			Debug.Assert(animations != null);
			Debug.Assert(animations.ContainsKey(defaultAnimationKey));
			this.animations = animations;
			currentAnimation = animations[defaultAnimationKey];
			Effects = SpriteEffects.None;
			TextureOrigin = Vector2.Zero;
			Scale = Vector2.One;
			Depth = 0;
		}

		public void Play(T animationKey)
		{
			Debug.Assert(animations.ContainsKey(animationKey));
			currentAnimation = animations[animationKey];
			currentAnimation.Play();
		}

		public void Loop(T animationKey, bool randomStartFrame = false)
		{
			Debug.Assert(animations.ContainsKey(animationKey));
			currentAnimation = animations[animationKey];
			currentAnimation.Loop(randomStartFrame);
		}

		public void Resume()
		{
			currentAnimation.Resume();
		}

		public void Pause()
		{
			currentAnimation.Pause();
		}

		public void Stop()
		{
			currentAnimation.Stop();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Debug.Assert(spriteBatch != null);
			KeyFrame kf = currentAnimation.GetCurrentKeyFrame();
			spriteBatch.Draw(kf.Texture, Position, kf.Source, Color.White, Angle, TextureOrigin, Scale, Effects, Depth);
		}

		public override void Update(GameTime gameTime)
		{
			Debug.Assert(gameTime != null);
			base.Update(gameTime);
			currentAnimation.Update(gameTime);
		}
	}
}

