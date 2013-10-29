using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameLib
{
	public static class TextureHelper
	{
		public static Color[,] TextureData(Texture2D texture, Rectangle source)
        {
            Color[] colors1D = new Color[source.Width * source.Height];
			texture.GetData(colors1D, source.Y * texture.Width + source.X, colors1D.Length);

            Color[,] colors2D = new Color[source.Width, source.Height];
            for (int x = 0; x < source.Width; x++)
                for (int y = 0; y < source.Height; y++)
                    colors2D[x, y] = colors1D[x + y * source.Width];

            return colors2D;
        }

		public static Color[,] TextureData(Texture2D texture)
        {
			return TextureHelper.TextureData(texture, texture.Bounds);
        }
	}
}

