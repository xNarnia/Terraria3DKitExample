using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria3DKit
{
	public static class Extensions
	{
		/// <summary>
		/// Converts this value from degrees to radians.
		/// </summary>
		/// <param name="degrees">Value in degrees.</param>
		/// <returns>The value converted from degrees to radians.</returns>
		public static double ToRadians(this double degrees) => degrees * (Math.PI / 180);

		/// <summary>
		/// Converts this value from degrees to radians.
		/// </summary>
		/// <param name="degrees">Value in degrees.</param>
		/// <returns>The value converted from degrees to radians.</returns>
		public static double ToRadians(this float degrees) => degrees * (Math.PI / 180);

		/// <summary>
		/// Converts this value from radians to degrees.
		/// </summary>
		/// <param name="radians">Value in radians.</param>
		/// <returns>The value converted from radians to degrees.</returns>
		public static double ToDegrees(this double radians) => radians * (180 / Math.PI);

		/// <summary>
		/// Converts this value from radians to degree.
		/// </summary>
		/// <param name="radians">Value in radians.</param>
		/// <returns>The value converted from radians to degrees.</returns>
		public static double ToDegrees(this float radians) => radians * (180 / Math.PI);
	}
}
