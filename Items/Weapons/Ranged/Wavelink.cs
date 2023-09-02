using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using tmt.Projectiles.Ranged;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using tmt.Tiles;
using tmt.Items.Misc;

namespace tmt.Items.Weapons.Ranged
{
    public class Wavelink : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 92;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 29;
            Item.crit = 2;
            Item.noMelee = true;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 20);
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
                         .AddIngredient(ModContent.ItemType<GalvanicConductor>())
                         .AddRecipeGroup(nameof(ItemID.CobaltBar), 12)
                 .AddIngredient(ModContent.ItemType<BottleOfSparks>(), 5)
                 .AddTile(TileID.MythrilAnvil)
           .Register();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<MimicArrow>();
            }
        }
    }
}
