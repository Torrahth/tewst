using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using tm.Projectiles.Melee;
using Terraria.GameContent.Creative;

namespace tm.Items.Tools
{
    public class MagicPickaxe : ModItem
    {
        int yea = 11;
        int timer = 0;
        public override void SetStaticDefaults()
        {
             Tooltip.SetDefault("Right Click to consume mana for faster pickaxe speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = yea;
            Item.useAnimation = yea + 2;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 8;
            Item.crit = 2;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1.2f;
            Item.pick = 45;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddIngredient(ItemID.GoldPickaxe) 
              .AddIngredient(ItemID.RedHusk) // may it have some magic properties... or not 
                          .AddIngredient(ItemID.FallenStar)
                   .AddIngredient(ItemID.Silk, 5)
              .AddTile(TileID.WorkBenches)
              .Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            Item.useTime = yea;
            Item.useAnimation = yea;
            if (timer <= 0)
            {
                yea = 11;
            }
            else
            {
                if (Main.rand.NextBool(6))
                {
                    var r = Dust.NewDustPerfect(player.Center, DustID.Torch, Main.rand.NextVector2CircularEdge(2, 4) * 2, 12, Scale: 0.6f);
                    player.statMana -= 5; // if your unlucky your mana will drain super fast, shit game design but idc
                }
                timer -= 1;
                yea = 6;

                if (player.statMana <= 0)
                {
                    timer = 0; // sucks to suck ran out of mana
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.statMana > 0 && timer <= 0)
                {
                    timer += 900;
                    for (int i = 0; i < 10; i++) {
                       var d = Dust.NewDustPerfect(player.Center, DustID.Torch, Main.rand.NextVector2CircularEdge(4, 4) * 4, 12, Scale: 2);
                        d.noGravity = true;
                    }
                }
            }
            return base.CanUseItem(player);
        }
    }
}
