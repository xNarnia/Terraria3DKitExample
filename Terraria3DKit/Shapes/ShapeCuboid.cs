using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria3DKit.Faces;

namespace Terraria3DKit.Shapes
{
	/// <summary>
	/// A cube shape consisting of 6 faces.
	/// </summary>
	public class ShapeCuboid : Shape
	{
		/// <summary>
		/// The size of the cube on the X-axis in pixels.
		/// </summary>
		public float Length { get; }

		/// <summary>
		/// The size of the cube on the Y-axis in pixels.
		/// </summary>
		public float Width { get; }

		/// <summary>
		/// The size of the cube on the Z-axis in pixels.
		/// </summary>
		public float Height { get; }

		public IFace FaceTop { get; }
		public IFace FaceBottom { get; }
		public IFace FaceFront { get; }
		public IFace FaceBack { get; }
		public IFace FaceLeft { get; }
		public IFace FaceRight { get; }

		/// <summary>
		/// Produces a cube with 6 sides.
		/// </summary>
		/// <param name="length">The length (x) of the cuboid's base in pixels.</param>
		/// <param name="width">The width (y) of the cuboid's base in pixels.</param>
		/// <param name="height">The height (z) of the total cuboid.</param>
		public ShapeCuboid(float length, float width, float height)
		{
			// Front is towards you
			// Back is away from you
			Length = length;
			Width = width;
			Height = height;

			// Place faces so that Position is origin
			FaceTop = new FaceSquare(
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2),
				new Vector3(
					length / 2,
					width / 2,
					height / 2),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2)
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

			FaceFront = new FaceSquare(
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2 * -1)
				);

			FaceBack = new FaceSquare(
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2),
				new Vector3(
					length / 2,
					width / 2,
					height / 2),
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2,
					height / 2 * -1)
				);

			FaceLeft = new FaceSquare(
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2),
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2),
				new Vector3(
					length / 2 * -1,
					width / 2 * -1,
					height / 2 * -1),
				new Vector3(
					length / 2 * -1,
					width / 2,
					height / 2 * -1)
				);

			FaceRight = new FaceSquare(
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2),
				new Vector3(
					length / 2,
					width / 2,
					height / 2),
				new Vector3(
					length / 2,
					width / 2 * -1,
					height / 2 * -1),
				new Vector3(
					length / 2,
					width / 2,
					height / 2 * -1)
				);

			Faces.Add(FaceTop);
			Faces.Add(FaceBottom);
			Faces.Add(FaceFront);
			Faces.Add(FaceBack);
			Faces.Add(FaceLeft);
			Faces.Add(FaceRight);

			Visible = true;
		}
	}
}
