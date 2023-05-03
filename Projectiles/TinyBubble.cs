using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace tm.Projectiles
{
    public class TinyBubble : ModProjectile
    {
        float SLOWDOWNVELOCITY = Main.rand.NextFloat(1.01f, 1.05f);
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 70 + Main.rand.Next(-12, 12);
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = Projectile.scale + Main.rand.NextFloat(-0.2f, 0.2f);
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
            if (Projectile.timeLeft <= 55)
            {
                Projectile.velocity /= SLOWDOWNVELOCITY * 1.05f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item54, Projectile.Center);
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(1, 1) * 7, DustID.PurpleTorch, Main.rand.NextVector2CircularEdge(1, 1) * 4, 43, default, 1);
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2CircularEdge(1, 1) * 7, DustID.PurpleMoss, Main.rand.NextVector2CircularEdge(1, 1), 43, default, 1);
                d.noGravity = true;
            }
        }
    }
}
