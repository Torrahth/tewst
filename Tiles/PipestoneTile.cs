using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace tmt.Tiles
{
    public class PipestoneTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = false;
            Main.tileSpelunker[Type] = false; 
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Pipestone");
            AddMapEntry(new Color(152, 171, 198), name);
            // dustType = 84;
            //   soundType = SoundID.Tink;
            //soundStyle = 1;
            //mineResist = 4f;
            //minPick = 200;
        }

    }
    public class Pipestone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Incredibly durable material designed to last for very long periods of time");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<PipestoneTile>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(4)
            .AddIngredient(ItemID.MudBlock)
             .AddIngredient(ItemID.ClayBlock)
  .AddCondition(Condition.NearHoney)
            .Register();
            CreateRecipe()
                  .AddIngredient(ModContent.ItemType<PipestoneWallItem>(), 4)
                        .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
    public class PipestoneWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            DustType = DustID.Mud; // temp because im lazy to make an ew dust
           // ItemDrop = ModContent.ItemType<Items.Placeable.ExampleWall>();

            AddMapEntry(new Color(150, 150, 150));
        }
    }
    public class PipestoneWallItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<PipestoneWall>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(4)
                 .AddIngredient(ModContent.ItemType<Pipestone>())
                 .AddTile(TileID.WorkBenches)
           .Register();
        }
    }
}
