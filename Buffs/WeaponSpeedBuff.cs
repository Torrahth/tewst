using Terraria;
using Terraria.ModLoader;


namespace tm.Buffs
{
    public class WeaponSpeedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Melee Speed");
            // Description.SetDefault("6% Melee Speed Bonus.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.6f;
        }
    }
}
