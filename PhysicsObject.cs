﻿using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public class PhysicsObject : IUpdatable
    {
        private Vector2 _velocity;
        private float _maxSpeed;
        private float _angle;
        private float _angularVelocity;
        private float _maxAngularSpeed;

        public virtual Vector2 Position { get; set; }

        public virtual Vector2 Acceleration { get; set; }

        public virtual float AngularAcceleration { get; set; }

        public virtual Vector2 Velocity
        {
            get { return _velocity; }

            set
            {
                Debug.Assert(value.Length() <= MaxSpeed);
                _velocity = value;
            }
        }

        public virtual float MaxSpeed
        {
            get { return _maxSpeed; }

            set
            {
                Debug.Assert(value >= 0);
                _maxSpeed = value;
            }
        }

        public virtual float AngularVelocity
        {
            get { return _angularVelocity; }

            set
            {
                Debug.Assert(Math.Abs(value) <= MaxAngularSpeed);
                _angularVelocity = value;
            }
        }

        public virtual float MaxAngularSpeed
        {
            get { return _maxAngularSpeed; }

            set
            {
                Debug.Assert(value >= 0);
                _maxAngularSpeed = value;
            }
        }

        public virtual float Angle
        {
            get { return _angle; }

            set
            {
                _angle = value;
                while (_angle < 0)
                {
                    _angle += MathHelper.TwoPi;
                }
                while (_angle >= MathHelper.TwoPi)
                {
                    _angle -= MathHelper.TwoPi;
                }
            }
        }

        public virtual Vector2 Orientation
        {
            get
            {
                return Vectors.Vector2FromAngle(Angle);
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
            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 initialVelocity = Velocity;
            _velocity += Acceleration * dt;
            CapVelocity();

            Vector2 displacement = (initialVelocity + Velocity) * 0.5f * dt;
            Position += displacement;

            float initialAngularVelocity = AngularVelocity;
            _angularVelocity += AngularAcceleration * dt;
            CapAngularVelocity();

            Angle += (initialAngularVelocity + AngularVelocity) * 0.5f * dt;
        }

        private void CapVelocity()
        {
            // Avoid square root calculation by comparing squared speeds.
            float speedSquared = _velocity.LengthSquared();
            if (speedSquared > MaxSpeed * MaxSpeed)
            {
                _velocity *= MaxSpeed / (float)Math.Sqrt(speedSquared);
            }
        }

        private void CapAngularVelocity()
        {
            _angularVelocity = MathHelper.Clamp(_angularVelocity, -MaxAngularSpeed, MaxAngularSpeed);
        }
    }
}
