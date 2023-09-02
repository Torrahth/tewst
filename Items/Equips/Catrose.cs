using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Enums;
using tmt.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace tmt.Items.Equips
{
    public class Catrose : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 1;
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Catrose>());
            Item.accessory = true;
        }
        public override void UpdateInventory(Player player)
        {
            if (!Main.dayTime && Main.moonPhase == 0 && Main.moonType == 4)
            {
                Item.value = Item.sellPrice(1, 0, 0, 0);
            }
            else
            {
                Item.value = Item.sellPrice(0, 1, 0, 0);
            }
        }
        public override void UpdateEquip(Player player)
        {
            if (Main.rand.NextBool(72) && player.ownedProjectileCounts[ModContent.ProjectileType<ACatRose>()] < 5) {
                SoundEngine.PlaySound(new SoundStyle("tmt/Common/Sounds/Mew") with
                {
                    Volume = 0.8f,
                    PitchVariance = 0.15f,
                });
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<ACatRose>(), 0, 0, player.whoAmI);
            }
        }
        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<tmtplayer>().CatRose = true;
            if (Main.rand.NextBool(72) && player.ownedProjectileCounts[ModContent.ProjectileType<AFriendlyCatRose>()] < 5)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<AFriendlyCatRose>(), 0, 0, player.whoAmI);
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (!Main.dayTime && Main.moonPhase == 0 && Main.moonType == 4)
            {
                tooltips.Add(new TooltipLine(Mod, "Quote!", "[c/d8fffc:Have a Glimpse at the moon tonight, isn't it Joyous]"));
            }
            tooltips.Add(new TooltipLine(Mod, "Zealous!", "[c/F37D45:Dedicated!]"));

        }
    }
}
