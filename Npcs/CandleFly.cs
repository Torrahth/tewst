using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.WorldBuilding;
using Terraria.ModLoader.Utilities;

namespace tm.Npcs
{
    public class CandleFly : ModNPC
    {
        public float LightTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 30;
          //  NPC.scale = Main.rand.NextFloat(0.7f, 1.9f);
            NPC.lifeMax = 25;
            NPC.damage = 10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 10;
            NPC.aiStyle = 64;
            NPC.noGravity = true;
            AnimationType = NPCID.Bird;
            AIType = NPCID.Firefly;

        }
        public override void PostAI()
        {
            var lightincreaser = (float)Math.Sin(LightTimer * 1.006f);
            Lighting.AddLight(NPC.Center, new Vector3(0.355f + lightincreaser, 0.355f + lightincreaser, 0.259f + lightincreaser));
            NPC.rotation = NPC.velocity.X * 0.004f;
            LightTimer += 0.05f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.raining)
            {
                return SpawnCondition.Overworld.Chance * 0.4f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
            }
      
       
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture2 = ModContent.Request<Texture2D>("tm/Common/Textures/FireflyLight").Value;
            Vector2 drawOrigin = texture2.Size() / 2f;
            Color color = new Color(255, 205, 56, 200) * (1f - (float)NPC.alpha / 255f) * ((NPC.oldPos.Length) / (float)NPC.oldPos.Length);

                Main.spriteBatch.Draw(texture2, NPC.Center - new Vector2(0, -8) - screenPos , null, color, NPC.rotation, drawOrigin, NPC.scale + (float)Math.Sin(LightTimer * 1.006f ), SpriteEffects.None, 1f);

    

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
      }
}
