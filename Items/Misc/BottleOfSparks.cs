using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using tmt.Tiles;

namespace tmt.Items.Misc
{
    public class BottleOfSparks : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.maxStack = 999;
            Item.buyPrice(0, 0, 10);
            Item.material = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                 .AddIngredient(ItemID.Bottle)
           .AddCondition(Condition.NearLava)
           .Register();
        }
    }
}
