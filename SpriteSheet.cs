using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XnaGameLib
{
	public class SpriteSheet
	{
		private Texture2D texture;
		private int rectangleWidth;
		private int rectangleHeight;

		public SpriteSheet(Texture2D texture, int rows, int columns)
		{
			Debug.Assert(texture != null);
			Debug.Assert(rows > 0);
			Debug.Assert(columns > 0);
			Debug.Assert(texture.Height % rows == 0);
			Debug.Assert(texture.Width % columns == 0);
			this.texture = texture;
			rectangleWidth = texture.Width / columns;
			rectangleHeight = texture.Height / rows;
		}

		public Rectangle this[int row, int column] {
			get
			{
				Debug.Assert(row >= 0);
				Debug.Assert(column >= 0);
				return new Rectangle(column * rectangleWidth, row * rectangleHeight,
			                         rectangleWidth, rectangleHeight);
			}
		}
	}
}

