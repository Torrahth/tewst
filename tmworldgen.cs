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
using tm.Tiles;

namespace tm
{

    public class tmworldgen : ModSystem
    {


        public class TmWorld : ModSystem
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

        // general generation generals
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

            // The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
            // First, we find out which step "Shinies" is.
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -1)
            {
                // Next, we insert our pass directly after the original "Shinies" pass.
                // ExampleOrePass is a class seen bellow
                tasks.Insert(ShiniesIndex + 1, new Garth("Over Meadow", 237.4298f));
            }
        }
        public class Garth : GenPass
        {
            public Garth(string name, float loadWeight) : base(name, loadWeight)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Over Meadow";

                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-04); k++)
                {
                    int x = WorldGen.genRand.Next(12 , Main.maxTilesX - 12);
                    int y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY);

                    Tile tile = Main.tile[x, y - 1];
                    if ((!tile.HasTile) && Main.tile[x, y + 1].HasTile )
                    {

                        WorldGen.PlaceObject(x, y - 1, ModContent.TileType<EnergizedRadio>());
                     //   WorldGen.PlaceChest(x , y - 1, (ushort)TileID.Containers, style: 1);
                        // WorldUtils.Gen(new Point(x, y ), new Shapes.Rectangle(2, 2), new Actions.SetTile((ushort)TileID.StoneSlab));
                    }
                
                }
            }
        }
        }
}
