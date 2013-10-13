using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public class BoundingCircle
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public BoundingCircle()
            : this(Vector2.Zero, 1)
        {
        }

        public BoundingCircle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public bool Intersects(BoundingCircle other)
        {
            Vector2 d = Position - other.Position;
            float r = Radius + other.Radius;
            return d.LengthSquared() < r * r;
        }
    }
}
