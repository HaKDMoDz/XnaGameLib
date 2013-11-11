using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public static class AnimatedSpriteHelper
    {
        public static AnimatedSprite<T> CreateAnimatedSprite<T>(Texture2D texture, int rows, int columns, int frames,
                                                                TimeSpan frameDuration, AnimationHelper.Order order,
                                                                Vector2 scale, Vector2 frameOrigin, T key)
        {
            Dictionary<T, Animation> animations = new Dictionary<T, Animation>();
            Animation animation = AnimationHelper.CreateAnimation(texture, rows, columns, frames, frameDuration, order);
            animations.Add(key, animation);
            AnimatedSprite<T> sprite = new AnimatedSprite<T>(animations, key);
            sprite.TextureOrigin = frameOrigin;
            sprite.Scale = scale;
            return sprite;
        }
    }
}

