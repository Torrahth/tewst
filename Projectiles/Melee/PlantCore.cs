using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace tm.Projectiles.Melee
{
    public class PlantCore : ModProjectile
    {
        public override void SetDefaults()
        {
         
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.timeLeft = 250;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.velocity /= 1.1f;
            if (Projectile.velocity.Length() <= 0.1 && Projectile.timeLeft >= 60 && Main.rand.NextBool(12))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 1), ModContent.ProjectileType<PlantRoots>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Grass, Projectile.Center);
            for (int i = 0; i < 13; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.JunglePlants, Main.rand.NextVector2Circular(3, 3) * 1.3f, 56);
            }
        }
    }
}
