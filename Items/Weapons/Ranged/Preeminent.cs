using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using tmt.Projectiles.Ranged;
using Terraria.Audio;
using tmt.Common;

namespace tmt.Items.Weapons.Ranged
{
    public class Preeminent : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 30;
            Item.knockBack = 1;
            Item.useTime = 1;
            Item.useAnimation = 1;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 46;
            Item.crit = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 1, 40);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<preeminetheld>();
            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.LightRed;
            Item.channel = true;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<preeminetheld>(), damage, knockback, player.whoAmI, ai1: type);
       //     Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: type);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.QuadBarrelShotgun)
                .AddIngredient(ItemID.CobaltBar, 15)
                     .AddIngredient(ItemID.EyeoftheGolem)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
       .AddIngredient(ItemID.QuadBarrelShotgun)
       .AddIngredient(ItemID.PalladiumBar, 15)
            .AddIngredient(ItemID.EyeoftheGolem)
       .AddTile(TileID.MythrilAnvil)
       .Register();
        }
    }
    public class preeminetheld : ModProjectile
    {
        public override string Texture => "tmt/Items/Weapons/Ranged/Preeminent";
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            DrawOriginOffsetX = 2;
        }

        int timeshot = 0;

        float plusRoto = 45f;

        float Distance = 20f;

        SpriteEffects spritedirectionvertical = SpriteEffects.None;

//        private Vector2 mouse = new Vector2();

        int overheatmeter;

        float shoottimer = 9;

        int effecttimer = 0;
        public override void AI()
        {
          
            Player player = Main.player[Projectile.owner];
            var mouse = player.Center.DirectionTo(Main.MouseWorld);

            var r = mouse.ToRotation() + 0.785398f + Projectile.ai[0]; ;

            player.heldProj = Projectile.whoAmI;
            var Rotation = r = Projectile.AngleTo(Main.MouseWorld);
            if (Main.mouseLeft == false)
            {
                Projectile.Kill();
            }
            player.itemAnimation = 2;
            player.itemTime = 2;
         
            if (Main.MouseWorld.X > player.Center.X)
            {
                player.ChangeDir(1);
                spritedirectionvertical = SpriteEffects.None;
            }// hot code
     
            else if (Main.MouseWorld.X < player.Center.X)
            {
                player.ChangeDir(-1);
                spritedirectionvertical = SpriteEffects.FlipVertically;
            }
             r =  mouse.ToRotation() + 0.785398f + Projectile.ai[0];
            Projectile.Center = player.Center + ((Distance * Projectile.scale) * (r - 0.785398f).ToRotationVector2());
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, r - 2.35619f);

            float water = player.GetAttackSpeed(DamageClass.Ranged);
            // actual shooting
            if (++shoottimer >= 10 - water)
            {
                if (shoottimer == 10 - water) // im so good at coding
                {
                    timeshot++;
                    Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.1f, 0.4f);
                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
                    Shoot(player, mouse);
                    for (int i = 0; i < 12; i++)
                    {
                        Dust.NewDustPerfect(Projectile.Center + ((Distance * Projectile.scale) * (r - 0.785398f).ToRotationVector2()), DustID.Smoke, Main.rand.NextVector2Circular(1, 1), 44);
                    }
                }
                if (++effecttimer >= 4)
                {
                    if (++effecttimer >= 6)
                    {
                        Distance = 20;
                        if (timeshot >= 3)
                        {
                            Projectile.rotation += 0.4f;
                            if (++effecttimer >= 90 / water)
                            {
                                Projectile.rotation = Rotation;
                                Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.2f, 0.6f);
                                timeshot = 0;
                                effecttimer = 0;
                                shoottimer = 9 - water;
                                Projectile.Kill();
                            }
                            else if (effecttimer == 8)
                            {
                             
                                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, Projectile.Center);
                            }
                        }
                        else
                        {
                            effecttimer = 0;
                            shoottimer = 0;
                        }
     
                    }
                    else
                    {
                        Distance += 4;
                    }


                }
                else
                {
                    Distance -= 2;
                }
            }
            else
            {
                Projectile.rotation = Rotation;
            }

          


            //  Projectile.position = player.MountedCenter + new Vector2(Distance, 0f).RotatedBy(Projectile.ai[1]);
            //    Projectile.position.X -= Projectile.width / 2f;
            //    Projectile.position.Y -= Projectile.height / 2f;
      
        }
        private void Shoot(Player player, Vector2 mouse)
        {
              for (int i = 0; i < 5; i++)
            {
                var newvelocity = new Vector2(0, Main.rand.Next(35, 61)).RotatedBy(mouse.ToRotation() - Math.PI / 2);//.RotatedByRandom(MathHelper.ToRadians(22));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, newvelocity.RotatedByRandom(MathHelper.ToRadians(16)), ModContent.ProjectileType<ShotgunShell>(), Projectile.damage, 2, player.whoAmI);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture2 = TextureAssets.Projectile[Projectile.type].Value;

            int frameHeight = texture2.Height / Main.projFrames[Projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, texture2.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;

            Main.spriteBatch.Draw(texture2, (Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) * -5), sourceRectangle, Color.White * (1 - Projectile.alpha), Projectile.rotation, origin, Projectile.scale, spritedirectionvertical, 0);

            return false;
        }
    }
}
