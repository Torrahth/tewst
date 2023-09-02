using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using tmt.Common;

/* // my code and not anyone elses, thanks! btw do not steal it or your dumb
using tin;
using preformies.bendo;
namespace bendocom.tinban
{
  if user == "tin"
  {
    banuser("1/1/2999", "you have been banned for: tinnicious activity")
  }
}
*/

namespace tmt.Projectiles.Mage
{
    public class MagicSpark : ModProjectile
    {
        float apple = Main.rand.NextFloat(0.1f, 0.3f);

        float mousex = Main.MouseWorld.X;
        float mousey = Main.MouseWorld.Y;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 27;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.timeLeft = 120 + Main.rand.Next(-12, 12);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = Projectile.scale + Main.rand.NextFloat(-0.1f, 0.1f);
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 3;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextBool(7))
            {
                player.lifeSteal += 1;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit30, Projectile.Center);
            Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.3f, 0.8f);
            if (target.boss == false) {
                target.velocity /= 2f;
            }
      
            target.AddBuff(BuffID.Cursed, 2000);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (info.PvP == true)
            {
                Player player = Main.player[Projectile.owner];
                if (Main.rand.NextBool(7))
                {
                    player.lifeSteal += 1;
                }
                Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit30, Projectile.Center);
                Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.3f, 0.8f);
                target.velocity /= 2f;
                target.AddBuff(BuffID.Cursed, 2000);
            }
        }
        public override void AI()
        {
   
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.AngleTo(new Vector2(mousex, mousey)), apple);// MathF. (Projectile.Center - Main.MouseWorld);
           Projectile.velocity = Utils.RotatedBy(new Vector2(0f, 14f), (Projectile.rotation - 90f), default(Vector2));
            if (Projectile.timeLeft <= 90)
            {
                Projectile.scale -= 0.01f;
            }
            if (Projectile.scale <= 0)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Mod.Assets.Request<Texture2D>("Common/Textures/FireflyLight").Value;//TextureAssets.Projectile[Projectile.type].Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.9f);
                Color color = new Color(175, 0, 50, Projectile.oldPos.Length * 5) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);


            return false;
        }
    }
}
