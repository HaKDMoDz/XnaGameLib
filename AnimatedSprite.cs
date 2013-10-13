using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace XnaGameLib
{
	public class AnimatedSprite<T> : PhysicsObject, IDrawable
	{
		// Should this be drawable? Implies we need position, rotation, etc. data. Sort of specific to model, but I guess it needn't be. Look at SFML interface.

		// Uses a sprite sheet? Plays animation, addressable with client supplied enum (like PlayerInput)
		// Animation, AnimatedSprite, SpriteSheet
		// KeyFrame: snapshot of animation: ref to sprite sheet, rectangle, and a timespan.
		// SpriteSheet: texture, ?, Is this useful?
			// Possibility: Texture indexed by [][], given a number of frames in two dimensions. Then [][] gets you the rect describing that frame.
			// Handy for manually creating keyframes. But after animations are created, just another level of detail.
			// Mainly a convenience for getting at rectangles. Maybe keyframe just refers to texture, and spritesheet exists separately as helper class.
		// Animation: list of keyframes. Anything else? 
			// Speed? Probably better to handle that in AnimatedSprite.play(anim, speed).
			// Maybe just list for now. But use it anyway for posterity.
		// Does this LOD enable anything?
			// Unless one has a dynamic animation system (animations change anything other than speed, e.g. keyframes, frequently), then probably not.
			// Still, have to represent animation somehow.
			// Animated sprite should have template arg for enum for naming animations.
			// Maybe two ways to create anim (i'd prefer one good way, and make the other way convenient to do in the good way)
				// 1. Create from Texture, fps, dim of rect for each frame, num frames in texture. Maybe this is the factory method. Internally maps to 2nd method.
				// 2. Create from Texture, List of keyframes.
			// Letting keyframe refer to a texture is a little too loose. Then a given animated sprite can refer to ANY number of textures. Maybe not so bad though.
			// Certainly is flexible, but is it needed? Does it hurt? Tying it to texture at least makes it possible to check rectangle validity.
			// It does cause a memory hit.
			// But it does represent a concept rather well...I'll go with it.

		// Need to work out what happens after animation has completed. Is there a default image? Do we just show frame 0 of the current animation? The last frame?

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

