using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using tmt.Projectiles.Mage;

namespace tmt.Items.Weapons.Mage
{
    public class Contrary : ModItem
    {
        public override void SetStaticDefaults()
        {
             // Tooltip.SetDefault("Forbidden tin, Forever cursed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.reuseDelay = 30;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 36;
            Item.crit = 1;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.UseSound = SoundID.Item78;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MagicSpark>();
            Item.shootSpeed = 22;
            Item.mana = 6;
            Item.rare = ItemRarityID.Orange;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(17))
            {
                player.statLife -= 1;
            }
  
            Projectile.NewProjectile(source, position, velocity.RotatedBy((MathHelper.Pi * Main.rand.Next(-3, 3))) * velocity.RotatedByRandom(MathHelper.ToRadians(32)), type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulDrain)
                    .AddIngredient(ItemID.TinBar, 12)
                  .AddIngredient(ItemID.TitaniumBar, 12)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
