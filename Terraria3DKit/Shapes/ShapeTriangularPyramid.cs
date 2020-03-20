using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria3DKit.Faces;

namespace Terraria3DKit.Shapes
{
	/// <summary>
	/// A pyramid shape consisting of 4 faces with the base of a triangle.
	/// </summary>
	public class ShapeTriangularPyramid : Shape
	{
		/// <summary>
		/// The length of each side of the base of the pyramid in pixels.
		/// </summary>
		public float SideLength { get; }

		/// <summary>
		/// The size of the pyramid on the Z-axis in pixels.
		/// </summary>
		public float Height { get; }

		public IFace FaceFrontLeft { get; }
		public IFace FaceFrontRight { get; }
		public IFace FaceBack { get; }
		public IFace FaceBottom { get; }

		/// <summary>
		/// Produces a triangular pyramid with 4 faces.
		/// </summary>
		/// <param name="sideLength">The length the edges of the triangles should be in pixels.</param>
		/// <param name="height">The height of the triangle in pixels.</param>
		public ShapeTriangularPyramid(float sideLength, float height)
		{
			SideLength = sideLength;
			Height = height;

			// Front is towards you
			// Back is away from you
			Vector3 front = new Vector3(
					0,
					sideLength / 2,
					height / 2 * -1
			);
			Vector3 backLeft = new Vector3(
					sideLength / 2,
					sideLength / 2 * -1,
					height / 2 * -1
			);
			Vector3 backRight = new Vector3(
					sideLength / 2 * -1,
					sideLength / 2 * -1,
					height / 2 * -1
			); 

			Vector3 top = new Vector3(
					0,
					0,
					height / 2
			);

			FaceFrontLeft = new FaceTriangle(
					top,
					backLeft,
					front
				);

			FaceFrontRight = new FaceTriangle(
					top,
					front,
					backRight
				);

			FaceBack = new FaceTriangle(
					top,
					backLeft,
					backRight
				);

			FaceBottom = new FaceTriangle(
					front,
					backLeft,
					backRight
				);

			Faces.Add(FaceFrontLeft);
			Faces.Add(FaceFrontRight);
			Faces.Add(FaceBack);
			Faces.Add(FaceBottom);
		}
	}
}
