using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace tmt.Projectiles
{
    public class FizzerBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        float SLOWDOWNVELOCITY = Main.rand.NextFloat(1.01f, 1.03f);
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.timeLeft = 150 + Main.rand.Next(-12, 12); 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.scale = Projectile.scale + Main.rand.NextFloat(-0.2f, 0.2f);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 8;
            height = 8;
            return true;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }

            Projectile.velocity /= SLOWDOWNVELOCITY;
            if (Projectile.timeLeft <= 120)
            {
                Projectile.velocity /= SLOWDOWNVELOCITY * 1.05f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item54, Projectile.Center);
            for (int i = 0; i < 22; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(1, 1) * 14, DustID.PurpleTorch, Main.rand.NextVector2CircularEdge(1, 1) * 4, 43, default, 1);
                d.noGravity = true;
            }
            for (int i = 0; i < 22; i++)
            {
               Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(1, 1) * 14, DustID.PurpleMoss, Main.rand.NextVector2CircularEdge(1, 1), 43, default, 1);
                d.noGravity = true;
            }
        }
    }
}
