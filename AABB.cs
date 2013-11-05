using System;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
	public class AABB
	{
		public Vector2 Min;
		public Vector2 Max;

		public AABB()
		{
			Min = new Vector2();
			Max = new Vector2();
			Empty();
		}

		public AABB(Vector2 min, Vector2 max)
		{
			Min = min;
			Max = max;
		}

		public Vector2 Size()
		{
			return Max - Min;
		}

		public float XSize()
		{
			return Max.X - Min.Y;
		}

		public float YSize()
		{
			return Max.Y - Min.Y;
		}

		public Vector2 Center()
		{
			return (Min + Max) * .5f;
		}

		public void Empty()
		{
			Min.X = Min.Y = 1e37f;
			Max.X = Max.Y = -1e37f;
		}

		public void Add(Vector2 p)
		{
			if (p.X < Min.X)
			{
				Min.X = p.X;
			}

			if (p.X > Max.X)
			{
				Max.X = p.X;
			}

			if (p.Y < Min.Y)
			{
				Min.Y = p.Y;
			}

			if (p.Y > Max.Y)
			{
				Max.Y = p.Y;
			}
		}

		public void Add(AABB box)
		{
			if (box.Min.X < Min.X)
			{
				Min.X = box.Min.X;
			}

			if (box.Max.X > Max.X)
			{
				Max.X = box.Max.X;
			}

			if (box.Min.Y < Min.Y)
			{
				Min.Y = box.Min.Y;
			}

			if (box.Max.Y > Max.Y)
			{
				Max.Y = box.Max.Y;
			}
		}

		public bool IsEmpty()
		{
			return (Min.X > Max.X) || (Min.Y > Max.Y);
		}

		public bool Contains(Vector2 p)
		{
			return (p.X >= Min.X) && (p.X <= Max.X) &&
				(p.Y >= Min.Y) && (p.Y <= Max.Y);
		}

		public Vector2 ClosestPointTo(Vector2 p)
		{
			Vector2 r = new Vector2 ();

			if (p.X < Min.X)
			{
				r.X = Min.X;
			}
			else if (p.X > Max.X)
			{
				r.X = Max.X;
			}
			else
			{
				r.X = p.X;
			}

			if (p.Y < Min.Y)
			{
				r.Y = Min.Y;
			}
			else if (p.Y > Max.Y)
			{
				r.Y = Max.Y;
			}
			else
			{
				r.Y = p.Y;
			}

			return r;
		}

		public bool Intersects(AABB box)
		{
			return Min.X <= box.Max.X && Max.X >= box.Min.X &&
				Min.Y <= box.Max.Y && Max.Y >= box.Min.Y;
		}

		public bool IntersectsCircle(BoundingCircle circle)
		{
			Vector2 closestPoint = ClosestPointTo(circle.Position);
			return Vector2.DistanceSquared(closestPoint, circle.Position) < circle.Radius * circle.Radius;
		}
	}
}