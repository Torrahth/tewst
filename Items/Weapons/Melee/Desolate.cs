using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using tm.Projectiles.Melee;
using Terraria.GameContent.Creative;

namespace tm.Items.Weapons.Melee
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
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 21;
            Item.knockBack = 1;
            Item.useAnimation = 21;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 23;
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
                     .AddIngredient(ItemID.DemoniteBar, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
              .AddIngredient(ItemID.WoodenSword)
              .AddIngredient(ItemID.Acorn, 8)
                   .AddIngredient(ItemID.CrimtaneBar, 6)
              .AddTile(TileID.WorkBenches)
              .Register();

        }

    }
}
