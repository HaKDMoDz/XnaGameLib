using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XnaGameLib
{
	public struct KeyFrame
	{
		public Texture2D Texture { get; private set; }
		public Rectangle Source { get; private set; }
		public TimeSpan Duration { get; private set; }

		public KeyFrame(Texture2D texture, Rectangle source, TimeSpan duration)
		  : this()
		{
			Debug.Assert(texture != null);
			Debug.Assert(duration.Ticks > 0);
			Debug.Assert(source.Width > 0);
			Debug.Assert(source.Height > 0);
			Debug.Assert(source.X >= 0);
			Debug.Assert(source.X < texture.Width);
			Debug.Assert(source.Y >= 0);
			Debug.Assert(source.Y < texture.Height);
			Texture = texture;
		    Source = source;
			Duration = duration;
		}

		public Color[,] FrameData()
        {
            Color[] colors1D = new Color[Source.Width * Source.Height];
			Texture.GetData(colors1D, Source.Y * Texture.Width + Source.X, colors1D.Length);

            Color[,] colors2D = new Color[Source.Width, Source.Height];
            for (int x = 0; x < Source.Width; x++)
                for (int y = 0; y < Source.Height; y++)
                    colors2D[x, y] = colors1D[x + y * Source.Width];

            return colors2D;
        }
	}
}

