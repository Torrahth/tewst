using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;
using tm.Common;

namespace tm.Projectiles
{
    public class BloodSplatter : ModProjectile
    {
        public override string Texture => "tm/Common/Textures/Empty"; 
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.3f, 0.1f);
            SoundEngine.PlaySound(new SoundStyle("tm/Common/Sounds/SpearSlice") with
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
            });

        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 32; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Blood, Main.rand.NextVector2Circular(2, 4), 12, default, Main.rand.NextFloat(0.4f, 2f));
            }
        }
    }
}
