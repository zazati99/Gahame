using System;

using System.Collections.Generic;

using Gahame.GameUtils;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameScreens
{
    public class ParticleSystem
    {
        // screen that it gets things from
        GameScreen screen;

        // Textures
        List<Texture2D> textures;

        // the particles
        public List<Particle> Particles;

        // particle properties
        // Destroy on collision?
        public bool DestroyOnCollision;

        // Position thing
        public Vector2 Position;
        public Vector2 PositionOffset;

        // Acceleration
        public Vector2 MinAcceleration;
        public Vector2 MaxAcceleration;

        // Start velocity
        public Vector2 MinStartVelocity;
        public Vector2 MaxStartVelocity;

        // how long will boys live
        public float LifeSpan;

        // System properties
        // Amount of particles that will be emitted
        public float EmitAmount;

        // Constructor thing
        public ParticleSystem(GameScreen screen)
        {
            // particle properties
            DestroyOnCollision = false;
            PositionOffset = new Vector2(0, 0);
            MinAcceleration = new Vector2(0, 0);
            MaxAcceleration = new Vector2(0, 0);
            MinStartVelocity = new Vector2(0, 0);
            MaxStartVelocity = new Vector2(0, 0);
            LifeSpan = 60;

            // System properties
            EmitAmount = 3;

            this.screen = screen;
            textures = new List<Texture2D>();
            Particles = new List<Particle>();
        }

        // Add a texture
        public void AddTexture(Texture2D texture)
        {
            textures.Add(texture);
        }

        // Emit particles
        public void Emit()
        {
            // Emit the right amount
            for (int i = 0; i < EmitAmount; i++)
            {
                // Create ransom stuff
                Random r = new Random();

                // Create the particle
                Particle p = new Particle(this);

                // initialize stuff
                p.Texture = textures[r.Next(0, textures.Count)];
                p.Position = Position + new Vector2(MyMaths.RandomInRange(-PositionOffset.X, PositionOffset.X), MyMaths.RandomInRange(-PositionOffset.Y, PositionOffset.Y));
                p.Velocity.X = MyMaths.RandomInRange(MinStartVelocity.X, MaxStartVelocity.X);
                p.Velocity.Y = MyMaths.RandomInRange(MinStartVelocity.Y, MaxStartVelocity.Y);
                p.Acceleration.X = MyMaths.RandomInRange(MinAcceleration.X, MaxAcceleration.X);
                p.Acceleration.Y = MyMaths.RandomInRange(MinAcceleration.Y, MaxAcceleration.Y);
                p.LifeSpan = LifeSpan;

                Particles.Add(p);
            }
        }

        // Update particles
        public void Update(GameTime gameTime)
        {
            // Update every singel particle
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update(gameTime);
            }

            // DO collision checking if that is enabled
            if (DestroyOnCollision)
            {
                for (int i = 0; i < Particles.Count; i++)
                {
                    Particles[i].CollisionCheck();
                }
            }
        }

        // Draw particles
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw every single particle
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(spriteBatch);
            }
        }
    }
}
