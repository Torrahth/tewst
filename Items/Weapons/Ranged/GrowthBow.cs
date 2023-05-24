using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using tm.Projectiles.Ranged;

namespace tm.Items.Weapons.Ranged
{
    public class GrowthBow : ModItem
    {
        float a = -6 ;
        public override void SetStaticDefaults()
        {
             Tooltip.SetDefault("When crystals reach the point of the mouse when spawned they may explode into many groups, However bring the mouse farther or being a enemy it will not split");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.useTime = 3;
            Item.useAnimation = 8;
            Item.reuseDelay = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 29;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<StrangeCrystak>();
            Item.shootSpeed = 12;
            Item.ammo = AmmoID.Arrow;

            Item.rare = ItemRarityID.LightRed;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Item.useAnimation <= 8)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, player.Center);
            }
                a += 3f;
            velocity = velocity.RotatedBy(MathHelper.ToRadians(a));
            if (a >= 6)
            {
                a = -6;
            }
        }
    }
}
