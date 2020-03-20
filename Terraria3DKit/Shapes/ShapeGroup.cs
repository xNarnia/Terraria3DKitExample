using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria3DKit.Shapes
{
	/// <summary>
	/// A shape consisting of other shapes.
	/// </summary>
	public class ShapeGroup : Shape
	{
		public void AddShape(Shape shape, Vector3? offset = null)
		{
			foreach(var face in shape.Faces)
			{
				face.Texture = face.Texture ?? shape.DefaultTexture;
				if(offset != null)
				{
					for(var i = 0; i < face.VerticeCount; i++)
					{
						face.Composition[i].Position += offset.Value;
					}
				}
				Faces.Add(face);
			}
		}
	}
}
