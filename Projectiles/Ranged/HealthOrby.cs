using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent;

namespace tmt.Projectiles.Ranged
{
    public class HealthOrby : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Heart);
            Item.width = 52;
            Item.height = 52;
        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, player.Center);
            player.Heal(10);
            Item.active = false;
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(244, 255, 120, 170);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(Item.position, new Vector3(0.444f, 0.455f, 0.320f));
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture = Mod.Assets.Request<Texture2D>("Common/Textures/FireflyLight").Value;
                var offset = new Vector2(Item.width / 2f, Item.height / 1.5f);

                Vector2 drawPos = (Item.position - Main.screenPosition) + offset;
                spriteBatch.Draw(texture, drawPos, null, lightColor, 0, texture.Size() / 2, Item.scale, SpriteEffects.None, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}
