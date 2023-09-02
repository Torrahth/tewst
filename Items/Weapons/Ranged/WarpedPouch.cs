using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using tmt.Projectiles.Ranged;

namespace tmt.Items.Weapons.Ranged
{
    public class WarpedPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 5;
            Item.useAnimation = 12;
            Item.reuseDelay = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 24;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<RoomyStars>();
            Item.shootSpeed = 8;
            Item.UseSound = SoundID.Item1;
            Item.useAmmo = AmmoID.FallenStar;

            Item.rare = ItemRarityID.LightRed;

            Item.consumeAmmoOnFirstShotOnly = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar)
                .AddRecipeGroup(nameof(ItemID.VilePowder), 9)
                    .AddIngredient(ItemID.Bone, 30)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
            type = ModContent.ProjectileType<RoomyStars>();
        }
    }
}
