using System;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public interface ICollidable
    {
        BoundingCircle Bounds { get; set; }
        byte[,] CollisionData();
        Matrix Transform();
    }
}