using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tm.Projectiles.Enemy
{
    public class SensitiveMine : ModProjectile
    {
         float timer = 25;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 18;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 200;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation = 0;
            return false;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= timer)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    timer -= 4;
                    Projectile.frame = 0;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<explosive>(), Projectile.damage, 12, Projectile.owner);
            for (int i = 0; i < 23; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, Main.rand.NextVector2Circular(2, 2) * 2, 0, default, Main.rand.NextFloat(-1.2f, 1.4f));
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(1, 1) * 2, 0, default, Main.rand.NextFloat(-1.2f, 1.4f));
                d.velocity.Y -= 0.1f;
                d.noGravity = true;
            }
        }
    }
    public class explosive : ModProjectile
    {
        public override string Texture => "tm/Common/Textures/Empty"; 
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 10;
        }
    }
}
