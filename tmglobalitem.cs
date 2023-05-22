using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using tm.Items.Equips;
using Terraria.ID;
using Terraria.IO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Generation;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.GameContent.ItemDropRules;
using tm.Items.Misc;

namespace tm
{
    public class tmglobalitem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.WoodenCrateHard)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeityBait>(), 12, 1, 2));
            }
            if (item.type == ItemID.IronCrateHard)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeityBait>(), 6, 1, 3));
            }
            if (item.type == ItemID.GoldenCrateHard)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeityBait>(), 4, 1, 4));
            }
        }

    }
}
