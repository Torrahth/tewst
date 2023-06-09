﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace tm.Items.Equips
{
    public class Obometer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases Attackspeed at the cost of your Life!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 33, 0);
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.12f;
            player.statLifeMax2 -= 20;
        }
    }
}
