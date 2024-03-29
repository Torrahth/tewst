﻿using Terraria;
using Terraria.ModLoader;

namespace tmt.Projectiles.Melee
{
    public class Hitbox : ModProjectile
    {
        public override string Texture => "tmt/Common/Textures/Empty";

        public override void SetDefaults()
        {
          Projectile.width = 152;
          Projectile.height = 152;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.alpha = 40;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 2;
            Projectile.scale = 1f;
         Projectile.localNPCHitCooldown = 10;
        }
    }
}
