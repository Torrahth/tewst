using Terraria;
using Terraria.ModLoader;

namespace tm.Items.Misc
{
    public class ActiveCoil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.stack = 999;
            Item.buyPrice(0, 0, 10);
            Item.material = true;
        }
    }
}
