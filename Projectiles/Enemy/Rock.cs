using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tm.Projectiles.Enemy
{
    public class Rock : ModProjectile
    {
        float rotationvalue = Main.rand.NextFloat(-0.4f, 0.4f);
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.aiStyle = 1;
            Projectile.rotation = Main.rand.Next(-180, 180);
        }
        public override void AI()
        {
            Projectile.rotation += rotationvalue;
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Stone, Main.rand.NextVector2Circular(1, 1) * (Projectile.velocity / 5));
            }
        }
    }
}
