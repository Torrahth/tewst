using Terraria;
using Terraria.ModLoader;

namespace tmt.Items.Misc
{
    public class ActiveCoil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.maxStack = 999;
            Item.buyPrice(0, 0, 10);
            Item.material = true;
        }
    }
}
