using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria3DKit.Shapes;

namespace Terraria3DKitExample.Tiles
{
	/// <summary>
	/// This is a PylonModel!
	/// 
	/// What you see here are textures being loaded, shapes being created,
	/// and then all put together into what is called a ShapeGroup.
	/// 
	/// A ShapeGroup is simply a shape that contains other shapes!
	/// Once you have all your shapes packaged into one group, the 
	/// class will orient all shapes together as one shape!
	/// 
	/// Rotation, translation, and draw will all act as if this is a singular 
	/// shape. 
	/// </summary>
	public class PylonModel : ShapeGroup
	{
		public static Texture2D Crystal;
		public static Texture2D Base;

		public void LoadContent(Mod mod)
		{
			// Load the textures we will be using for the shapes
			Crystal = mod.GetTexture("Tiles/PylonTileTextures/crystal");
			Base = mod.GetTexture("Tiles/PylonTileTextures/base");

			// Define the crystal shape and add it to the group
			var pylonCrystal = new ShapeDiamond(24, 24, 48);
			pylonCrystal.DefaultTexture = Crystal;
			AddShape(pylonCrystal);

			// Define the rotating base of the pylon
			var pylonBase = new ShapeSquarePyramid(32, 32, 40);
			pylonBase.DefaultTexture = Base;

			// We rotate the mesh 180 degrees before adding it to the group
			// This will help when we rotate the pylon so that the base will
			// rotate upside down!
			pylonBase.RotateVerticesAboutOrigin(new Vector3(0, 180, 0));
			AddShape(pylonBase, new Vector3(0, 0, -26));

			LightingEnabled = true;
		}
	}
}
