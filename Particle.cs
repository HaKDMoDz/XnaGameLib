using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameLib
{
    public class Particle : PhysicsObject, IDrawable
    {
        public Texture2D Texture { get; set; }

        public Vector2 TextureOrigin { get; set; }

        public Vector2 Scale { get; set; }

        public Color Tint { get; set; }

        public double TimeToLive { get; set; }

        public Particle(Texture2D texture, Vector2 textureOrigin,
                        Vector2 scale, Color tint, double timeToLive)
        {
            Texture = texture;
            TextureOrigin = textureOrigin;
            Scale = scale;
            Tint = tint;
            TimeToLive = timeToLive;
        }

        public override void Update(GameTime gameTime)
        {
            TimeToLive = Math.Max(0, TimeToLive - gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive())
            {
                spriteBatch.Draw(Texture, Position, Texture.Bounds, Tint, Angle, TextureOrigin, Scale, SpriteEffects.None, 0);
            }
        }

        public bool IsActive()
        {
            return TimeToLive > 0;
        }
    }
}
