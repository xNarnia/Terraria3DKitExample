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

namespace Terraria3DKitExample.Tiles
{
	// This class provides an example on how to draw a 3D model!
	// Simply refer to the SpecialDrawing method. 
	// Everything else can be found under tModLoader examples.
	public class PylonTile : ModTile
	{
		// Create a static model that we can load to from the Mod class.
		/// <summary>
		/// A 3D model of a Pylon.
		/// </summary>
		public static Shape PylonModel { get; set; }

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

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
			name.SetDefault("Example 3D Pylon");
			AddMapEntry(new Color(150, 150, 250), name);
			dustType = mod.DustType("Sparkle");
			disableSmartCursor = true;
			adjTiles = new int[] { Type };
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 64, 32, ItemType<Pylon3DItem>());
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			base.DrawEffects(i, j, spriteBatch, ref drawColor, ref nextSpecialDrawIndex);

			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		// SpecialDraw allows us to draw over the tile! 
		// We can hide the tile underneath by covering it with the 3D model.
		//
		// If you want to draw the 3D model behind the player, just use 
		// PostDraw in the same way.
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile t = Main.tile[i, j];

			// This is to ensure it only runs on the first frame.
			// Otherwise this runs for each frame!
			if (t.frameX % 32 == 0 && t.frameY % 32 == 0) 
			{
				// Let's rotate it 1 degree each draw.
				PylonModel.RotationZ += 1;

				if (PylonModel.RotationZ == 360)
					PylonModel.RotationZ = 0;

				// Draw the PylonModel using World Coordinates!
				PylonModel.Draw(new Vector2(i, j).ToWorldCoordinates(16, 0));
			}

			base.SpecialDraw(i, j, spriteBatch);
		}
	}
}