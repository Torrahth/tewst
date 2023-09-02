using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using tmt.Npcs.BossEnemy;

namespace tmt.Tiles
{
    public class EnergizedRadio : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;
            DustType = DustID.BlueTorch;
            Main.tileSpelunker[Type] = false;
            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);
            // Etc
            AddMapEntry(new Color(200, 200, 200), Language.GetText("Energized Radio"));
        }
        public override bool IsTileSpelunkable(int i, int j)
        {
            return true;
        }
        public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<Windlass>()))
            {

                var n = NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, ModContent.NPCType<Windlass>());
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                }
            }
        }

    

        public override void RandomUpdate(int i, int j)
        {
                Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(0, 0.500f, 0));

            if (Main.rand.NextBool(999)) // you werent here.
            {
                SoundEngine.PlaySound(new SoundStyle("tmt/Common/Sounds/PortalRadio")
                {
                });
            }
        }
    }
}
