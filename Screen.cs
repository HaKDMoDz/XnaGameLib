using System;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public static class Screen
    {
        private static Random _Random = new Random();

        public static bool IsOnscreen(Rectangle extent, BoundingCircle bounds)
        {
            float left = extent.Left + bounds.Radius;
            float right = extent.Right - bounds.Radius;
            float bottom = extent.Bottom - bounds.Radius;
            float top = extent.Top + bounds.Radius;
            Vector2 position = bounds.Position;
            return left <= position.X && position.X <= right &&
                top <= position.Y && position.Y <= bottom;
        }

        public static Vector2 RandomOnscreenPosition(Rectangle extent, BoundingCircle bounds)
        {
            float left = extent.Left + bounds.Radius;
            float right = extent.Right - bounds.Radius;
            float bottom = extent.Bottom - bounds.Radius;
            float top = extent.Top + bounds.Radius;
            return new Vector2(_Random.NextFloat(left, right), _Random.NextFloat(top, bottom));
        }

        public static bool IsOffscreen(Rectangle extent, BoundingCircle bounds)
        {
            // Offscreen if closest point on extent is > radius distance away.
            AABB extentAABB = new AABB(new Vector2(extent.Left, extent.Top),
                                   new Vector2(extent.Right, extent.Bottom));
            Vector2 closestPoint = extentAABB.ClosestPointTo(bounds.Position);
            Vector2 displacement = closestPoint - bounds.Position;
            return displacement.LengthSquared() > bounds.Radius * bounds.Radius;
        }

        public static Vector2 RandomOffscreenPosition(Rectangle extent, BoundingCircle bounds)
        {
            // Choose a fully onscreen position.
            Vector2 onScreenPosition = RandomOnscreenPosition(extent, bounds);

            // Choose an offset to an offscreen position, randomly.
            float[] xOffsets = {-extent.Width, 0, extent.Width};
            float[] yOffsets = {-extent.Height, 0, extent.Height};
            Vector2 offset;
            do
            {
                offset = new Vector2(xOffsets[_Random.Next(3)], yOffsets[_Random.Next(3)]);
            }
            while (offset == Vector2.Zero);

            return onScreenPosition + offset;
        }

        public static Vector2 OnscreenDirection(Rectangle extent, BoundingCircle bounds)
        {
            // Calculate the normalized vector from current position to onscreen position.
            Vector2 onScreenPosition = RandomOnscreenPosition(extent, bounds);
            Vector2 direction = onScreenPosition - bounds.Position;
            direction.Normalize();
            return direction;
        }

        public enum ScreenState
        {
            Off,
            Partial,
            On
        }
    }
}
