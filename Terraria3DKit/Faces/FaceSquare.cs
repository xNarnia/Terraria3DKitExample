using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria3DKit.Faces
{
	/// <summary>
	/// <para>A face consisting of 6 points in the shape of a square.</para>
	/// This face uses two triangles combined together to form a square.
	/// </summary>
	public class FaceSquare : IFace
	{
		public VertexPositionNormalTexture[] Composition { get; set; }
		public int VerticeCount { get; }
		public int PolygonCount { get; }
		public Texture2D Texture { get; set; }

		public FaceSquare(
			Vector3 topLeft, 
			Vector3 topRight, 
			Vector3 bottomLeft, 
			Vector3 bottomRight)
		{
			VerticeCount = 6;
			PolygonCount = 2;
			Composition = new VertexPositionNormalTexture[VerticeCount];

			var normal = ShapeHelpers.GetNormal(topLeft, topRight, bottomLeft);

			// Squares are made of two triangles. This makes it easier to process
			Composition[0] = new VertexPositionNormalTexture(topLeft, normal, new Vector2(0, 0));
			Composition[1] = new VertexPositionNormalTexture(topRight, normal, new Vector2(1, 0));
			Composition[2] = new VertexPositionNormalTexture(bottomLeft, normal, new Vector2(0, 1));

			normal = ShapeHelpers.GetNormal(bottomLeft, topRight, bottomRight);

			Composition[3] = new VertexPositionNormalTexture(bottomLeft, normal, new Vector2(0, 1));
			Composition[4] = new VertexPositionNormalTexture(topRight, normal, new Vector2(1, 0));
			Composition[5] = new VertexPositionNormalTexture(bottomRight, normal, new Vector2(1, 1));
		}
	}
}
