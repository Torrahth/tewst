using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace tmt.Projectiles.Melee
{
    public class PlantRoots : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.frame = Main.rand.Next(1, 3);
            Projectile.width = 16;
            Projectile.height = 15;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.scale = Projectile.scale + Main.rand.NextFloat(-0.4f, 0.4f);
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Grass, Projectile.Center);
            Projectile.rotation = Main.rand.Next(-180, 180);
            Projectile.position += new Vector2(0, 4).RotatedBy(Projectile.rotation);
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.JunglePlants, Projectile.velocity.RotatedByRandom(Projectile.rotation + MathHelper.ToDegrees(14)) * 1.3f, 56);
            }
            Projectile.velocity = Vector2.Zero;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (Projectile.ai[0] == 1f) // or if(isStickingToTarget) since we made that helper method.
            {
                int npcIndex = (int)Projectile.ai[1];
                if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                {
                    if (Main.npc[npcIndex].behindTiles)
                    {
                        behindNPCsAndTiles.Add(index);
                    }
                    else
                    {
                        behindNPCs.Add(index);
                    }

                    return;
                }
            }
            // Since we aren't attached, add to this list
            behindProjectiles.Add(index);
        }
     
        public override void AI()
        {
            if (Projectile.timeLeft <= 50)
            {
                Projectile.scale -= 0.05f;
            }
            if (Projectile.scale <= 0)
            {
                Projectile.Kill();
            }

        }
    }
}
