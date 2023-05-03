using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace tm.Items.Weapons.Mage
{
    public class AncientFIzzer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Swing to Blow Dangerious Bubbles to harm your Foe");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 20;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 11;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bubble;
            Item.shootSpeed = 12;

            Item.rare = ItemRarityID.Blue;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
               var newvelocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
                newvelocity *= 1 + Main.rand.NextFloat(-0.3f, 0.3f);
                Projectile.NewProjectile(source, position, newvelocity, ModContent.ProjectileType<Projectiles.FizzerBubble>(), damage, knockback, player.whoAmI);
            }
            for (int i = 0; i < 6; i++)
            {
                var newvelocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
                newvelocity *= 1 + Main.rand.NextFloat(-0.3f, 0.3f);
                Projectile.NewProjectile(source, position, newvelocity, ModContent.ProjectileType<Projectiles.TinyBubble>(), 0, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                  .AddIngredient(ItemID.Gel, 8)
                     .AddIngredient(ItemID.FallenStar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
