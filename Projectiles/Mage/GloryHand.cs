using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;
using tm.Common;

namespace tm.Projectiles.Mage
{
    public class GloryHand : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(0.700f, 0, 0));
            if (Projectile.velocity.Length() <= 12 && Projectile.timeLeft <= 580)
            {
                Projectile.velocity *= 1.4f;
            }
            for (int ia = 0; ia < Main.maxProjectiles; ia++)
            {
                if (ia == Projectile.whoAmI)
                    continue;  // Ignore "this projectile"

                Projectile otherProj = Main.projectile[ia];
                if (otherProj.active && otherProj.friendly )
                {
                    if (Projectile.Hitbox.Intersects(otherProj.Hitbox))
                    {
                        Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.7f, 0.32f);
                        Projectile.Kill();
                        otherProj.Kill();
                        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<HITBOX>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        for (int i = 0; i < 10; i++)
                        {
                            Dust.NewDustPerfect(Projectile.Center, DustID.Torch, 1.1f  * Main.rand.NextVector2Circular(3, 3)  , 0, Scale: 2);
                        }
                  
                    }
                }
            }
            
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Ash, (Projectile.velocity / 5) * Main.rand.NextVector2Circular(3, 3) , 0);
            }
        }

    }
    public class HITBOX : ModProjectile
    {
        public override string Texture => "tm/Common/Textures/Empty";
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 12;
            Projectile.penetrate = -1;
        }


    }
}
