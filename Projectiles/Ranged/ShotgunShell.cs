using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.Audio;

namespace tmt.Projectiles.Ranged
{
    public class ShotgunShell : ModProjectile
    {
        int increasetimer;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 21;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.timeLeft = 75;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.alpha = 85;
            Projectile.extraUpdates = 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(18))
            {
                SoundEngine.PlaySound(SoundID.Item4 with
                {
                    PitchVariance = 0.5f
                }, Projectile.Center) ;
                for (int i = 0;  i < 10; i++)
                {
                    Dust.NewDustPerfect(Projectile.Center, DustID.GreenFairy, Main.rand.NextVector2Circular(3, 3), 33);
                }
                Item.NewItem(Projectile.GetSource_FromThis(), target.Hitbox, ModContent.ItemType<HealthOrby>(), 1);
            } 
        }
        public override void AI()
        {
                Projectile.damage += 1;
            Projectile.velocity *= 1.001f; // they will never know...
            Projectile.scale -= 0.001f;
            if (Projectile.timeLeft <= 55)
            {   
                if (Projectile.timeLeft <= 20)
                {
                    Projectile.scale -= 0.05f;
                }
                else
                {
                    Projectile.alpha--;
                }
 
              
                Projectile.velocity /= 1.02f;
                if (Projectile.scale <= 0)
                {
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Mod.Assets.Request<Texture2D>("Projectiles/Ranged/ShotgunShellTrail").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                Color color = new Color(255, 225, 44, Projectile.oldPos.Length * 9) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, Projectile.scale, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);


            return false;
        }
    }
}
