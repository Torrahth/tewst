using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader.Utilities;

namespace tm.Npcs.Underground
{
    public class Cascade : ModNPC
    {
        int switchd = Main.rand.Next(1, 4);
        int phasenum = 0;
        int changephase = 0;
        int changenum = 0;
        int r = Main.rand.Next(1,255);
        int g = Main.rand.Next(1, 255);
        int b = Main.rand.Next(1, 255);
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 28;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit36;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.value = 20;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.frame.Y = Main.rand.Next(1, 6) * 28;
        }
        public override void OnKill()
        {
            var player = Main.player[NPC.target];
            NPC.TargetClosest(faceTarget: true);
            if (switchd == 1)
            {
                player.AddBuff(BuffID.Mining, 6000);
            }
            else if (switchd == 2)
            {
                player.AddBuff(BuffID.Spelunker, 6000);
            }
            else if (switchd == 3)
            {
                player.AddBuff(BuffID.Dangersense, 6000);
            }
            for (int i = 0; i < 20; i++)
            {
                var dusts = Main.rand.NextVector2CircularEdge(3, 3);
               var ra = Dust.NewDustPerfect(NPC.Center + dusts * 7, DustID.WhiteTorch, dusts , 0, new Color(r, g, b), Scale: 2);
                ra.noGravity = true;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime) // dont mine at night
            {
                return SpawnCondition.Cavern.Chance * 0.1f;
            }
            else
            {
                return SpawnCondition.Cavern.Chance * 0.05f;
            }


        }
        public override void AI()
        {
         
            var player = Main.player[NPC.target];
            NPC.TargetClosest(faceTarget: true);
            if (NPC.Center.Distance(player.Center) < 70)
            {
                NPC.ai[0] = 1;
            }
            switch (NPC.ai[0])
            {
                case 0: // spinning around
                    {
                        NPC.rotation += NPC.ai[1] + (NPC.velocity.X * 0.03f);
                        if (NPC.velocity.X < 3)
                        {
                            NPC.velocity.X += NPC.ai[2];
                        }
                        else if (NPC.velocity.Y > 3)
                        {
                            NPC.velocity.X -= NPC.ai[2];
                        }

                        if (NPC.velocity.Y < 3)
                        {
                            NPC.velocity.Y += NPC.ai[3];
                        }
                        else if (NPC.velocity.Y > 3)
                        {
                            NPC.velocity.Y -= NPC.ai[3];
                        }


                        phasenum++;
                        if (phasenum >= Main.rand.Next(23, 190) || NPC.velocity.Length() > 0)
                        {
                            NPC.ai[1] = Main.rand.NextFloat(-0.02f, 0.02f);
                            NPC.ai[2] = Main.rand.NextFloat(-0.9f, 0.9f);
                            NPC.ai[3] = Main.rand.NextFloat(-0.9f, 0.9f);
                            phasenum = 0;

                        }
                        if (++changephase >= 300)
                        {
                            changephase = 0;
                            NPC.ai[0] = 1;
                        }
                    }
                    break;
                case 1: // symbols/computing
                    {
                        changenum++;
                        if (changenum >= 18)
                        {
                            SoundEngine.PlaySound(SoundID.Item65, NPC.Center);
                            Lighting.AddLight(NPC.Center, new Vector3(r, g, b) / 228);
                            ChangeFace();
                            changenum = 0;
                            NPC.velocity.X = Main.rand.NextFloat(-2.9f, 2.9f);
                            NPC.velocity.Y = Main.rand.NextFloat(-2.9f, 2.9f);
                        }
                        NPC.velocity /= 1.06f;
                        if (++changephase >= 100)
                        {
                            changephase = 0;
                            NPC.ai[0] = 0;
                        }
                    }
                    break;
            }
         
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return new Color(r,g,b, 0) * (1f - NPC.alpha);
        }
        public void ChangeFace()
        {
            NPC.frame.Y = Main.rand.Next(1, 6) * 28;
        }
        }
}
