using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria3DKit.Faces;
using Terraria3DKit.Shapes;

namespace Terraria3DKit.Broken
{
	/// <summary>
	/// A sphere shape consisting of square faces.
	/// </summary>
	public class ShapeUVSphere : Shape
	{
		/// <summary>
		/// The radius of the sphere.
		/// </summary>
		public float Radius { get; }

		/// <summary>
		/// Produces a sphere with the given radius.
		/// </summary>
		/// <param name="radius">The radius of the sphere in pixels.</param>
		/// <param name="horizontalSegments">Number of times to divide sphere horizontally.</param>
		/// <param name="verticalSegments">Number of times to divide sphere vertically.</param>
		public ShapeUVSphere(float radius, int horizontalSegments, int verticalSegments) : base()
		{
			Radius = radius;
			var vertices = GetSphereVectors(radius, horizontalSegments, verticalSegments);

			Vector3 point1;
			Vector3 point2;
			Vector3 point3;

			for (var j = 0; j < horizontalSegments - 1; j++)
			{
				for (var i = 0; i < verticalSegments - 1; i++)
				{
					point1 = vertices[i + (j * verticalSegments)];
					point2 = vertices[(i + 1) + (j * verticalSegments)];
					point3 = vertices[i + ((j + 1) * verticalSegments)];
					Faces.Add(new FaceTriangle(point1, point2, point3));

					point1 = vertices[(i + 1) + ((j + 1) * verticalSegments)];
					Faces.Add(new FaceTriangle(point1, point2, point3));
				}

				point1 = vertices[(verticalSegments - 1) + (j * verticalSegments)];
				point2 = vertices[j * verticalSegments];
				point3 = vertices[(verticalSegments - 1) + ((j + 1) * verticalSegments)];
				Faces.Add(new FaceTriangle(point1, point2, point3));

				point1 = vertices[(j + 1) * verticalSegments];
				Faces.Add(new FaceTriangle(point1, point2, point3));
			}

			// Top point of sphere
			point1 = new Vector3(0, 0, radius);
			for (var i = (verticalSegments - 1) * horizontalSegments; i < verticalSegments * horizontalSegments; i++)
			{
				point2 = vertices[i];

				if (i == (verticalSegments * horizontalSegments) - 1)
					point3 = vertices[(verticalSegments - 1) * horizontalSegments];
				else
					point3 = vertices[i + 1];

				Faces.Add(new FaceTriangle(point1, point2, point3));
			}

			// Bottom point of sphere
			point1 = new Vector3(0, 0, radius * -1);

			for (var i = 0; i < verticalSegments; i++)
			{
				point2 = vertices[i];

				if (i == verticalSegments - 1)
					point3 = vertices[0];
				else
					point3 = vertices[i + 1];

				Faces.Add(new FaceTriangle(point1, point2, point3));
			}

			Visible = true;
		}

		/// <summary>
		/// <para>Produces vertices for a UVSphere mesh.</para>
		/// Increasing the segments horizontally or vertically will increase the resolution of the mesh.
		/// </summary>
		/// <param name="radius">Radius of sphere to base mesh on</param>
		/// <param name="horizontalSegments">Number of times to divide sphere horizontally.</param>
		/// <param name="verticalSegments">Number of times to divide sphere vertically.</param>
		/// <returns></returns>
		public List<Vector3> GetSphereVectors(float radius, float horizontalSegments, float verticalSegments)
		{
			List<Vector3> coords = new List<Vector3>();

			for (var i = 1; i <= horizontalSegments; i++)
			{
				// Move down by (Math.PI / 2) to orient the semi-circle correctly
				var parallel = (Math.PI * i / (horizontalSegments + 1)) - Math.PI / 2;

				for (var j = 0; j < verticalSegments; j++)
				{
					var meridian = 2.0 * Math.PI * (j + 1) / verticalSegments;

					coords.Add(ShapeHelpers.SphereCoordsToWorldCoords(radius, meridian, parallel));
				}
			}

			var things = "";
			foreach(var co in coords)
			{
				things += $" = ({co.X}, {co.Y}, {co.Z})\n";
			}

			return coords;
		}
	}
}
