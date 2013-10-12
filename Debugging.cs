using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameLib
{
	public static class Debugging
	{
		private static Texture2D boundingCircleTexture;

		[Conditional("DEBUG")]
        public static void LoadContent(ContentManager content, string boundingCircleAssetName)
        {
			boundingCircleTexture = content.Load<Texture2D>(boundingCircleAssetName);
        }

		[Conditional("DEBUG")]
		public static void Draw(SpriteBatch spriteBatch, BoundingCircle circle)
		{
			Rectangle textureBounds = boundingCircleTexture.Bounds;
			Vector2 origin = new Vector2(textureBounds.Width / 2, textureBounds.Height / 2);
			float scale = (circle.Radius * 2.0f) / boundingCircleTexture.Width;
			spriteBatch.Draw(boundingCircleTexture, circle.Position, textureBounds,
			                 Color.White, 0, origin, scale, SpriteEffects.None, 1.0f);
		}
	}
}

