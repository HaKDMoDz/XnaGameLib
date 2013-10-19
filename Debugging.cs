using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace XnaGameLib
{
	public class Debugging
	{
		private static Texture2D _BoundingCircleTexture;
		private static SpriteFont _SpriteFont;

		private Stopwatch _stopwatch = Stopwatch.StartNew();
		private Queue<long> _updates = new Queue<long>();
		private Queue<long> _frames = new Queue<long>();

		[Conditional("DEBUG")]
        public static void LoadContent(ContentManager content, string boundingCircleImageName, string spriteFontName)
        {
			_BoundingCircleTexture = content.Load<Texture2D>(boundingCircleImageName);
			_SpriteFont = content.Load<SpriteFont>(spriteFontName);
        }

		[Conditional("DEBUG")]
		public void Update(GameTime gameTime)
		{
			_UpdateQueue(_updates, _stopwatch.ElapsedMilliseconds);
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

		[Conditional("DEBUG")]
		public void DrawFramesPerSecond(SpriteBatch spriteBatch, Vector2 position)
		{
			_UpdateQueue(_frames, _stopwatch.ElapsedMilliseconds);
			spriteBatch.DrawString(_SpriteFont, _frames.Count.ToString(), position, Color.Yellow,
			                       0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
		}

		[Conditional("DEBUG")]
		public void DrawUpdatesPerSecond(SpriteBatch spriteBatch, Vector2 position)
		{
			spriteBatch.DrawString(_SpriteFont, _updates.Count.ToString(), position, Color.Blue,
			                       0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
		}

		private static void _UpdateQueue(Queue<long> queue, long elapsedMilliseconds)
		{
			queue.Enqueue(elapsedMilliseconds);
			while (elapsedMilliseconds - queue.Peek() > 1000)
			{
				queue.Dequeue();
			}
		}
	}
}

