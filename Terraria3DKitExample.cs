using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria3DKit.Shapes;
using Terraria3DKitExample.Tiles;

namespace Terraria3DKitExample
{
	/// <summary>
	/// Hello and welcome to the Terraria3DKitExample!                      <para />
	/// 
	/// NOTE: This is library is in early development stages and 
	/// could suffer from performance issues if not used responsibly
	/// as well as serious bugs.											<para />
	/// 
	/// Here, you will find examples of how to create 3D shapes,
	/// how to draw them onto the screen, and more!                         <para />
	/// 
	/// You can find the source code for this library here.
	/// https://github.com/xPanini/Terraria3DKitExample                     <para />
	/// 
	/// Creating Shapes:
	///   Refer to Tiles/PylonModel.cs and Tiles/PylonTileTextures          <para />
	///   
	/// Drawing Shapes:
	///   Refer to Tiles/PylonTile.cs                                       <para />
	///   
	/// UV Mapping:
	///   Triangle Faces:
	///      Refer to the Tiles/PylonTileTextures/crystal.png
	///      texture for how textures handle textures assigned to them      <para />
	///   
	///   Square Faces:
	///      The top left of any square face will refer to the top left
	///      of any texture													<para />
	///      
	///   Custom Faces:
	///      You can create your own custom face if you want to make
	///      your own UV mapping and faces to use in your shapes.
	///          You can refer to Terraria3DKit/Faces/FaceTriangle.cs		<para />
	///          
	/// For other support, please visit the Panini Mods Discord:
	///    https://discord.gg/nJ5vPeA										<para />
	/// </summary>
	public class Terraria3DKitExample : Mod
	{
		// The TileTracker class can be found just below this!
		// It's just used to keep track of the tile coordinates.
		// You can implement this however you want, this is just
		// one example.

		public override void PostSetupContent()
		{
			base.PostSetupContent();

			// This pylon will be rendered at an inconsistent FPS using SpecialDraw 
			// (slows down when zoomed in)
			var pylon = new PylonModel();
			pylon.LoadContent(this);
			PylonTile.PylonModel = pylon;

			// This dirt cube will be rendered at a consistent FPS using ModifyInterfaceLayers
			var dirtCube = new ShapeCuboid(32, 32, 32);
			dirtCube.RotationY = -15;
			dirtCube.DefaultTexture = GetTexture("Tiles/DirtBlockTileTextures/Dirt");
			dirtCube.FaceTop.Texture = GetTexture("Tiles/DirtBlockTileTextures/DirtTop");
			dirtCube.FaceBottom.Texture = GetTexture("Tiles/DirtBlockTileTextures/DirtBottom");
			DirtBlockTile.DirtBlock = dirtCube;
		}

		// We will rotate the block at consistent FPS using PostUpdateEverything
		public override void PostUpdateEverything()
		{
			base.PostUpdateEverything();
			DirtBlockTile.DirtBlock.RotationZ += 1;
		}

		// We will draw the block at a consistent FPS using ModifyInterfaceLayers
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"Terraria3DKit: Models",
					delegate
					{
						foreach(var tile in DirtBlockTile.TilesToRenderOn)
						{
							DirtBlockTile.DirtBlock.ModifyInterfaceLayerFix = true;
							DirtBlockTile.DirtBlock.Draw(new Vector2(tile.Value.i, tile.Value.j).ToWorldCoordinates(16, 16));
						}
						return true;
					},
					InterfaceScaleType.Game)
				);
			}
		}
	}

}