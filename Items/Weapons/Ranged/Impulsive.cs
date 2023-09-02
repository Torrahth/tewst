using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace tmt.Items.Weapons.Ranged
{
    public class Impulsive : ModItem
    {
        public override void SetStaticDefaults()
        {
             // Tooltip.SetDefault("Fires two skulls that ram into your enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 30;
            Item.useTime = 30;
            Item.knockBack = 1;
            Item.useAnimation = 30;
            Item.reuseDelay = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 36;
            Item.crit = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 1, 40);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;   
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.FleshPike>();
            Item.shootSpeed = 22;
            Item.rare = ItemRarityID.LightRed;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                var newvelocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
                newvelocity *= 1 + Main.rand.NextFloat(-0.3f, 0.3f);
                Projectile.NewProjectile(source, position, newvelocity, ModContent.ProjectileType<Projectiles.Ranged.FleshPike>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Harpoon)
                .AddIngredient(ItemID.SoulofNight, 6)
                     .AddIngredient(ItemID.EbonstoneBlock, 30)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
              .AddIngredient(ItemID.Harpoon)
              .AddIngredient(ItemID.SoulofNight, 6)
                   .AddIngredient(ItemID.CrimstoneBlock, 30)
              .AddTile(TileID.MythrilAnvil)
              .Register();
        }
    }
}
