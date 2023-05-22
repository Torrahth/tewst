using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Input;
using tm.Projectiles.Mage;

namespace tm.Items.Weapons.Mage
{
    public class DantesInferno : ModItem
    {
        public override void SetStaticDefaults()
        {
             Tooltip.SetDefault("Summons the spawn of hell to clap your foe\nThere were only 9 Triangles of hell");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.reuseDelay = 20;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 43;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 30);
            Item.UseSound = SoundID.DD2_EtherianPortalOpen;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Fireball; // so true
            Item.shootSpeed = 12;
            Item.mana = 9;

            Item.rare = ItemRarityID.LightRed;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var newvelocity = Main.rand.Next(140, 240) ;//velocity.RotatedByRandom(MathHelper.ToRadians(180));
                Projectile.NewProjectile(source, Main.MouseWorld - new Vector2(0, 200).RotatedBy( MathHelper.ToRadians(MathHelper.TwoPi - newvelocity)), new Vector2(0, -2).RotatedBy(-MathHelper.ToRadians(180 + newvelocity) )  , ModContent.ProjectileType<GloryHand>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, Main.MouseWorld - new Vector2(0, -200).RotatedBy(-MathHelper.TwoPi - MathHelper.ToRadians(MathHelper.TwoPi - newvelocity)), new Vector2(0, 2).RotatedBy(MathHelper.ToRadians(180 + newvelocity) ) , ModContent.ProjectileType<GloryHand>(), damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BookofSkulls)
                  .AddIngredient(ItemID.SoulofNight, 6)
                     .AddIngredient(ItemID.SoulofMight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
