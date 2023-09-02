using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace tmt.Projectiles.Mage
{
    public class JellyStar : ModProjectile
    {
        float x;
        float y;
        float rotationvalue = Main.rand.NextFloat(0.04f, 0.04f);
        float timer;
        float timer2;
        int r = Main.rand.Next(80, 180);
        int b = Main.rand.Next(80, 180);
        int g = Main.rand.Next(80, 180);
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.rotation = Main.rand.Next(-180, 180);
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.timeLeft = 50 + Main.rand.Next(-12, 12);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.scale = Projectile.scale + Main.rand.NextFloat(-0.2f, 0.2f);
        }
        public override void AI()
        {
            Projectile.velocity = new Vector2(x, y).RotatedBy(Projectile.rotation) / 22;
            x = (float)Math.Sin(timer) * 132;
            y = (float)Math.Sin(timer) * 132;
           timer += Main.rand.NextFloat(-0.5f, 0.5f);
            timer2 += Main.rand.NextFloat(-0.5f, 0.5f);
            Projectile.rotation += rotationvalue;
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            Lighting.AddLight(Projectile.Center, new Vector3(r, g, b) / 122);
         
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item50, Projectile.Center);
            for (int i = 0; i < 9; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowTorch, Main.rand.NextVector2Circular(2, 2) * 3, 0, new Color(r, g, b), 1);
                d.noGravity = true;
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
                Color color = new Color(r, g, b, Projectile.oldPos.Length * 4) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);


            return false;
        }
    }
}
