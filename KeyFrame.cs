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
			Texture = texture;
		    Source = source;
			Duration = duration;
		}
	}
}

