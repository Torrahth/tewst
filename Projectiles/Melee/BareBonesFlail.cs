using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using System.Collections.Generic;
using tm.Common;

namespace tm.Projectiles.Melee
{
    public class BareBonesFlail : ModProjectile 
    {
        float num210;
        float value = 0; 
        float speed
        {
            get { return num210; }
            set { num210 = value; }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bare-Bones");
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.DamageType = DamageClass.Melee; 
           // Projectile.scale = 0.8f;
            Projectile.usesLocalNPCImmunity = true; 
            Projectile.localNPCHitCooldown = 15; 
            Projectile.aiStyle = ProjAIStyleID.Flail; // oh holy moly this exists?
           AIType = ProjectileID.TheMeatball; // FUCK YOUUU!!!
         //   DrawOffsetX = -6;
          //  DrawOriginOffsetY = -6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.boss == false)
            {
                Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.4f, 0.12f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, 6).RotatedBy(Projectile.rotation), new Vector2(0, 5).RotatedBy(Projectile.rotation + Main.rand.Next(-5, 5)) , ModContent.ProjectileType<Metalspark>(), 0, 0, Projectile.owner);
                SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                Projectile.velocity *= 1f - (Projectile.velocity.Length() / 32);
                target.velocity.Y *= 0 - (Projectile.velocity.Length() / 16);
                Projectile.rotation += 0.4f;
                if (value <= 43)
                {
                    target.velocity /= 1.5f;
                    value++;
                }
                value = 0;
            }

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
  

           
        }
        public override bool PreDrawExtras()
        {
            var player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            Texture2D chainTexture = Mod.Assets.Request<Texture2D>("Common/Textures/BigBoner").Value;

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
                drawPosition += remainingVectorToPlayer * 23 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                Main.spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
 

          //  Projectile.type = Projectile.whoAmI;

            if (Projectile.ai[0] == 1f)
            {
                Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
                Vector2 drawPosition2 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Vector2 drawOrigin = new Vector2(projectileTexture.Width, projectileTexture.Height) / 2f;
                Color drawColor = Projectile.GetAlpha(lightColor);
                drawColor.A = 127;
                drawColor *= 0.5f;
                int launchTimer = (int)Projectile.ai[1];
                if (launchTimer > 5)
                {
                    launchTimer = 5;
                }

                SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                for (float transparancy = 1f; transparancy >= 0f; transparancy -= 0.125f)
                {
                    float opacity = 1f - transparancy;
                    Vector2 drawAdjustment = Projectile.velocity * -launchTimer * transparancy;
                    Main.EntitySpriteDraw(projectileTexture, drawPosition2 + drawAdjustment, null, drawColor * opacity, Projectile.rotation, drawOrigin, Projectile.scale * 1.15f * MathHelper.Lerp(0.5f, 1f, opacity), spriteEffects, 0);
                }
            }
            return true;
        }
        public override void AI()
        {
      
        }
        public override void PostAI()
        {
            base.PostAI();
        }
        public override bool PreAI()
        {
            speed = 1;// DustID.RedMoss;
            return true;
        }
    }
}
