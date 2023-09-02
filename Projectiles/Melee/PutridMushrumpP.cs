using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace tmt.Projectiles.Melee
{
    public class PutridMushrumpP : ModProjectile
    {
        public override string Texture => "tmt/Items/Weapons/Melee/PutridMushrump";
        
        private float distanceFromPlayer = 25;

        private float Speed = 9999f;

        private float rotation;

        private float WeaponSpeed = 50f;

        private float WeaponSpeedUpAmount = 0.05f;
        float angle;

        float radius = 60;

        float speed
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(128);
          Projectile.aiStyle = -1;
       Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.alpha = 40;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
         Projectile.scale = 0.8f;
            Projectile.localNPCHitCooldown = 10;
       //     DrawOriginOffsetX = -9;
       //     DrawOriginOffsetY = -9;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            WeaponSpeed /= player.GetTotalAttackSpeed(DamageClass.Melee);
            WeaponSpeedUpAmount *= player.GetTotalAttackSpeed(DamageClass.Melee);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            var r = 58;
            hitbox.Width = r *= (int)Projectile.scale;
            hitbox.Height = r *= (int)Projectile.scale;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[0] >= MathHelper.TwoPi) { Projectile.ai[0] = 0; }
            Projectile.ai[0] += speed;

            Projectile.Center = player.Center + new Vector2(radius).RotatedBy(Projectile.ai[0]);
        }
    /*    public override void AI()
        {
           // Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<Hitbox>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
         /*   Projectile.timeLeft = 6;
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 2;
            player.itemTime = 2;
            if (!Main.mouseLeft)
            {
                Speed = MathHelper.Lerp(Speed, 1000f, 0.009f);
                Projectile.scale = MathHelper.Lerp(Projectile.scale, 0.8f, WeaponSpeedUpAmount);
                if (Speed >= 780f)
                {
                    Projectile.Kill();
                }
            }
            else
            {
               Projectile.scale = MathHelper.Lerp(Projectile.scale, 2.3f, 0.3f);
                Speed = MathHelper.Lerp(Speed, WeaponSpeed, WeaponSpeedUpAmount);
            }
             angle = Projectile.ai[1];
            Projectile.position.X = player.Center.X + (float)Math.Cos(angle)  * (distanceFromPlayer * Projectile.scale); //  Main.GetPlayerArmPosition(Projectile).X + (float)Math.Cos(Projectile.ai[1]) * (distanceFromPlayer * Projectile.scale) - (float)
            Projectile.position.Y = player.Center.Y + (float)Math.Sin(angle) * (distanceFromPlayer * Projectile.scale); //  Main.GetPlayerArmPosition(Projectile).Y + (float)Math.Sin(Projectile.ai[1]) * (distanceFromPlayer * Projectile.scale) - (float)
            Projectile.position.X -= (float)Projectile.width / 2; //  Main.GetPlayerArmPosition(Projectile).X + (float)Math.Cos(Projectile.ai[1]) * (distanceFromPlayer * Projectile.scale) - (float)
            Projectile.position.Y -= (float)Projectile.height / 2;
            rotation = (float)Math.PI / Speed;
            Projectile.ai[1] -= rotation;
            if (Projectile.ai[1] > (float)Math.PI)
            {
                Projectile.ai[1] -= (float)Math.PI * 4f;
                Projectile.netUpdate = true;
            }
            Projectile.rotation = angle + (float)Math.PI / 4f;
            Lighting.AddLight(Projectile.position, new Vector3(0f, 0.522f, 0.522f) * 2f);*/
        }
       /* public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Player player = Main.player[Projectile.owner];
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture3 = Mod.Assets.Request<Texture2D>("Items/Weapons/Melee/PutridMushrump").Value;
        Texture2D texture = Mod.Assets.Request<Texture2D>("Common/Textures/MeleeArc0").Value;
            Texture2D texture2 = Mod.Assets.Request<Texture2D>("Common/Textures/MeleeArc3").Value;
            var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);

            var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);

            /* for (int i = 0; i < Projectile.oldPos.Length; i++)
             {
                 var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);

                 Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + offset;
                 float sizec = Projectile.scale;
                 Color color = new Color(0, 122 * Projectile.oldPos.Length / 3, 122 * Projectile.oldPos.Length / 3, (1f - Speed / 55f)) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                 Color color2 = new Color(0, 150 * Projectile.oldPos.Length / 3, 150 * Projectile.oldPos.Length / 3, (1f - Speed / 55f)) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                 //   Color color = new Color(0, 122 * Projectile.oldPos.Length / 3, 122 * Projectile.oldPos.Length / 3, (1f - Speed / 55f) * (float)Projectile.oldPos.Length - i / (float)Projectile.oldPos.Length);
                 //   Color color2 = new Color(0, 150 * Projectile.oldPos.Length / 3, 150 * Projectile.oldPos.Length / 3, (1f - Speed / 55f) * Projectile.oldPos.Length - i / Projectile.oldPos.Length);


                 Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[i], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
                 Main.EntitySpriteDraw(texture2, drawPos, frame, color2,Projectile.oldRot[i], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
             }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }*/
    }

