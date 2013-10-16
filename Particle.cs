using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameLib
{
    public class Particle : PhysicsObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 TextureOrigin { get; set; }
        public Color Tint { get; set; }
        public double TimeToLive { get; set; }

        public Particle(Texture2D texture, Vector2 textureOrigin, Color tint, double timeToLive)
        {
            Texture = texture;
            TextureOrigin = textureOrigin;
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
                spriteBatch.Draw(Texture, Position, Texture.Bounds, Tint, Angle, TextureOrigin, 1, SpriteEffects.None, 0);
            }
        }

        public bool IsActive()
        {
            return TimeToLive > 0;
        }
    }
}
