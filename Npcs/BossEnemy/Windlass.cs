using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.WorldBuilding;
using Terraria.ModLoader.Utilities;
using tmt.Projectiles.Enemy;
using Terraria.GameContent.ItemDropRules;
using tmt.Items.Misc;

namespace tmt.Npcs.BossEnemy
{
    public class Windlass : ModNPC
    {
        float x;
        float y;
        int mainai = 0;
        int animationstate;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 76;
            //  NPC.scale = Main.rand.NextFloat(0.7f, 1.9f);
            NPC.lifeMax = 625;
            NPC.damage = 12;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 9999;
            NPC.aiStyle = -1;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
        }
        private void Stomp()
        {
            animationstate = 1;
            y = y + 0.4f;
            if (y > 12f)
            {
                y = 12f;
            }
            x /= 1.01f;
            NPC.ai[1]++;
            if (NPC.ai[1] >= 130 && NPC.collideY == true)
            {
                animationstate = 2;
                x = NPC.direction * 3;//new Vector2(NPC.direction * 3, -13);
                y = -12;
                NPC.ai[1] = 0;

                NPC.ai[0] = 1;
            }
            NPC.velocity.X = x;
            NPC.velocity.Y = y;
        }
        private void Stomp2()
        {
            animationstate = 0;
            Player player = Main.player[NPC.target];
            y = y + 0.4f;
            if (y > 12f)
            {
                y = 12f;
            }
            x /= 1.01f;
            if (NPC.collideY == true)
            {
                mainai++;
                y = 0;
                x = 0;
                if (mainai >= 3)
                {
                    mainai = 0;
                    NPC.ai[0] = Main.rand.Next(2, 4);
                 
                    }
                else
                {
                    NPC.ai[0] = 0;
                }
                if (NPC.life <= NPC.lifeMax / 2)
                {
              for (int i = 0; i < 7; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, -6 + Main.rand.Next(-3, 3)).RotatedByRandom(MathHelper.ToRadians(56)), ModContent.ProjectileType<Rock>(), NPC.damage / 4, 4, NPC.target);
                }
                }
            

                SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
                for (int i = 0; i < 13; i++)
                {
                    Dust.NewDustPerfect(NPC.Center + new Vector2(Main.rand.Next(-124, 62), 7), DustID.Stone, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-7, -4)), 0, default, Main.rand.NextFloat(-1.2f, 2.2f));
                    Gore.NewGorePerfect(NPC.GetSource_FromThis(), NPC.Center + new Vector2(Main.rand.Next(-124, 62), 7), Vector2.Zero, GoreID.Smoke1);
                }
                if (player.velocity.Y == 0)
                {
                    player.velocity.Y -= 6;
                    player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0, false, false, -1);
                }
            }
            // y /= 1.02f;
            NPC.velocity.X = x;
            NPC.velocity.Y = y;
        }
        private void Bombed()
        {
            animationstate = 0;
            y = -8;
            NPC.ai[2]++;
            if (NPC.ai[2] >= 20)
            {
                animationstate = 2;
                SoundEngine.PlaySound(SoundID.Item37, NPC.Center);
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, -5 + Main.rand.Next(-3, 3)).RotatedByRandom(MathHelper.ToRadians(62)), ModContent.ProjectileType<SensitiveMine>(), NPC.damage / 3, 4, NPC.target);
                    }
                }
                   for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, -5 + Main.rand.Next(-3, 3)).RotatedByRandom(MathHelper.ToRadians(62)), ModContent.ProjectileType<SensitiveMine>(), NPC.damage / 3, 4, NPC.target);
                }

                NPC.ai[0] = 0;
                NPC.ai[2] = 0;
            }
        
       
            NPC.velocity.X = x;
            NPC.velocity.Y = y;
        }
        private void Dash()
        {
            animationstate = 1;
            y = y + 0.4f;
            if (y > 12f)
            {
                y = 12f;
            }
            NPC.ai[2]++;
            if (NPC.ai[2] >= 30)
            {
                y = -4;
                NPC.ai[2] = 0;
            }
            NPC.ai[3]++;
            if (NPC.ai[3] >= 130)
            {
                animationstate = 2;
                y = -2;
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    x = NPC.direction * 13;
                }
                else
                {
                    x = NPC.direction * 9;
                }
                
                NPC.ai[3] = 0;
                NPC.ai[0] = 0;
            }
                NPC.velocity.X = x;
            NPC.velocity.Y = y;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(faceTarget: true);
            switch (NPC.ai[0])
            {
                case 0:
                    {
                        Stomp();
                    }
                    break;
                    case 1:
                    {
                        Stomp2();
                    }
                    break;
                case 2:
                    {
                        Bombed();
                    }
                    break;
                case 3:
                    {
                        Dash();
                    }
                    break;
                case 4:
                    {
                        NPC.ai[0] = 0; 
                    }
                    break;

            }
        }
        private enum Frames
        {
            a,
            b,
            c
        }
        private enum ActionState
        {
            standstill,
            Jump,
            hopping
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            switch (animationstate)
            {
                case 0:
                    {
                        if (NPC.frameCounter < 5)
                        {
                            NPC.frame.Y = (int)Frames.a * frameHeight;
                        }
                        else
                        {
                            NPC.frameCounter = 0;
                        }
                    }
                    break;
                case 1:
                    {
                        if (NPC.frameCounter < 5)
                        {
                            NPC.frame.Y = (int)Frames.a * frameHeight;
                        }
                        else if (NPC.frameCounter < 10)
                        {
                            NPC.frame.Y = (int)Frames.b * frameHeight;
                        }
                        else
                        {
                            NPC.frameCounter = 0;
                        }
                    }
                    break;
                case 2:
                    {
                         if (NPC.frameCounter < 10)
                        {
                            NPC.frame.Y = (int)Frames.c * frameHeight;
                        }
                        else
                        {
                            NPC.frameCounter = 0;
                        }
                    }
                    break;
            }


        }
        public override void OnKill()
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(NPC.Center, DustID.Smoke, Main.rand.NextVector2Circular(2, 2) * 3, 0, default, 1);
                Dust.NewDustPerfect(NPC.Center, DustID.Torch, Main.rand.NextVector2Circular(1, 1) * 2, 0, default, 2);
                d.noGravity = true;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ActiveCoil>(), 1, 2, 4));
        }
    }
}
