using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using ReLogic.Graphics;
using System.Net;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria3DKitExample.Items;
using Terraria3DKit.Shapes;
using System.Collections.Generic;

namespace Terraria3DKitExample.Tiles
{
	// This class provides an example on how to draw a 3D model!
	// Simply refer to the methods at the end. 
	// This runs at a more consistent FPS until other bugs are fixed.
	// Everything else can be found under tModLoader examples.
	public class DirtBlockTile : ModTile
	{
		// Create a static model that we can load to from the Mod class.
		/// <summary>
		/// A 3D model of a Dirt Cube.
		/// </summary>
		public static Shape DirtBlock { get; set; }
		public static Dictionary<Tile, TileIndex> TilesToRenderOn;
	
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileSolid[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);

			// Place anywhere
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[] { 124 };
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorRight = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorTop = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Example 3D Dirt Block");
			AddMapEntry(new Color(150, 150, 250), name);
			dustType = mod.DustType("Sparkle");
			disableSmartCursor = true;
			adjTiles = new int[] { Type };

			TilesToRenderOn = TilesToRenderOn ?? new Dictionary<Tile, TileIndex>();
		}

		// This will add it to the list of tiles to draw the 3D dirt cube on
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			if (tile.frameX % 32 == 0 && tile.frameY % 32 == 0) // t.frameX % 54 == 0
			{
				if (!TilesToRenderOn.ContainsKey(tile))
				{
					TilesToRenderOn[tile] = new TileIndex(i, j);
				}
			}

			base.PostDraw(i, j, spriteBatch);
		}

		// When destroyed, remove it from the list of tiles to draw the 3D dirt cube on
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Tile tile = Main.tile[i, j];

			TilesToRenderOn.Remove(tile);
			Item.NewItem(i * 16, j * 16, 64, 32, ItemType<DirtBlock3DItem>());
		}
	}

	// Used to track the tiles that need to render the 3D Dirt Cube
	public class TileIndex
	{
		public int i;
		public int j;

		public TileIndex(int _i, int _j)
		{
			i = _i;
			j = _j;
		}
	}
}