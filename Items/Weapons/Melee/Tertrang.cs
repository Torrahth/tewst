using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using tmt.Projectiles.Melee;

namespace tmt.Items.Weapons.Melee
{
    public class Tertrang : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 46;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.noMelee = true;
            Item.rare = 2;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Tertrang>();
            Item.shootSpeed = 7;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WoodenBoomerang)
                 .AddRecipeGroup(nameof(ItemID.DemoniteBar), 7)
                 .AddIngredient(ItemID.Emerald, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 2;
        }
    }
}
