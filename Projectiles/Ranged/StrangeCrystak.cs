using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;

namespace tmt.Projectiles.Ranged
{
    public class CrystakArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 17; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.hostile = false;
            Projectile.extraUpdates = 3;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.5f);
                Color color = new Color(201 * Projectile.oldPos.Length / 3, 46, 126, Projectile.oldPos.Length * 2) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_GoblinBomb, Projectile.Center);
            for (int i = 0; i < 4; i++)
            {
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.OrangeTorch, Main.rand.NextVector2Circular(2, 2), 0 , Scale: 2);
                d.noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-8, 8), 0), DustID.OrangeStainedGlass, new Vector2(0, Main.rand.NextFloat(-4.3f, -3)), 0);
            }
                if (Main.rand.NextBool(6))
            {
                SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaivePierce, Projectile.Center);
                for (int i = 0; i < 3; i++)
                {
                    var p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, -4).RotatedByRandom(MathHelper.ToRadians(12)), ProjectileID.WoodenArrowFriendly, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    Main.projectile[p].extraUpdates = 3;
                    for (int f = 0; f < 2; f++)
                    {

                        var e = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.Next(-8, 8), 0), DustID.OrangeTorch, new Vector2(0, Main.rand.NextFloat(-2.3f, -1)), 0, Scale: 2);
                        e.noGravity = true;
                    }
                }
            }
        }
    }
        public class StrangeCrystak : ModProjectile
    {
        Vector2 mousepos = Main.MouseWorld;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.hostile = false;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.Center.Distance(mousepos) <= Main.rand.Next(-63, 63))
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Main.rand.Next(-2, 2), -5).RotatedByRandom(MathHelper.ToRadians(12)), ModContent.ProjectileType<CrystakArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
         
                Projectile.Kill();
            }
            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 5)
                {
                    Projectile.frame = 0;
                }
            }

        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, Projectile.Center);
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PurpleCrystalShard, Main.rand.NextVector2Circular(2, 2) * 2, 0);
            }
            }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.5f);
                Color color = new Color(201 * Projectile.oldPos.Length / 3, 46, 126, Projectile.oldPos.Length * 2) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}
