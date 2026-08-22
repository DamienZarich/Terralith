using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Buffers.Text;
using Terraria;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace W.Content.Items.Weapons.Ranger
{
public class Lazr : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
    }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.channel || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }
            Projectile.Center = player.Center;
            Vector2 aimDir = Vector2.Normalize(Main.MouseWorld - player.Center);
            Projectile.velocity = aimDir;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)System.Math.Atan2(aimDir.Y * player.direction, aimDir.X * player.direction);

            Vector2 startPos = Projectile.Center;
            Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.Zero);
            float maxDistance = 800f;
            float currentDistance = 0f;

            while (currentDistance < maxDistance)
            {
                Vector2 checkPos = startPos + direction * currentDistance;

                int tileX = (int)(checkPos.X / 16f);
                int tileY = (int)(checkPos.Y / 16f);

                Tile tile = Main.tile[tileX, tileY];
                if (tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType])
                {
                    break;
                }
                currentDistance += 16f;
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
{
    float laserLength = Projectile.localAI[0];
    Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.Zero);
    Vector2 startPos = Projectile.Center;
    Vector2 endPos = startPos + Projectile.velocity * laserLength;

    float point = 0f;
    return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), startPos, endPos, 16f, ref point);
}
public override bool PreDraw(ref Color lightColor)
{
    Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
    Rectangle headFrame = new Rectangle(0, 0, 16, 16);
    Rectangle bodyFrame = new Rectangle(0, 16, 16, 16);
    Rectangle tailFrame = new Rectangle(0, 32, 16, 16);

    Vector2 unit = Projectile.velocity.SafeNormalize(Vector2.Zero);
    float laserLength = 800f;
    float rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
    Vector2 origin = new Vector2(8, 8);
    Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, headFrame, Color.White, rotation, origin, Projectile.scale, SpriteEffects.None, 0);
    for (float i = 16; i < laserLength - 16; i += 16)
        {
            Vector2 drawPos = Projectile.Center + unit * i;
            Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition, bodyFrame, Color.White, rotation, origin, Projectile.scale, SpriteEffects.None, 0);
        }
         Vector2 tailPos = Projectile.Center + unit * laserLength;
            Main.EntitySpriteDraw(texture, tailPos - Main.screenPosition, tailFrame, Color.White, rotation, origin, Projectile.scale, SpriteEffects.None, 0);

            return false;

}
}
}