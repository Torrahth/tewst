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
    public class AFriendlyCatRose : ModProjectile
    {
        public override string Texture => "tmt/Projectiles/ACatRose";
        Vector2 endpos;
        float water = Main.rand.Next(-62, 62);
        float water2 = Main.rand.Next(63, 144);
        int quickness = Main.rand.Next(45, 120);
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1;
        }
        public override void Kill(int timeLeft)
        {
          
        }
        public override void OnSpawn(IEntitySource source)
        {
            water = Main.rand.Next(-62, 62);
            water2 = Main.rand.Next(63, 144);
        }
        public override void AI()
        {
            if (!Main.dayTime && Main.moonPhase == 0 && Main.moonType == 4)
            {
                Lighting.AddLight(Projectile.Center, new Vector3(0.538f, 0.628f, 0.655f));
            }
            Player player = Main.player[Projectile.owner];
            if (player.GetModPlayer<tmtplayer>().CatRose == false)
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 2;
            if (++Projectile.frameCounter >= quickness)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                    water = Main.rand.Next(-62, 62);
                    water2 = Main.rand.Next(63, 144);
                }
            }
            endpos = new Vector2(water, -water2);
            Projectile.ai[1] += 0.1f;
       
            Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center + endpos, 0.04f);
            Projectile.position.X += (float)Math.Sin(Projectile.ai[1]) * 0.5f;
            Projectile.position.Y += (float)Math.Cos(Projectile.ai[1]) * 0.5f;
        }
    }
    public class ACatRose : ModProjectile
    {
        Vector2 endpos;
        float water = Main.rand.Next(-62, 62);
        float water2 = Main.rand.Next(63, 144);
        int quickness = Main.rand.Next(45, 120);
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 5555;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item5 with
            {
                PitchVariance = 0.8f,
            });
            Vector2 orpeojct = Main.MouseWorld - Projectile.Center;
            float desiredRotation = orpeojct.ToRotation();

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(23, 0).RotatedBy(desiredRotation), ModContent.ProjectileType<RosePebble>(), 12, 0, Projectile.owner);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Astra, Main.rand.NextVector2Circular(3, 3));
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            water = Main.rand.Next(-62, 62);
            water2 = Main.rand.Next(63, 144);
        }
        public override void AI()
        {
            if (!Main.dayTime && Main.moonPhase == 0 && Main.moonType == 4)
            {
                Lighting.AddLight(Projectile.Center, new Vector3(0.538f, 0.628f, 0.655f));
            }
            Player player = Main.player[Projectile.owner];
            if (++Projectile.frameCounter >= quickness)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.Kill();
                }
            }
            endpos = new Vector2(water, -water2);
            Projectile.ai[1] += 0.1f;

            Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center + endpos, 0.04f);
            Projectile.position.X += (float)Math.Sin(Projectile.ai[1]) * 0.5f;
            Projectile.position.Y += (float)Math.Cos(Projectile.ai[1]) * 0.5f;
        }
    }
}
