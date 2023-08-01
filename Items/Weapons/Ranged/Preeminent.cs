using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace tm.Items.Weapons.Ranged
{
    public class Preeminent : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Plant growing roots into your foe");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 30;
            Item.useTime = 10;
            Item.knockBack = 1;
            Item.useAnimation = 30;
            Item.reuseDelay = 22;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 36;
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
    }
    public class preeminetheld : ModProjectile
    {
        public override string Texture => "tm/Items/Weapons/Ranged/Preeminent";
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
        }

        float plusRoto = 45f;

        float Distance = 20f;

        SpriteEffects spritedirectionvertical = SpriteEffects.None;

//        private Vector2 mouse = new Vector2();

        int overheatmeter;

        int shoottimer = 40;

        public override void AI()
        {
          
            Player player = Main.player[Projectile.owner];
            var mouse = player.Center.DirectionTo(Main.MouseWorld);

            player.heldProj = Projectile.whoAmI;
            var Rotation = Projectile.rotation = Projectile.AngleTo(Main.MouseWorld);
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
            Projectile.rotation = mouse.ToRotation() + 0.785398f + Projectile.ai[0];
            Projectile.Center = player.Center + ((Distance * Projectile.scale) * (Projectile.rotation - 0.785398f).ToRotationVector2());
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - 2.35619f);

            // actual shooting
            if (++shoottimer >= 40)
            {
                for (int i = 0; i < 5; i++)
                {
                    var newvelocity = new Vector2(0, 12).RotatedBy(mouse.ToRotation() - Math.PI / 2);//.RotatedByRandom(MathHelper.ToRadians(22));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, newvelocity.RotatedByRandom(MathHelper.ToRadians(22)), (int)Projectile.ai[1], Projectile.damage, 2, player.whoAmI);
                }
                shoottimer = 0;
            }
    


          //  Projectile.position = player.MountedCenter + new Vector2(Distance, 0f).RotatedBy(Projectile.ai[1]);
        //    Projectile.position.X -= Projectile.width / 2f;
        //    Projectile.position.Y -= Projectile.height / 2f;
            Projectile.rotation = Rotation;
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
