using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria3DKit
{
	public class ShapeHelpers
	{
		// This will convert the spherical coordinates to coordinates 
		// in space that we can use to plot points in the 3D world!

		// verticalSegments = Longitude = Elevation = Merdian 
		// horizontalSegments = Latitude = Polar = Parallel
		// Source: https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/
		// Spherical Coordinate System: https://mortennobel.files.wordpress.com/2010/10/spherical.jpg

		// World coords = Cartesian coords
		// Sphere coords = Spherical coords

		/// <summary>
		/// Refer to the ShapeHelpers class for more information.
		/// </summary>
		/// <returns></returns>
		public static Vector3 SphereCoordsToWorldCoords(float radius, double polarCoord, double elevationCoord)
		{
			// Remembering Trigonometry
			// Soh Cah Toa
			//   Sin(') = Opposite / Hypotenuse
			//   Cosine(') = Adjacent / Hypotenuse
			//   Tangent(') = Opposite / Adjacent

			var position = new Vector3();

			float adjacent = (float)(radius * Math.Cos(elevationCoord));
			position.X = (float)(adjacent * Math.Cos(polarCoord));
			position.Y = (float)(adjacent * Math.Sin(polarCoord));
			position.Z = (float)(radius * Math.Sin(elevationCoord));

			return position;
		}

		/// <summary>
		/// Returns the normal of the 3 supplied points.
		/// </summary>
		/// <param name="point1">The first Vector3 coordinate.</param>
		/// <param name="point2">The second Vector3 coordinate.</param>
		/// <param name="point3">The third Vector3 coordinate.</param>
		/// <returns></returns>
		public static Vector3 GetNormal(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			return 
				Vector3.Normalize(
					Vector3.Cross(point2 - point1, point3 - point1));
		}
	}
}
