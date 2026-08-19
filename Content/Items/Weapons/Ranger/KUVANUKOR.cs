using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Buffers.Text;
using Terraria;

namespace W.Content.Items.Weapons.Ranger
{
    public class KUVANUKOR : ModItem
    {
        public override void SetDefaults()
        {
            Item.useTime = 2;
            Item.useAnimation = 2;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.Size = new Vector2(102, 66);
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 425;
            Item.knockBack = 2f;
            Item.material = true;
            Item.ArmorPenetration = 100;

            Item.shoot = ProjectileID.HeatRay;
            Item.UseSound = SoundID.Item12 with { Pitch = 0.2f, Volume = 0.5f};
            Item.shootSpeed = 12f;
            
            Item.useStyle = ItemUseStyleID.Shoot;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentSolar, 15)
            .AddIngredient(ItemID.LunarBar, 10)
            .AddIngredient(ItemID.ToxicFlask, 3)
            .AddIngredient(ItemID.HeatRay, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(hit.Crit)
            {
                
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float muzzleOffset = 35f;

            Vector2 muzzleDirection = Vector2.Normalize(velocity);

            if (Collision.CanHit(position, 0, 0, position + muzzleDirection * muzzleOffset, 0, 0))
            {
                position += muzzleDirection * muzzleOffset;
            }
        }
        
    }
}