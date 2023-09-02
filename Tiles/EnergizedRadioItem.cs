using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace tmt.Tiles
{
    public class EnergizedRadioItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Energized Radio");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<EnergizedRadio>());
            Item.width = 20;
            Item.height = 22;
            Item.value = 300;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IronBar, 6)
                .AddIngredient(ItemID.DartTrap)
            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
.AddIngredient(ItemID.LeadBar, 6)
    .AddIngredient(ItemID.DartTrap)
.AddTile(TileID.Anvils)
.Register();
        }
    }
}
