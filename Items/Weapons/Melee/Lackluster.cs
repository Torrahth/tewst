using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using tm.Projectiles.Melee;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace tm.Items.Weapons.Melee
{
    public class Lackluster : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Striking a Enemy while above 50% Health awards a 6% Melee Bonus\nJabs at enemys... Thats it.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 38; // post eow
            Item.height = 40;
            Item.useTime = 52;
            Item.knockBack = 4;
            Item.useAnimation = 52;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 21;
            Item.crit = 3;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.value = Item.sellPrice(0, 0, 30);
            Item.UseSound = new SoundStyle("tm/Common/Sounds/Jab") with
            {
                Volume = 0.7f,
                PitchVariance = 0.2f,
            };
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<LacklusterP>();
            Item.shootSpeed = 1f;//6;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1.5f;
        }
        public override bool? UseItem(Player player)
        {
            return null;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.DemoniteBar, 6)
                 .AddIngredient(ItemID.Amber, 4)
            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
              .AddIngredient(ItemID.AntlionMandible)
              .AddIngredient(ItemID.CrimtaneBar, 6)
                   .AddIngredient(ItemID.Amber, 4)
              .AddTile(TileID.Anvils)
              .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
