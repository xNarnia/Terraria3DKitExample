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
	/// A face consisting of three points in the shape of a triangle.
	/// </summary>
	public class FaceTriangle : IFace
	{
		public VertexPositionNormalTexture[] Composition { get; set; }
		public int VerticeCount { get; }
		public int PolygonCount { get; }
		public Texture2D Texture { get; set; }

		public FaceTriangle(
			Vector3 top,
			Vector3 bottomLeft,
			Vector3 bottomRight)
		{
			VerticeCount = 3;
			PolygonCount = 1;
			Composition = new VertexPositionNormalTexture[VerticeCount];

			var normal = ShapeHelpers.GetNormal(top, bottomLeft, bottomRight);

			Composition[0] = new VertexPositionNormalTexture(top, normal, new Vector2(0.5f, 0));
			Composition[1] = new VertexPositionNormalTexture(bottomLeft, normal, new Vector2(0, 1));
			Composition[2] = new VertexPositionNormalTexture(bottomRight, normal, new Vector2(1, 1));
		}
	}
}
