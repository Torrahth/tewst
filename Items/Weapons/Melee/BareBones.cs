using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using tm.Projectiles.Melee;

namespace tm.Items.Weapons.Melee
{
    public class BareBones : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.noMelee = true;
            Item.rare = 3;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;

            Item.shoot = ModContent.ProjectileType<BareBonesFlail>();
            Item.shootSpeed = 7;
        }
    }
}
