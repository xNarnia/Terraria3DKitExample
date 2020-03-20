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
	/// A diamond shape consisting of 8 faces with a square center.
	/// </summary>
	public class ShapeDiamond : Shape
	{
		/// <summary>
		/// The size of the diamond on the X-axis in pixels.
		/// </summary>
		public float Length { get; }

		/// <summary>
		/// The size of the diamond on the Y-axis in pixels.
		/// </summary>
		public float Width { get; }

		/// <summary>
		/// The size of the cube on the Z-axis from the bottom point to the top point in pixels.
		/// </summary>
		public float Height { get; }

		public IFace FaceTopFront { get; }
		public IFace FaceTopBack { get; }
		public IFace FaceTopLeft { get; }
		public IFace FaceTopRight { get; }
		public IFace FaceBottomFront { get; }
		public IFace FaceBottomBack { get; }
		public IFace FaceBottomLeft { get; }
		public IFace FaceBottomRight { get; }

		/// <summary>
		/// Produces a diamond with 8 sides, two square pyramids. One on top, one on the bottom.
		/// </summary>
		/// <param name="length">The length (x) of the diamonds's center in pixels.</param>
		/// <param name="width">The width (y) of the diamonds's center in pixels.</param>
		/// <param name="height">The height (z) of the total diamonds in pixels.</param>
		public ShapeDiamond(float length, float width, float height)
		{
			Height = height;
			Length = length;
			Width = width;

			Vector3 centerTop =
				new Vector3(
					0,
					0,
					height / 2);

			Vector3 centerBottom =
				new Vector3(
					0,
					0,
					height / 2 * -1);

			FaceTopFront = new FaceTriangle(
				centerTop,
				new Vector3(
					length / 2 * -1,
					width / 2,
					0),
				new Vector3(
					length / 2,
					width / 2,
					0)
			);

			FaceTopBack = new FaceTriangle(
				centerTop,
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					0),
				new Vector3(
					length / 2,
					width / 2 * -1,
					0)
			);

			FaceTopLeft = new FaceTriangle(
				centerTop,
				new Vector3(
					length / 2 * -1,
					width / 2,
					0),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					0)
			);

			FaceTopRight = new FaceTriangle(
				centerTop,
				new Vector3(
					length / 2,
					width / 2,
					0),
				new Vector3(
					length / 2,
					width / 2 * -1,
					0)
			);

			FaceBottomFront = new FaceTriangle(
				centerBottom,
				new Vector3(
					length / 2 * -1,
					width / 2,
					0),
				new Vector3(
					length / 2,
					width / 2,
					0)
			);

			FaceBottomBack = new FaceTriangle(
				centerBottom,
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					0),
				new Vector3(
					length / 2,
					width / 2 * -1,
					0)
			);

			FaceBottomLeft = new FaceTriangle(
				centerBottom,
				new Vector3(
					length / 2 * -1,
					width / 2,
					0),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					0)
			);

			FaceBottomRight = new FaceTriangle(
				centerBottom,
				new Vector3(
					length / 2,
					width / 2,
					0),
				new Vector3(
					length / 2,
					width / 2 * -1,
					0)
			);

			Faces.Add(FaceTopFront);
			Faces.Add(FaceTopBack);
			Faces.Add(FaceTopLeft);
			Faces.Add(FaceTopRight);
			Faces.Add(FaceBottomFront);
			Faces.Add(FaceBottomBack);
			Faces.Add(FaceBottomLeft);
			Faces.Add(FaceBottomRight);
		}
	}
}
