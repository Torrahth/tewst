using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using tmt.Projectiles.Melee;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace tmt.Items.Weapons.Melee
{ 
    public class VeinSpine : ModItem
    {
        int combo = 1;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Swing around a spear that when piecing causes a healing effect\nThe roots of a Great Rotting Tree");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 92;
            Item.useTime = 21;
            Item.knockBack = 1;
            Item.useAnimation = 21;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 77;
            Item.crit = 2;
            Item.useStyle = ItemUseStyleID.HiddenAnimation;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<VeinStarP>();
            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1.2f;
            Item.noMelee = true; // misinformation
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (combo == 1)
            {
                combo++;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -1);
            }
            else if (combo == 2)
            {
                combo--;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1);
            }
            else
            {
                combo = 1;
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteGlaive)
                .AddIngredient(ItemID.ChlorophyteBar, 10)
                                .AddIngredient(ItemID.DemoniteBar, 10)
                     .AddIngredient(ItemID.SoulofSight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
              .AddIngredient(ItemID.TitaniumTrident)
              .AddIngredient(ItemID.ChlorophyteBar, 10)
                              .AddIngredient(ItemID.DemoniteBar, 10)
                   .AddIngredient(ItemID.SoulofSight, 15)
              .AddTile(TileID.WorkBenches)
              .Register();

        }
    }

    public class VeinStarP : ModProjectile
    {
        int g = Main.rand.Next(1, 3);
        int STATE = 1;
        int range = 24;
        float spin;
         public float t;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 324;
            Projectile.height = 324;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.numHits = 4;
            Projectile.localNPCHitCooldown = 10;
        }
        private Vector2 mouse = new Vector2();
        public override void OnSpawn(IEntitySource source)
        {
            spin = Projectile.ai[0];

            Projectile.ai[0] = 0;
        }
        public override void AI()
        {
            /// ewwww effects who cares
            /// 
            //new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, 333, 333);
        

            var d = Dust.NewDustPerfect(Projectile.Center, DustID.CursedTorch, Main.rand.NextVector2Circular(3, 5).RotatedBy(Projectile.rotation), 0);

            d.noGravity = true;

            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 2;
            player.itemTime = 2;
            if (STATE == 1 && Projectile.ai[1] <= 0.2f)
            {
                Projectile.scale += 0.2f;
                t = spin > 0 ? -0.4f : 0.4f;
                Projectile.ai[1] += 0.2f;

                mouse = player.Center.DirectionTo(Main.MouseWorld);
                Projectile.rotation = mouse.ToRotation() + 0.785398f;
            }
            else
            {
               
                if (range >= 24)
                {
                    t /= 1.15f;
                }
             

               Projectile.ai[0] += t;
                STATE = 2;
                if (range <= 180 && Projectile.ai[1] != 0.7f)
                {
                    Projectile.ai[1] = 0.7f;
                
                    range += 10;
                }
               if (Projectile.ai[1] == 0.7f && (t <= 0.1f && t >= -0.1f))
                {
                    Projectile.scale -= 0.04f;
                    range -= 3;
                }
                else
                {
                    Projectile.scale += 0.02f;
                    range += 10;
                }
                if ( (t <= 0.02f && t >= -0.02f))
                {
                    Projectile.Kill();
              
                }
            }
        
            Projectile.rotation = mouse.ToRotation() + 0.785398f + Projectile.ai[0];
            Projectile.Center = player.Center + ((range * Projectile.scale) * (Projectile.rotation - 0.785398f).ToRotationVector2()) ;
          //  Projectile.position.X -= Projectile.width / 2;
            //Projectile.position.Y -= Projectile.width / 2;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - 2.35619f);
  
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, Projectile.Center);
            if (Main.rand.NextBool(5))
            {
                player.Heal(1);
            }
         
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDraw(ref Color lightColor)
        {


            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Mod.Assets.Request<Texture2D>("Common/Textures/MeleeArc2").Value;// TextureAssets.Projectile[Projectile.type].Value;
            var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);

            Texture2D texture2 = TextureAssets.Projectile[Projectile.type].Value;

            int frameHeight = texture2.Height / Main.projFrames[Projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, texture2.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
          
               
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale + 0.6f;
                Color color = new Color(153 / Projectile.oldPos.Length * 6, 255 / Projectile.oldPos.Length * 5, 102 / Projectile.oldPos.Length * 6, 77) * -(1f + -(Math.Abs(t) * 3 * MathHelper.Pi)) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
               Main.spriteBatch.Draw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2, sizec, SpriteEffects.None, 0);
             
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.spriteBatch.Draw(texture2, (Projectile.Center - Main.screenPosition  + new Vector2(0f, Projectile.gfxOffY ) * -5), sourceRectangle, Color.White * (1 - Projectile.alpha), Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}
