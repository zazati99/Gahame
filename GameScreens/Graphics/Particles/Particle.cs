using System;

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class Particle
    {
        // textruuur
        public Texture2D Texture;

        // Particle system
        ParticleSystem particleSystem;

        // Position
        public Vector2 Position;

        // velocity
        public Vector2 Velocity;

        // acceleration;
        public Vector2 Acceleration;

        // scale
        public Vector2 Scale;

        // Lifespan
        public float LifeSpan;

        // meme
        public Particle(ParticleSystem particleSystem)
        {
            this.particleSystem = particleSystem;
        }

        // Collision check
        public void CollisionCheck()
        {
            
        }

        // Update memes
        public void Update(GameTime gameTime)
        {
            // lifespan
            LifeSpan -= GahameController.GameSpeed;
            if (LifeSpan <= 0)
            {
                particleSystem.Particles.Remove(this);
            }
            
            // acceleration
            Velocity += Acceleration * GahameController.GameSpeed;

            // add trash
            Position += Velocity * GahameController.GameSpeed;
        }

        // Draw the particle
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, scale: Scale); 
        }
    }
}
