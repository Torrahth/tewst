using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tmt.Items.Misc
{
    public class Deworminator : ModItem

    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("worms violently explode and you dont have to deal with them");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Master;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            for (int ia = 0; ia < Main.maxNPCs; ia++)
            {
                NPC npc = Main.npc[ia];
                if (npc.active == true && (npc.type == NPCID.GiantWormHead || npc.type == NPCID.DevourerHead || npc.type == NPCID.DuneSplicerHead || npc.type == NPCID.DiggerHead || npc.type == NPCID.TombCrawlerHead))
                {
                        npc.active = false;
                }
                else
                {
                    continue;
                }
               

            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 7)
                    .AddIngredient(ItemID.WhoopieCushion)
                  .AddIngredient(ItemID.Worm)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
    .AddIngredient(ItemID.LeadBar, 7)
        .AddIngredient(ItemID.WhoopieCushion)
      .AddIngredient(ItemID.Worm)
    .AddTile(TileID.Anvils)
    .Register();
        }

    }
}
