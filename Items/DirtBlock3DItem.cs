using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria3DKitExample.Tiles;

namespace Terraria3DKitExample.Items
{
	/// <summary>
	/// This is a standard Mod Item.
	/// 
	/// This is used to place the ModTile that will render our 3D Dirt Cube.
	/// </summary>
	public class DirtBlock3DItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Terraria3DKit decorative dirt cube.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 2000;
			item.createTile = TileType<DirtBlockTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}