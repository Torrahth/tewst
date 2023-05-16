using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using tm.Projectiles.Melee;

namespace tm.Items.Weapons.Melee
{
    public class PutridMushrump : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Plant growing roots into your foe");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 21;
            Item.knockBack = 1;
            Item.useAnimation = 21;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 23;
            Item.crit = 2;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1.2f;
            Item.shoot = ModContent.ProjectileType<PutridMushrumpP>();
            Item.noUseGraphic = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.MountedCenter;
        }
        public override void HoldItem(Player player)
        {
            player.jumpSpeedBoost *= 0.4f;
            player.accRunSpeed *= 0.4f;
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
