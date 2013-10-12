using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameLib
{
    public class PhysicsObject
    {
        public Vector2 Position { get; set; }
        private Vector2 velocity;
        public Vector2 Acceleration { get; set; }
        private float maxSpeed;

        private float angle;
        private float angularVelocity;
        public float AngularAcceleration { get; set; }
        private float maxAngularSpeed;

        public Vector2 Velocity
        {
            get { return velocity; }

            set
            {
				Debug.Assert(value.Length() <= MaxSpeed);
                velocity = value;
            }
        }

        public float MaxSpeed
        {
            get { return maxSpeed; }

            set
            {
                Debug.Assert(value >= 0);
                maxSpeed = value;
            }
        }

        public float AngularVelocity
        {
            get { return angularVelocity; }

            set
            {
                Debug.Assert(Math.Abs(value) <= MaxAngularSpeed);
                angularVelocity = value;
            }
        }

        public float MaxAngularSpeed
        {
            get { return maxAngularSpeed; }

            set
            {
                Debug.Assert(value >= 0);
                maxAngularSpeed = value;
            }
        }

        public float Angle
        {
            get { return angle; }

            set
            {
                angle = value;
                while (angle < 0) angle += MathHelper.TwoPi;
                while (angle >= MathHelper.TwoPi) angle -= MathHelper.TwoPi;
            }
        }

        public Vector2 Orientation
        {
            get
            {
                return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            }

            set
            {
                Angle = (float)Math.Acos(value.X);

                if (value.Y < 0)
                {
                    Angle = MathHelper.TwoPi - Angle;
                }
            }
        }

        public PhysicsObject()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            MaxSpeed = float.MaxValue;

            Angle = 0;
            AngularVelocity = 0;
            AngularAcceleration = 0;
            MaxAngularSpeed = float.MaxValue;
        }

        public virtual void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 initialVelocity = Velocity;
            velocity += Acceleration * dt;
            CapVelocity();

            Vector2 displacement = (initialVelocity + Velocity) * 0.5f * dt;
            Position += displacement;

            float initialAngularVelocity = AngularVelocity;
            angularVelocity += AngularAcceleration * dt;
            CapAngularVelocity();

            Angle += (initialAngularVelocity + AngularVelocity) * 0.5f * dt;
        }

        private void CapVelocity()
        {
            // Avoid square root calculation by comparing squared speeds.
            float speedSquared = velocity.LengthSquared();
            if (speedSquared > MaxSpeed * MaxSpeed)
            {
                velocity *= MaxSpeed / (float) Math.Sqrt(speedSquared);
            }
        }

        private void CapAngularVelocity()
        {
            angularVelocity = MathHelper.Clamp(angularVelocity, -MaxAngularSpeed, MaxAngularSpeed);
        }
    }
}
