using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using tm.Projectiles.Ranged;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace tm.Items.Weapons.Ranged
{
    public class ObliqueDownpour : ModItem
    {
        float a = -6 ;
        public override void SetStaticDefaults()
        {
             Tooltip.SetDefault("When crystals reach the point of the mouse when spawned they may explode into many groups, However bring the mouse farther or being a enemy it will not split");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 76;
            Item.useTime = 3;
            Item.useAnimation = 8;
            Item.reuseDelay = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 29; 
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<StrangeCrystak>();
            Item.shootSpeed = 12;
            Item.ammo = AmmoID.Arrow;

            Item.rare = ItemRarityID.LightRed;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("tm/Common/Textures/ObliqueDownpourGlowmask", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Item.useAnimation <= 8)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, player.Center);
            }
                a += 3f;
            velocity = velocity.RotatedBy(MathHelper.ToRadians(a));
            if (a >= 6)
            {
                a = -6;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Marrow)
                  .AddIngredient(ItemID.CrystalShard, 10)
                     .AddIngredient(ItemID.HellstoneBar, 6)
                    .AddIngredient(ItemID.DemoniteBar, 6)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
