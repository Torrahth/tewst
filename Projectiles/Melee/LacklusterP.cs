using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using XPT.Core.Audio.MP3Sharp.Decoding;
using tm.Items.Weapons.Melee;
using Terraria.DataStructures;
using tm.Common;
using Terraria.Audio;

namespace tm.Projectiles.Melee
{
    public class LacklusterP : ModProjectile
    {
        public const int FadeInDuration = 4;
        public const int FadeOutDuration = 7;

        public float TotalDuration = 52;

        // The "width" of the blade
        public float CollisionWidth => 40f * Projectile.scale;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.manualDirectionChange = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1.5f;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            TotalDuration /= player.GetTotalAttackSpeed(DamageClass.Melee);
        

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(Main.rand.NextFloat(-0.1f, 0.1f), Main.rand.NextFloat(-0.1f, 0.1f)), Vector2.Zero, ModContent.ProjectileType<BloodSplatter>(), 0, 0, Projectile.owner);
            if (player.statLife >= player.statLifeMax2 / 2)
            {
                player.AddBuff(ModContent.BuffType<Buffs.WeaponSpeedBuff>(), 360);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(Main.rand.NextFloat(-0.1f, 0.1f), Main.rand.NextFloat(-0.1f, 0.1f)), Vector2.Zero, ModContent.ProjectileType<BloodSplatter>(), 0, 0, Projectile.owner);
            if (player.statLife >= player.statLifeMax2 / 2)
            {
                player.AddBuff(ModContent.BuffType<Buffs.WeaponSpeedBuff>(), 360);
            }

        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var f = player.Center - Main.MouseWorld;
            if (Timer == 1)
            {
              // Projectile.velocity.DirectionTo(f);
              //  Projectile.velocity = Projectile.velocity + new Vector2(0, 0.1f);
                //  Projectile.velocity = new Vector2(0, 0.1f).DirectionTo(f);
            }
            else if (Timer == 10)
            {
                
              /*  if (player.statLife >= player.statLifeMax2 / 2)
                {
                    player.AddBuff(ModContent.BuffType<Buffs.WeaponSpeedBuff>(), 360);
                 //   SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
    //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 12).RotatedBy(Projectile.velocity.ToRotation() + MathHelper.Pi + MathHelper.PiOver2), ModContent.ProjectileType<Wave>(), Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
                }*/
            
                Projectile.extraUpdates = 3;
                //      Projectile.velocity = new Vector2(0, 1).DirectionTo(f);
            }
            Timer += 1;
            if (Timer >= TotalDuration)
            {
                Projectile.Kill();
                return;
            }
            else
            {
                player.heldProj = Projectile.whoAmI;
            }

            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            SetVisualOffsets();
        }

        private void SetVisualOffsets()
        {
            const int HalfSpriteWidth = 32 / 2;
            const int HalfSpriteHeight = 32 / 2;

            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;

            DrawOriginOffsetX = 0;
            DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
            DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
        }
    }
}
