using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;

namespace tmt.Projectiles.Ranged
{
    public class SparkArrow : ModProjectile
    {
        int ai;
        NPC npc;
        float t = 16;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 400;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_WitherBeastCrystalImpact, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                var hm = Main.rand.NextVector2Circular(4, 4);
                var d = Dust.NewDustPerfect(Projectile.Center + hm * 3, DustID.BlueFlare, hm * 2, 33);
                d.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 25);
            Projectile.velocity *= 2;
            npc = target;
            ai = 1;
    
        }
        public override void AI()
        {
            switch (ai)
            {
                case 0:
                    {
                        if (Main.rand.NextBool(4))
                        {
                            var hm = Main.rand.NextVector2Circular(4, 4);
                            var d = Dust.NewDustPerfect(Projectile.Center + hm * 3, DustID.BlueFlare, hm * 2, 33);
                            d.noGravity = true;
                        }
                        Projectile.rotation += 0.4f;
                    }
                    break;
                    case 1:
                    {
                        Projectile.tileCollide = false;
                        t /= 1.002f;
                        if (Projectile.timeLeft <= 50)
                        {
                            t /= 1.1f;
                        }
                        if (Main.rand.NextBool(3))
                        {
                            var hm = Main.rand.NextVector2Circular(4, 4);
                            var d = Dust.NewDustPerfect(Projectile.Center + hm * 3, DustID.BlueFlare, hm * 2, 33);
                            d.noGravity = true;
                        }
                        //  Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                        Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.AngleTo(npc.Center), 0.2f);
                        Projectile.velocity = Utils.RotatedBy(new Vector2(0f, t), (Projectile.rotation - 90f), default(Vector2));
                    }
                    break;
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
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.8f);
                Color color = new Color(105, 162, 255, Projectile.oldPos.Length * 7) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);


            return true;
        }
    }
}
