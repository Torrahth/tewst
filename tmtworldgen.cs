using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using tmt.Items.Equips;
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
using tmt.Tiles;
using Terraria.ObjectData;
using StructureHelper;

namespace tmt
{

    public class tmworldgen : ModSystem
    {

        public class waterworld : GlobalTile
        {

            public override void RandomUpdate(int i, int j, int type)
            {
                if (WorldGen.genRand.NextBool(53))
                {
                    int y = WorldGen.genRand.Next((int)GenVars.worldSurface, Main.maxTilesY);
                    Tile tile = Main.tile[i - 1, y - 1];
                    if (tile.TileType == TileID.JungleGrass && type != ModContent.TileType<Tiles.Catrose>())
                    {
                        WorldGen.PlaceTile(i, y, ModContent.TileType<Tiles.Catrose >(), mute: true);
                    }
                }

            }
        }
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
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {

            // Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

            // The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
            // First, we find out which step "Shinies" is.
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            if (ShiniesIndex != -1)
            {
                // Next, we insert our pass directly after the original "Shinies" pass.
                // ExampleOrePass is a class seen bellow
                tasks.Insert(ShiniesIndex + 1, new Garth("Radios", 237.4298f));
            }

            int underocean = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (underocean != -1)
            {
                // Next, we insert our pass directly after the original "Shinies" pass.
                // ExampleOrePass is a class seen bellow
                tasks.Insert(underocean + 1, new Meadow("Over Meadow", 237.4298f));
            }
        }
        public class Garth : GenPass
        {
            int tpe = 3;
            public Garth(string name, float loadWeight) : base(name, loadWeight)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Radios";

                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-04); k++)
                {
                    int x = WorldGen.genRand.Next(12, Main.maxTilesX - 12);
                    int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);
     
