using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using tm.Projectiles.Mage;

namespace tm.Items.Weapons.Mage
{
    public class GelBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Spawns Jellious Stars On the cursor\nIt has a slight hum");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 18;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.UseSound = new SoundStyle("tm/Common/Sounds/Rattle") with
            {
                Volume = 0.4f,
                PitchVariance = 0.3f,
            };
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<JellyStar>();
            Item.shootSpeed = 1;
            Item.mana = 6;

            Item.rare = ItemRarityID.LightRed;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 7; i++)
            {
                position = Main.MouseWorld + Main.rand.NextVector2Circular(Main.rand.Next(-42, 42), Main.rand.Next(-42, 42));
                Dust d = Dust.NewDustPerfect(position, DustID.RainbowTorch, Main.rand.NextVector2Circular(2, 2) * 3, 0, default, 1);
                d.noGravity = true;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Gel, 10)
              .AddIngredient(ItemID.FallenStar)
            .AddIngredient(ItemID.DemoniteBar, 6)
                .AddIngredient(ItemID.ShadowScale, 12)

            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
              .AddIngredient(ItemID.AntlionMandible)
                .AddIngredient(ItemID.FallenStar)
              .AddIngredient(ItemID.CrimtaneBar, 6)
                  .AddIngredient(ItemID.TissueSample, 12)
              .AddTile(TileID.Anvils)
              .Register();
        }
    }
}
