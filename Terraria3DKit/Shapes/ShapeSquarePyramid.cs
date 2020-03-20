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
	/// A pyramid shape consisting of 5 faces with the base of a square.
	/// </summary>
	public class ShapeSquarePyramid : Shape
	{
		/// <summary>
		/// The size of the base of the pyramid on the X-axis in pixels.
		/// </summary>
		public float Length { get; }

		/// <summary>
		/// The size of the base of the pyramid on the Y-axis in pixels.
		/// </summary>
		public float Width { get; }

		/// <summary>
		/// The size of the base of the pyramid on the Z-axis in pixels.
		/// </summary>
		public float Height { get; }

		public IFace FaceFront { get; }
		public IFace FaceBack { get; }
		public IFace FaceLeft { get; }
		public IFace FaceRight { get; }
		public IFace FaceBottom { get; }

		/// <summary>
		/// Produces a square pyramid with 5 faces.
		/// </summary>
		/// <param name="length">The length (x) of the pyramid's base in pixels.</param>
		/// <param name="width">The width (y) of the pyramid's base in pixels.</param>
		/// <param name="height">The height (z) of the pyramid in pixels.</param>
		public ShapeSquarePyramid(float length, float width, float height)
		{
			Length = length;
			Width = width;
			Height = height;

			// Front is towards you
			// Back is away from you
			Vector3 center = 
				new Vector3(
					0,
					0,
					height / 2);

			FaceFront = new FaceTriangle(
				center,
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2,
					height / 2 * -1)
			);

			FaceBack = new FaceTriangle(
				center,
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2 * -1)
			);

			FaceLeft = new FaceTriangle(
				center,
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2 * -1)
			);

			FaceRight = new FaceTriangle(
				center,
				new Vector3(
					length / 2,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2 * -1)
			);

			FaceBottom = new FaceSquare(
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2 * -1)
				);

			Faces.Add(FaceFront);
			Faces.Add(FaceBack);
			Faces.Add(FaceLeft);
			Faces.Add(FaceRight);
			Faces.Add(FaceBottom);
		}
	}
}
