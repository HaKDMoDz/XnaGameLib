using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;

namespace XnaGameLib
{
    public static class AnimationHelper
    {
        public static Animation CreateAnimation(Texture2D texture, int rows, int columns,
                                                int frames, TimeSpan frameDuration, Order order)
        {
            SpriteSheet sheet = new SpriteSheet(texture, rows, columns);
            List<KeyFrame> keyFrames = new List<KeyFrame>();
            int k = 0;

            switch (order)
            {
            case Order.Column:
                for (int col = 0; col < columns; ++col)
                {
                    for (int row = 0; row < rows && k < frames; ++row, ++k)
                    {
                        keyFrames.Add(new KeyFrame(texture, sheet[row, col], frameDuration));
                    }
                }
                break;
            case Order.Row:
                for (int row = 0; row < rows; ++row)
                {
                    for (int col = 0; col < columns && k < frames; ++col, ++k)
                    {
                        keyFrames.Add(new KeyFrame(texture, sheet[row, col], frameDuration));
                    }
                }
                break;
            default:
                throw new InvalidEnumArgumentException();
            }

            return new Animation(keyFrames);
        }

        public enum Order
        {
            Row,
            Column
        }
    }
}

