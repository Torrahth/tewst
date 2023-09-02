using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent;

namespace tmt.Projectiles.Ranged
{
    public class FleshPike : ModProjectile
    {
        private const string Chaintext = "Common/Textures/Intestant";

        private int MaxDistance = 600;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Type] = 2; 
        }

        public override void SetDefaults()
        {
           Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
          Projectile.width = 22;
      Projectile.height = 22;
           Projectile.penetrate = -1;
          Projectile.friendly = true;
            Projectile.hostile = false;
        Projectile.extraUpdates = 2;
          Projectile.DamageType = DamageClass.Ranged;
            Projectile.frame = Main.rand.Next(1, 2);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.DD2_SkeletonHurt, Projectile.Center);
            for (int i = 0; i < 12; i++)
            {
                Vector2 speed = Utils.NextVector2Circular(Main.rand, 1f, 1f);
                Dust.NewDustPerfect(Projectile.Center, 5, speed * 4f, 20, default(Color), 2f);
            }
    Projectile.ai[1] = 1f;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1] = 1f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (info.PvP == true)
            {
                Projectile.ai[1] = 1f;
            }
        }
  

        public override void AI()
        {
            Projectile.rotation += 0.3f;
            if (Main.rand.NextBool(7))
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Projectile.velocity / 4, 20, default, 2);
            }
            Lighting.AddLight(Projectile.Center, new Vector3(0.501f, 0.346f, 0.426f));
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 2;
            player.itemTime = 2;
            switch (Projectile.ai[1])
            {
                case 0:
                    {
                        if (Main.mouseLeft)
                        {
                            Projectile.ai[1] = 0;
                        }
                        else
                        {
                            Projectile.ai[1] = 1;
                        }
                        if (Projectile.Center.Distance(player.Center) > MaxDistance)
                        {
                            Projectile.ai[1] = 1;
                        }
                    }
                    break;
                case 1:
                    {
                        Projectile.tileCollide = false;
                        Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 18f;

                        if (Projectile.Center.Distance(player.Center) < 12)
                        {
                            Projectile.Kill();
                        }
                    }
                    break;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;
            Texture2D chainTexture = Mod.Assets.Request<Texture2D>(Chaintext).Value;

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Projectile.Center.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 18 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                Main.spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

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
                Color color = new Color(201 * Projectile.oldPos.Length / 3, 46, 126 , Projectile.oldPos.Length * 2) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }

    }
}
