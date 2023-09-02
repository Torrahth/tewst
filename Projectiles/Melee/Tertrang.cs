using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace tmt.Projectiles.Melee
{
    public class Tertrang : ModProjectile
    {
        int Frozen = 1;
        int Progress = 0;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.timeLeft = 1000; 
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            DrawOriginOffsetX = 9;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            Projectile.ai[0] = 1;
            Projectile.tileCollide = false;
            return false;
        }


        public override void AI()
        {
            Projectile.frame = Frozen;
            Player player = Main.player[Projectile.owner];

            Projectile.timeLeft = 2;


            if (Main.mouseRightRelease)
            {
                Projectile.ai[2] += 0.1f;
                SoundEngine.PlaySound(SoundID.Item46 with
                {
                    PitchVariance = 0.8f,
                });
                Projectile.rotation += 0.3f;
                Progress++;
                Frozen = 0;

            }
            else
            {
                Projectile.ai[2] += 0.02f;
                Projectile.rotation += 0.01f;
                Projectile.velocity = new Vector2(0, 0);
                Frozen = 1;
            }


            switch (Projectile.ai[0])
            {
                case 0:
                    {
                     
                        if (Progress >= 40)
                        {
                            Projectile.ai[0] = 1;
                        }
        
                    }
                    break;
                case 1:
                    {
                        Projectile.velocity += Projectile.velocity.DirectionTo(player.Center - Projectile.Center) / 4;
                        if (Progress >= 130)
                        {
                            Projectile.velocity += Projectile.velocity.DirectionTo(player.Center - Projectile.Center) * 3;
                        }
                        if (Projectile.Center.Distance(player.Center) < 12)
                        {
                            Projectile.Kill();
                        }
                        Projectile.tileCollide = false;
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
            var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
         
                Color color = new Color(173, 210, 255, Projectile.oldPos.Length * 6) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale + 0.4f * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.8f);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);

                Texture2D tex = Mod.Assets.Request<Texture2D>("Common/Textures/FireflyLight").Value;
                Texture2D tex2 = Mod.Assets.Request<Texture2D>("Common/Textures/LargeChain").Value;

                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, color, Projectile.rotation, tex.Size() / 2, Projectile.scale, SpriteEffects.None, 0);

                Main.EntitySpriteDraw(tex2, Projectile.Center - Main.screenPosition, null, color, Projectile.ai[2], tex2.Size() / 2, Projectile.scale + (float)Math.Sin(Projectile.ai[2]) / 8, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

    
            return true;
        }
    }
}
