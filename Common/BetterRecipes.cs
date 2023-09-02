using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace tmt.Common
{
    public class BetterRecipes : ModSystem
    {
        public override void AddRecipeGroups() // i took this code from (Basically im a little cat)!
        {
            RecipeGroup.RegisterGroup(nameof(ItemID.SilverBar), new(
        () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
        ItemID.SilverBar, ItemID.TungstenBar
    ));
            RecipeGroup.RegisterGroup(nameof(ItemID.CobaltBar), new(
        () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CobaltBar)}",
        ItemID.CobaltBar, ItemID.PalladiumBar
    ));
             RecipeGroup.RegisterGroup(nameof(ItemID.DemoniteBar), new(
        () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.DemoniteBar)}",
        ItemID.DemoniteBar, ItemID.CrimtaneBar
    ));
             RecipeGroup.RegisterGroup(nameof(ItemID.VilePowder), new(
        () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.VilePowder)}",
        ItemID.VilePowder, ItemID.ViciousPowder
    ));
        }
    }
}
