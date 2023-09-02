using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using tmt.Npcs.BossEnemy;
using Microsoft.Xna.Framework;
using tmt.Items.Equips;
using Microsoft.Xna.Framework.Graphics;

namespace tmt.Tiles
{
    public class Catrose : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;
            DustType = DustID.Astra;
            Main.tileSpelunker[Type] = false;
            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);
            Main.tileLighted[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;
            // Etc
            AddMapEntry(new Color(176, 255, 254), Language.GetText("Catrose"));
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (Main.rand.NextBool(7))
            {
                Dust d = Dust.NewDustPerfect(new Vector2(i * 16 + Main.rand.Next(-12, 12), j * 18 + Main.rand.Next(-12, 12)), DustID.BlueFlare, new Vector2(-6, -4), 0);
                d.noGravity = true;
            }
        }
        public override bool IsTileSpelunkable(int i, int j)
        {
            return true;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {

            r = 0.176f;
            g = 0.255f;
            b = 0.254f;
        }
    }
}
