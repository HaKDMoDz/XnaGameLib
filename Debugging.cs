using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
	public class Debugging
	{
		private static Texture2D _BoundingCircleTexture;

		[Conditional("DEBUG")]
        public static void LoadContent(ContentManager content, string boundingCircleAssetName)
        {
			_BoundingCircleTexture = content.Load<Texture2D>(boundingCircleAssetName);
        }

		[Conditional("DEBUG")]
		public void DrawBoundingCircle(SpriteBatch spriteBatch, BoundingCircle circle)
		{
			Rectangle textureBounds = _BoundingCircleTexture.Bounds;
			Vector2 origin = new Vector2(textureBounds.Width / 2, textureBounds.Height / 2);
			float scale = (circle.Radius * 2.0f) / _BoundingCircleTexture.Width;
			spriteBatch.Draw(_BoundingCircleTexture, circle.Position, textureBounds,
			                 Color.White, 0, origin, scale, SpriteEffects.None, 1.0f);
		}
	}
}

