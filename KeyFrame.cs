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

		public Color this[int x, int y]
		{
			get
			{
				Debug.Assert(x >= 0);
				Debug.Assert(x < Source.Width);
				Debug.Assert(y >= 0);
				Debug.Assert(y < Source.Height);

				if (_frameData == null)
				{
					_frameData = GetFrameData();
				}

				return _frameData[x, y];
			}
		}

		private Color[,] _frameData;

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
			_frameData = null;
		}

		private Color[,] GetFrameData()
		{
            Color[] frameData1D = new Color[Source.Width * Source.Height];
			Texture.GetData(frameData1D, Source.Y * Texture.Width + Source.X, frameData1D.Length);

            Color[,] frameData2D = new Color[Source.Width, Source.Height];
            for (int x = 0; x < Source.Width; x++)
                for (int y = 0; y < Source.Height; y++)
                    frameData2D[x, y] = frameData1D[x + y * Source.Width];

            return frameData2D;
		}
	}
}

