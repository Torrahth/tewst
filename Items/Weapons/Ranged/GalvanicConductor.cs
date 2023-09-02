using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tmt.Projectiles.Ranged;
using Terraria.Audio;
using tmt.Items.Misc;

namespace tmt.Items.Weapons.Ranged
{
    public class GalvanicConductor : ModItem
    {
        int i = 0;
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 54;
            Item.useTime = 25;
            Item.useAnimation = 42;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 12;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 30);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MimicArrow>();
            Item.shootSpeed = 12;
            Item.UseSound = SoundID.Item5;
            Item.useAmmo = AmmoID.Arrow;

            Item.rare = ItemRarityID.LightRed;

            Item.consumeAmmoOnFirstShotOnly = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                         .AddIngredient(ModContent.ItemType<ActiveCoil>(), 3)
               .AddRecipeGroup(nameof(ItemID.SilverBar), 6)
                 .AddRecipeGroup("IronBar", 6)
                 .AddTile(TileID.Anvils)
           .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (i >= 8)
            {
                SoundEngine.PlaySound(SoundID.Item11, player.Center);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SparkArrow>(), damage / 2, knockback, player.whoAmI);
                i = 0;
            }
            else
            {
                i++;
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(5)),type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
