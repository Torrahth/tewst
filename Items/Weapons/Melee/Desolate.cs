using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using tmt.Projectiles.Melee;
using Terraria.GameContent.Creative;

namespace tmt.Items.Weapons.Melee
{
    public class Desolate : ModItem
    {
        public override void SetStaticDefaults()
        {
             // Tooltip.SetDefault("Right click to spawn a Yarn of Roots");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 92;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 43;
            Item.crit = 2;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PlantCore>();
            Item.shootSpeed = 12;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1.2f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

                Item.shoot = ModContent.ProjectileType<PlantCore>();
                Item.shootSpeed = 12;
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
            else
            {
                Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.Acorn, 8)
                .AddRecipeGroup(nameof(ItemID.DemoniteBar), 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

    }
}
