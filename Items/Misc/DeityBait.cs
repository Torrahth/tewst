using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tmt.Items.Misc
{
    public class DeityBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Chance to give lesser bait when consumed");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.buyPrice(0, 0, 30);
            Item.material = true;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.bait = 45;
            Item.rare = ItemRarityID.LightRed;
        }
        public override bool? CanConsumeBait(Player player)
        {
            var soluck = player.luck;
            int masterbaitchance = 12 - (int)soluck;
            int journeybaitchance = 9 - (int)soluck;
            int appricebaitchance = 6 - (int)soluck;

            var d = Main.rand.Next(0, 5);
            // ngl im just that good at coding fr
            switch (d)
            {
                case 0:
                    {
                        if (Main.rand.NextBool(masterbaitchance)) 
                        {
                            player.QuickSpawnItem(player.GetSource_DropAsItem(), ItemID.MasterBait, Main.rand.Next(1, 3));
                        }
                
                    }
                    break;
                case 1:
                    {
                        if (Main.rand.NextBool(journeybaitchance))
                        {
                            player.QuickSpawnItem(player.GetSource_DropAsItem(), ItemID.JourneymanBait, Main.rand.Next(2, 6));
                        }
                    }
                    break;
                case 2:
                    {
                        if (Main.rand.NextBool(appricebaitchance))
                        {
                            player.QuickSpawnItem(player.GetSource_DropAsItem(), ItemID.ApprenticeBait, Main.rand.Next(4, 8));
                        }
                    }
                    break;
            }
            return base.CanConsumeBait(player);
        }
    }
}
