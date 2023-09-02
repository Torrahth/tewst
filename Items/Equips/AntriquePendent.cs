using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System.Linq.Expressions;
using tmt.Common;
using System;

namespace tmt.Items.Equips
{
    public class AntriquePendent : ModItem
    {
        bool falling = false;
        int falldamage = 0;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 32;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 33, 0);
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.noFallDmg = true;
            player.maxFallSpeed *= 3;
            if (player.velocity.Y >= 16)
            {
                falling = true;
                player.velocity.Y *= 1.05f;

                falldamage = Convert.ToInt16(player.velocity.Y);
            }
            if (player.velocity.Y == 0 && falling == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust.NewDustPerfect(player.Bottom, DustID.Stone, Main.rand.NextVector2Circular(6, 6), 0, Scale: 2);
                }
              
           
                Terraria.Audio.SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, player.Center);
                Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(0.32f, 0.42f);
                falling = false;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<wwwa>(), 6 * (falldamage / 3), 2, player.whoAmI);
                falldamage = 0;
            }
        }
    }
    public class wwwa : ModProjectile
    {
        public override string Texture => "tmt/Common/Textures/Empty";
        public override void SetDefaults()
        {
            Projectile.width = 370;
            Projectile.height = 370;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 4;
        }

    }
}
