using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using tm.Items.Equips;
using Terraria.ID;

namespace tm
{
    public class tmworldgen : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int c = 0; c < 8000; c++)
            {
                Chest chest = Main.chest[c];
                if (chest == null)
                {
                    continue;
                }
                Tile tile = Framing.GetTileSafely(chest.x, chest.y);
                if (tile.TileType != 21)
                {
                    continue;
                }
                switch (tile.TileFrameX)
                {
                    case 540:
                        {
                            if (!Utils.NextBool(WorldGen.genRand, 3))
                            {
                                break;
                            }
                            for (int k = 0; k < 40; k++)
                            {
                                if (chest.item[k].type == ItemID.WebSlinger)
                                {
                                    chest.item[k].SetDefaults(ModContent.ItemType<Obometer>());
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}
