using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XnaGameLib
{
    public class SpriteSheet
    {
        private int _rectangleWidth;
        private int _rectangleHeight;

        public SpriteSheet(Texture2D texture, int rows, int columns)
        {
            Debug.Assert(texture != null);
            Debug.Assert(rows > 0);
            Debug.Assert(columns > 0);
            Debug.Assert(texture.Height % rows == 0);
            Debug.Assert(texture.Width % columns == 0);
            _rectangleWidth = texture.Width / columns;
            _rectangleHeight = texture.Height / rows;
        }

        public Rectangle this[int row, int column]
        {
            get
            {
                Debug.Assert(row >= 0);
                Debug.Assert(column >= 0);
                return new Rectangle(column * _rectangleWidth, row * _rectangleHeight,
                                     _rectangleWidth, _rectangleHeight);
            }
        }
    }
}

