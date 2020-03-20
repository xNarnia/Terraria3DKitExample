using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria3DKit.Faces;

namespace Terraria3DKit.Shapes
{
	/// <summary>
	/// The base interface for all shapes. 
	/// </summary>
	public interface IShape
	{
		List<IFace> Faces { get; set; }
		PrimitiveType PrimitiveType { get; }

		Texture2D DefaultTexture { get; set; }
		Vector2 Position { get; set; }
		float RotationX { get; set; }
		float RotationY { get; set; }
		float RotationZ { get; set; }
		bool LightingEnabled { get; set; }

		void Draw(float x, float y);
		void Draw(Vector2 position);
		void ResetCameraOrientation();
		void RotateVerticesAboutOrigin(Vector3 rotationVectorDegrees);
	}
}
