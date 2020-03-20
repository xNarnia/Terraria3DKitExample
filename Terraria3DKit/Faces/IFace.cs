using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria3DKit.Faces
{
	/// <summary>
	/// The base interface for all faces.
	/// </summary>
	public interface IFace
	{
		VertexPositionNormalTexture[] Composition { get; set; }
		int VerticeCount { get; }
		int PolygonCount { get; }
		Texture2D Texture { get; set; }
	}
}