                    // generatestructure(x, y);  im so fucking tired of this shit i spent so fucking long trying to do this and i fucking suck im using sturcutre helper fuck you
                    Tile tile = Main.tile[x, y - 1];
                    if ((!tile.HasTile) && Main.tile[x, y + 1].HasTile)
                    {
              
                            //   helper.TryFind("Generator", out ModSystem Generator);
                        WorldGen.PlaceObject(x, y - 1, ModContent.TileType<EnergizedRadio>());
                        //   WorldGen.PlaceChest(x , y - 1, (ushort)TileID.Containers, style: 1);
                        // WorldUtils.Gen(new Point(x, y ), new Shapes.Rectangle(2, 2), new Actions.SetTile((ushort)TileID.StoneSlab));

                    }

                }
               
            }

        }
            /*      public readonly int[,] shape = {
          { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
          { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
          { 1, 1, 1, 2, 2, 2, 2, 2, 1, 1},
          { 0, 0, 0, 2, 3, 3, 0, 2, 0, 0},
          { 0, 0, 0, 4, 3, 0, 0, 4, 0, 0},
          { 0, 0, 0, 4, 0, 0, 0, 4, 0, 0},
          { 0, 0, 0, 4, 0, 0, 0, 4, 0, 0},
          { 0, 0, 2, 4, 5, 5, 0, 5, 2, 0},
      };*/
            /*        private readonly int[,] _shape = new int[18, 7]
                    {
                    {0,0,3,1,4,0,0 },
                    {0,3,1,1,1,4,0 },
                    {3,1,1,1,1,1,4 },
                    {5,5,5,6,5,5,5 },
                    {5,5,5,6,5,5,5 },
                    {5,5,5,6,5,5,5 },
                    {2,1,5,6,5,1,2 },
                    {1,1,5,5,5,1,1 },
                    {1,1,5,5,5,1,1 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,5,5,5,1,0 },
                    {0,1,1,1,1,1,0 },
                };
                    public bool generatestructure(int i, int j)
                    {
                        int[,] shape3 = _shape;


                        for (int y = 0; y < shape3.GetLength(0); y++)
                        {
                            for (int x = 0; x < shape3.GetLength(1) ; x++)
                            {
                                int k = i + _shape.GetLength(0) / 2 + x;
                                int l = j + _shape.GetLength(1) / 2 + y;
                                if (shape3[y, x] == 1)
                                {
                                    Tile val = Framing.GetTileSafely(k, l); //Main.tile[k, l];
                                    val.ClearTile();
                                    val = Main.tile[k, l];
                                    if (!val.HasTile)
                                    {
                                        WorldGen.PlaceTile(k, l, val.TileType, false, false, -1, 0);
                                        //    Tile tile = Framing.GetTileSafely(k, l);//Main.tile[k, l];
                                        switch (_shape[y, x])
                                        {
                                            case 0:
                                                    val.TileType = TileID.Dirt;
                                                break;
                                            case 1:
                                                    val.TileType = TileID.BlueDynastyShingles;
                                                    val.HasTile = true;
                                                break;
                                            case 2:
                                                    val.TileType = (ushort)ModContent.TileType<PipestoneTile>();
                                                    val.HasTile = true;
                                                break;
                                            case 3:
                                                    val.TileType = TileID.Cobweb;
                                                    val.HasTile = true;
                                                break;
                                            case 4:
                                                    val.TileType = TileID.WoodenBeam;
                                                    val.HasTile = true;
                                                break;
                                            case 5:
                                                    val.TileType = TileID.DynastyWood;
                                                    val.HasTile = true;
                                                break;
                                        }

                                          Tile tile = Main.tile[x, y];
                                           for (int i = x; i < x + shape.GetLength(1); i++)
                                           {
                                               for (int j = y; j < y; j++)
                                               {
                                                   if (shape[i, j] == 1)
                                                   { 
                                                       tpe = TileID.GoldBrick;
                                                   }
                                                   else
                                                   {
                                                       tpe = TileID.TinBrick;
                                                   }
                                                   WorldGen.PlaceTile(i, j, tpe, mute: true, forced: true);
                                                   WorldGen.SlopeTile(i, j);
                                               }
                                           }

                                            }


                                        }
                                    }
                                }
                            }
                        }
                        return true;
                    }
                }*/

            public class Meadow : GenPass
            {
                int rx;
                int ry;
                int type;
                public Meadow(string name, float loadWeight) : base(name, loadWeight)
                {
                }

                protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
                {
                    progress.Message = "Over Meadow";

                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-06); k++)
                    {
                    int x = WorldGen.genRand.Next(132, Main.maxTilesX - 132);
                    int y = WorldGen.genRand.Next((int)GenVars.rockLayer, (Main.maxTilesY - 200));
                    type = Main.rand.Next(1, 8);
                    rx = Main.rand.Next(3, 12);
                    ry = Main.rand.Next(3, 12);
                    var d = new Point16(0, 0);
                    ModLoader.TryGetMod("StructureHelper", out Mod helper);
                    Tile tile = Main.tile[x, y - 1];
                        if (tile.TileType == TileID.Stone || tile.TileType == TileID.Dirt)
                        {


                        ShapeData shapeData = new ShapeData();
                        if (type == 1)
                            {
       
                            WorldUtils.Gen(new Point(x, y), new Shapes.Circle(9, 9), new Actions.Clear().Output(shapeData));
                            WorldUtils.Gen(new Point(x, y + 4), new Shapes.Circle(6, 6), new Actions.SetLiquid((ushort)LiquidID.Water));
                            WorldUtils.Gen(new Point(x - 3, y + 2), new Shapes.Rectangle(1, rx), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x + 3, y + 2), new Shapes.Rectangle(1, ry), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x - 9, y + 1), new Shapes.Rectangle(19, 1), new Actions.SetTile(TileID.LivingWood));
                            WorldUtils.Gen(new Point(x, y - 1), new Shapes.Circle(1, 1), new Actions.SetTile(TileID.GoldCoinPile));
                            }
                            else if (type == 2)
                            {
                            WorldUtils.Gen(new Point(x, y), new Shapes.Circle(9, 9), new Actions.Clear().Output(shapeData));
                            WorldUtils.Gen(new Point(x, y + 4), new Shapes.Circle(6, 6), new Actions.SetLiquid((ushort)LiquidID.Water));
                            WorldUtils.Gen(new Point(x - 3, y + 2), new Shapes.Rectangle(1, rx), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x + 3, y + 2), new Shapes.Rectangle(1, ry), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x - 9, y + 1), new Shapes.Rectangle(19, 1), new Actions.SetTile(TileID.LivingWood));
                            WorldGen.digTunnel(x, y, -1, 1, 2, 2, false);
                            }
                            else if (type == 3)
                            {
                            WorldUtils.Gen(new Point(x, y), new Shapes.Circle(9, 9), new Actions.Clear().Output(shapeData));
                            WorldUtils.Gen(new Point(x, y + 4), new Shapes.Circle(6, 6), new Actions.SetLiquid((ushort)LiquidID.Water));
                            WorldUtils.Gen(new Point(x - 3, y + 2), new Shapes.Rectangle(1, rx), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x + 3, y + 2), new Shapes.Rectangle(1, ry), new Actions.SetTile(TileID.WoodenBeam));
                            WorldUtils.Gen(new Point(x - 9, y + 1), new Shapes.Rectangle(19, 1), new Actions.SetTile(TileID.LivingWood));
                            WorldUtils.Gen(new Point(x - 18, y), new Shapes.Circle(9, 9), new Actions.Clear().Output(shapeData));
                                WorldUtils.Gen(new Point(x - 18, y + 4), new Shapes.Circle(6, 6), new Actions.SetLiquid((ushort)LiquidID.Water));
                                WorldUtils.Gen(new Point(x - 21, y + 2), new Shapes.Rectangle(1, rx), new Actions.SetTile(TileID.WoodenBeam));
                                WorldUtils.Gen(new Point(x - 15, y + 2), new Shapes.Rectangle(1, ry), new Actions.SetTile(TileID.WoodenBeam));
                                WorldUtils.Gen(new Point(x - 27, y + 1), new Shapes.Rectangle(19, 1), new Actions.SetTile(TileID.LivingWood));
                            }
                            else if (type == 4)
                        {
                            if (tile.TileType == TileID.Stone || tile.TileType == TileID.Dirt)
                            {
                                if (Main.rand.NextBool(4))
                                {


                                    var dims = new Point16(x, y);
                                    Generator.GetDimensions("Common/CandleStructure", ModContent.GetInstance<tmt>(), ref d, false);
                                    Generator.GenerateStructure("Common/CandleStructure", dims, ModContent.GetInstance<tmt>(), false, false);
                                }
                                else
                                {
                                    var list = new List<string> { "Common/cavea", "Common/caveb", "Common/cavec", "Common/caved", "Common/cavee" };
                                    var dims2 = new Point16(x, y);
                                    Random rnd = new Random();
                                    int index = rnd.Next(list.Count);
                                    Generator.GetDimensions(list[index], ModContent.GetInstance<tmt>(), ref d, false);
                                    Generator.GenerateStructure(list[index], dims2, ModContent.GetInstance<tmt>(), false, false);
                                }
                            }
                
                        }
                        else
                            {

                            if (tile.TileType == TileID.Stone)
                            {
                                if (Main.rand.NextBool(4))
                                {


                                    var dims = new Point16(x, y);
                                    Generator.GetDimensions("Common/CandleStructure", ModContent.GetInstance<tmt>(), ref d, false);
                                    Generator.GenerateStructure("Common/CandleStructure", dims, ModContent.GetInstance<tmt>(), false, false);
                                }
                                else
                                {
                                    var list = new List<string> { "Common/cavea", "Common/caveb", "Common/cavec", "Common/caved", "Common/cavee" };
                                    var dims2 = new Point16(x, y);
                                    Random rnd = new Random();
                                    int index = rnd.Next(list.Count);
                                    Generator.GetDimensions(list[index], ModContent.GetInstance<tmt>(), ref d, false);
                                    Generator.GenerateStructure(list[index], dims2, ModContent.GetInstance<tmt>(), false, false);
                                }
                            }
                        }


                        }

                    }
                }
            }
        }
    }