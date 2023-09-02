using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace tmt.Projectiles
{
    public class RosePebble : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.timeLeft = 70 + Main.rand.Next(-6, 32);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.velocity *= 1.05f;
        }

          public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10 with
            {
                PitchVariance = 0.8f,
            });
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Astra, Main.rand.NextVector2Circular(3, 3));
            }
        }
    }
}
