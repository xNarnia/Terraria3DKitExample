using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria3DKit.Faces
{
	public abstract class Face : IFace
	{
		public VertexPositionNormalTexture[] Composition { get; set; }
		public int VerticeCount { get; }
		public int PolygonCount { get; }
		public Texture2D Texture { get; set; }

		public Face()
		{
			VerticeCount = 0;
			PolygonCount = 0;
			Composition = new VertexPositionNormalTexture[VerticeCount];
		}
	}
}
