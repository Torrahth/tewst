using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace tm.Common
{
    public class TmScreenshake : ModPlayer
    {
        //Main.LocalPlayer.GetModPlayer<TmScreenshake>().ShakeScreen(6, 0.75f);
        float screenShakeStrength;
        float screenShakeDissolve;
        public void ShakeScreen(float strenght, float desolve = 0.95f)
        {
            screenShakeStrength = strenght;
            screenShakeDissolve = Math.Clamp(desolve, 0, 0.9999f);
        }
        public override void ModifyScreenPosition()
        {
            if (screenShakeStrength > 0.001f)
            {
                Main.screenPosition += (new Vector2(Main.rand.NextFloat(-screenShakeStrength, screenShakeStrength), Main.rand.NextFloat(-screenShakeStrength, screenShakeStrength)) * 0.5f);
                screenShakeStrength *= screenShakeDissolve;
            }
        }
    }
}
