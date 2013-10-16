using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameLib
{
    public class ParticleEmitter : PhysicsObject
    {
        private static readonly Random _Random = new Random();

        private List<Particle> _particles;
        private float _timeSinceLastEmission;

        public List<Texture2D> Textures { get; set; }
        public Vector2 TextureOriginOffset { get; set; }
        public bool UseTextureCenterAsOrigin { get; set; }
        public float EmissionRate { get; set; }
        public float EmissionAcceleration { get; set; }
        public float MaxEmissionRate { get; set; }
        public float ConeAngle { get; set; }
        public float MinInitialSpeed { get; set; }
        public float MaxInitialSpeed { get; set; }
        public float MinAccelerationMagnitude { get; set; }
        public float MaxAccelerationMagnitude { get; set; }
        public float MinInitialAngularVelocity { get; set; }
        public float MaxInitialAngularVelocity { get; set; }
        public float MinAngularAcceleration { get; set; }
		public float MaxAngularAcceleration { get; set; }
        public double MinTtl { get; set; }
        public double MaxTtl { get; set; }

        public ParticleEmitter(List<Texture2D> textures)
        {
            Textures = textures;
            TextureOriginOffset = Vector2.Zero;
            UseTextureCenterAsOrigin = true;
            EmissionRate = 50f / 1000;
            EmissionAcceleration = 0;
            MaxEmissionRate = 100f / 1000;
            ConeAngle = MathHelper.TwoPi;
            MinInitialSpeed = 50f / 1000;
            MaxInitialSpeed = 50f / 1000; 
            MinAccelerationMagnitude = 0;
            MaxAccelerationMagnitude = 0;
            MinInitialAngularVelocity = -MathHelper.TwoPi / 1000;
            MaxInitialAngularVelocity = MathHelper.TwoPi / 1000;
            MinAngularAcceleration = 0;
            MaxAngularAcceleration = 0;
            MinTtl = 1000;
            MaxTtl = 1000;

			_particles = new List<Particle>();
            _timeSinceLastEmission = 1f / EmissionRate;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateParticles(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            EmissionRate += EmissionAcceleration * dt;
            _timeSinceLastEmission += dt;

            if (_timeSinceLastEmission >= 1f / EmissionRate)
            {
                AddParticle();
                _timeSinceLastEmission = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in _particles)
            {
                p.Draw(spriteBatch);
            }
        }

        public Particle CreateParticle()
        {
            Texture2D texture = Textures[_Random.Next(Textures.Count)];
            Vector2 textureOrigin = UseTextureCenterAsOrigin ? new Vector2(texture.Width / 2, texture.Height / 2) : TextureOriginOffset;
            //Color tint = new Color(_Random.Next(256), _Random.Next(256), _Random.Next(256));
            double ttl = RandomDouble(MinTtl, MaxTtl);

            Particle p = new Particle(texture, textureOrigin, Color.White, ttl);           

            // This isn't necessarily something that should be done...possibly make it an option.
            // It makes initial angle of particle the same as the angle of the emitter.
            p.Angle = Angle + RandomFloat(-ConeAngle / 2, ConeAngle / 2);
            p.AngularVelocity = RandomFloat(MinInitialAngularVelocity, MaxInitialAngularVelocity);
            p.AngularAcceleration = RandomFloat(MinAngularAcceleration, MaxAngularAcceleration);

            Vector2 orientation = new Vector2((float)Math.Cos(p.Angle), (float)Math.Sin(p.Angle));

            p.Position = Position;
            p.Velocity = RandomFloat(MinInitialSpeed, MaxInitialSpeed) * orientation;
            p.Acceleration = RandomFloat(MinAccelerationMagnitude, MaxAccelerationMagnitude) * orientation;

            return p;
        }

        private void UpdateParticles(GameTime gameTime)
        {
            for (int i = _particles.Count - 1; i >= 0; --i)
            {
                Particle p = _particles[i];
                p.Update(gameTime);

                if (!p.IsActive)
                {
                    _particles.RemoveAt(i);
                }
            }
        }

        private void AddParticle()
        {
            _particles.Add(CreateParticle());
        }

        private float RandomFloat(float min, float max)
        {
            return (float)RandomDouble(min, max);
        }

        private float RandomDouble(double min, double max)
        {
            return (float)(min + _Random.NextDouble() * (max - min));
        }
    }
}
