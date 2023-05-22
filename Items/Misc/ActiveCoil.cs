using Terraria;
using Terraria.ModLoader;

namespace tm.Items.Misc
{
    public class ActiveCoil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Despite what you think it is, it provides little to no Energy");
        }
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
