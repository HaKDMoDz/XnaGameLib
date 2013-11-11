using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XnaGameLib
{
    public static class TextureHelper
    {
        public static Color[,] ColorData(Texture2D texture, Rectangle source)
        {
            Debug.Assert(texture.Bounds.Contains(source));
            Color[] color1D = new Color[texture.Width * texture.Height];
            texture.GetData(color1D);

            Color[,] color2D = new Color[source.Width, source.Height];
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    color2D[x, y] = color1D[x + source.X + (y + source.Y) * texture.Width];
                }
            }

            return color2D;
        }

        public static Color[,] ColorData(Texture2D texture)
        {
            return TextureHelper.ColorData(texture, texture.Bounds);
        }

        public static byte[,] AlphaData(Texture2D texture, Rectangle source)
        {
            Debug.Assert(texture.Bounds.Contains(source));
            Color[] color1D = new Color[texture.Width * texture.Height];
            texture.GetData(color1D);

            byte[,] alpha2D = new byte[source.Width, source.Height];
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    alpha2D[x, y] = color1D[x + source.X + (y + source.Y) * texture.Width].A;
                }
            }

            return alpha2D;
        }

        public static byte[,] AlphaData(Texture2D texture)
        {
            return TextureHelper.AlphaData(texture, texture.Bounds);
        }
    }
}

