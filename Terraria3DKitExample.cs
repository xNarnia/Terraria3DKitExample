using Terraria.ModLoader;
using Terraria3DKitExample.Tiles;

namespace Terraria3DKitExample
{
	/// <summary>
	/// Hello and welcome to the Terraria3DKitExample!                      <para />
	/// 
	/// NOTE: This is library is in early development stages and 
	/// could suffer from performance issues if not used responsibly.		<para />
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
		public override void PostSetupContent()
		{
			base.PostSetupContent();

			var pylon = new PylonModel();
			pylon.LoadContent(this);

			PylonTile.PylonModel = pylon;
		}
	}
}