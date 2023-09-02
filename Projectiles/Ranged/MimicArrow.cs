using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace tmt.Projectiles.Ranged
{
    public class MimicArrow : ModProjectile
    {
        private const string Chaintext = "Common/Textures/MagicChain";
        int typer;
        int water = 0;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 31;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
      
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.penetrate = 3;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 1;
            Projectile.extraUpdates = 5;
        }
        public override void AI()
        {
            if (++typer == 30)
            {
                if (Projectile.penetrate < 5)
                {
                    Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(34)) * 1.4f;
                }
          
                typer = 20;
            }
            if (Projectile.penetrate < 1)
            {

                water++;
                if (water > 130)
                {
                    Projectile.Kill();
                }
                Projectile.velocity /= 1.05f;
            }
   
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(207 + water, 185 + water, 161 + water) * (1 - Projectile.alpha);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);   
            for (int i = 0; i < 15; ++i) {
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.FireworkFountain_Green, Main.rand.NextVector2CircularEdge(4 * Main.rand.NextFloat(-0.3f, 0.3f), 4 * Main.rand.NextFloat(-0.3f, 0.3f)) , 12);
                d.noGravity = true;
            }
            Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.TwoPi) / 2;
            
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 35; ++i)
            {
                var sp = Main.rand.NextVector2Circular(7, 7);
                var d = Dust.NewDustPerfect(Projectile.Center + sp * 12, DustID.GreenFairy, sp * 2, 12);
                d.noGravity = true;
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ExplosionEffect>(), Projectile.damage / 2, 4, Projectile.owner);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale + 0.4f * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.8f);
                Color color = new Color(50, 64, 62, Projectile.oldPos.Length * 12) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);


            return true;
        }

    }
}
