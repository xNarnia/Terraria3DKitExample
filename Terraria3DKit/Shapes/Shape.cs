using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria3DKit.Faces;

namespace Terraria3DKit.Shapes
{
	/// <summary>
	/// An enum containing all possible projection types.
	/// </summary>
	public enum ProjectionType
	{
		Orthographic
		//Perspective
	}

	public abstract class Shape : IShape
	{
		/// <summary>
		/// The faces that compose this shape.
		/// </summary>
		public List<IFace> Faces { get; set; }

		/// <summary>
		/// The type of primitive that composes the faces of this shape.
		/// </summary>
		public PrimitiveType PrimitiveType { get; }
		
		/// <summary>
		/// The default texture to assign to faces without a texture.
		/// </summary>
		public Texture2D DefaultTexture { get; set; }

		/// <summary>
		/// The position of this shape, usually from its origin.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// The rotation on the X-axis in degrees.
		/// </summary>
		public float RotationX { get; set; }

		/// <summary>
		/// The rotation on the Y-axis in degrees.
		/// </summary>
		public float RotationY { get; set; }

		/// <summary>
		/// The rotation on the Z-axis in degrees.
		/// </summary>
		public float RotationZ { get; set; }

		/// <summary>
		/// <para>A flag that designates whether to use Terraria's lighting or ignore it entirely.</para>
		/// Setting this to disabled will leave the model unaffected by darkness.
		/// </summary>
		public bool LightingEnabled { get; set; }

		/// <summary>
		/// <para>Enables coordinate and scaling fixes for models placed in the ModifyInterfaceLayer method.</para>
		/// </summary>
		public bool ModifyInterfaceLayerFix { get; set; }

		/// <summary>
		/// <para>The number of frames to skip before drawing again.</para>
		/// Increase this improves performance. 
		/// </summary>
		//public int FrameskipCount { get; set; }

		/// <summary>
		/// Determines whether the model is visible. This will suspend drawing of the model.
		/// </summary>
		public bool Visible { get; set; }

		/// <summary>
		/// <para>The projection (or view-mode) type this shape will use.</para>
		/// Defaults to perspective.
		/// </summary>
		public ProjectionType Projection { get; set; }

		private BasicEffect _effect { get; set; }
		private VertexPositionNormalTexture[] _vertices { get; set; }
		private int _polyCount { get; set; }
		//private int _framesSkipped { get; set; }
		private GameTime _previousTime { get; set; }

		private static RasterizerState rasterizerState;
		private static DepthStencilState depthStencilState;
		private static BlendState blendState;

		private static RasterizerState previousRS { get; set; }
		private static DepthStencilState previousDS { get; set; }
		private static BlendState previousBS { get; set; }

		/// <summary>
		/// <para>The base model for all shapes.</para>
		/// When inherited from, it provides drawing functionality, vertice handling, and graphic rendering management.
		/// </summary>
		public Shape()
		{
			Faces = new List<IFace>();
			PrimitiveType = PrimitiveType.TriangleList;

			Position = Vector2.Zero;
			RotationX = 0f;
			RotationY = 0f;
			RotationZ = 0f;
			LightingEnabled = true;
			Projection = ProjectionType.Orthographic;
			ModifyInterfaceLayerFix = false;

			if (rasterizerState == null)
			{
				rasterizerState = new RasterizerState();
				rasterizerState.CullMode = CullMode.None;
			}

			if (depthStencilState == null)
				depthStencilState = DepthStencilState.Default;

			if(blendState == null)
				blendState = BlendState.AlphaBlend;

			previousRS = previousRS ?? Main.graphics.GraphicsDevice.RasterizerState;
			previousDS = previousDS ?? Main.graphics.GraphicsDevice.DepthStencilState;
			previousBS = previousBS ?? Main.graphics.GraphicsDevice.BlendState;

			//FrameskipCount = 5;
			Visible = true;

			//_framesSkipped = 0;
		}

		/// <summary>
		/// Resets the rotation of this model to 0 on the X, Y, and Z axis.
		/// </summary>
		public void ResetCameraOrientation()
		{
			RotationX = 0f;
			RotationY = 0f;
			RotationZ = 0f;
		}

		/// <summary>
		/// Rotates the camera around the origin by the given rotation vector in degrees.
		/// </summary>
		public void RotateCamera(Vector3 rotationVectorDegrees)
		{
			RotationX += rotationVectorDegrees.X;
			RotationY += rotationVectorDegrees.Y;
			RotationZ += rotationVectorDegrees.Z;
		}

		/// <summary>
		/// <para>Rotates all vertices in the mesh, from the origin, by the given rotation vector in degrees.</para>
		/// <para>This is useful for rotating an object before placing it in a ShapeGroup.</para> 
		/// For camera rotation, use RotateCamera(rotationVector).
		/// </summary>
		public void RotateVerticesAboutOrigin(Vector3 rotationVectorDegrees)
		{
			foreach (var face in Faces)
			{
				for (var i = 0; i < face.VerticeCount; i++)
				{
					face.Composition[i].Position =
						Vector3.Transform(
							face.Composition[i].Position,
							Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), (float)rotationVectorDegrees.X.ToRadians()));

					face.Composition[i].Position =
						Vector3.Transform(
							face.Composition[i].Position,
							Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), (float)rotationVectorDegrees.Y.ToRadians()));

					face.Composition[i].Position =
						Vector3.Transform(
							face.Composition[i].Position,
							Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), (float)rotationVectorDegrees.Z.ToRadians()));
					if((i + 1) % 3 == 0 && i != face.VerticeCount - 1)
					{
						var newNormal = ShapeHelpers.GetNormal(
							face.Composition[i].Position, 
							face.Composition[i + 1].Position, 
							face.Composition[i + 2].Position);

						face.Composition[i].Normal = newNormal;
						face.Composition[i + 1].Normal = newNormal;
						face.Composition[i + 2].Normal = newNormal;
					}
				}
			}
		}

		/// <summary>
		/// Returns an array of all vertices that make up all faces contained in this model.
		/// </summary>
		/// <returns>Array of vertices in the form of VertexPositionNormalTextures.</returns>
		public VertexPositionNormalTexture[] GetVertexPositions()
		{
			if (!Visible)
				return null;

			if (_vertices == null)
			{
				_polyCount = 0;
				foreach (var face in Faces)
				{
					_polyCount += face.Composition.Count();
				}

				_vertices = new VertexPositionNormalTexture[_polyCount];
				int currentIndex = 0;

				foreach (var face in Faces)
				{
					foreach (var polygon in face.Composition)
					{
						_vertices[currentIndex] = polygon;
						currentIndex++;
					}
				}

				// All shapes are made of triangles. 
				// Divide number of vertices by 3 to get number of polygons
				_polyCount /= 3;
			}

			return _vertices;
		}

		/// <summary>
		/// <para>Draws the shape at the desired coordinates.</para>
		/// Coordinates are aligned to world coordinates.
		/// </summary>
		/// <param name="spriteBatch">Spritebatch that will be used to draw this shape.</param>
		/// <param name="x">The X world coordinate to draw this shape.</param>
		/// <param name="y">The Y world coordinate to draw this shape.</param>
		public void Draw(float x = 0, float y = 0)
			=> Draw(new Vector2(x, y));

		/// <summary>
		/// <para>Draws the shape at the desired coordinates. Coordinates are aligned to world coordinates.</para>
		/// <para>If you place this draw in a Tile draw such as SpecialDraw, post-draw, or another Tile draw, it will not draw at 60 fps.</para>
		/// <para>To draw your 3D model at 60 FPS, place it in the main Mod Draw function of your mod and set (ModifyInterfaceLayerFix = true).</para>
		/// </summary>
		/// <param name="spriteBatch">Spritebatch that will be used to draw this shape.</param>
		/// <param name="pos">The world coordinate to draw this shape.</param>
		public void Draw(Vector2 pos)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			//if (_framesSkipped >= FrameskipCount)
			//{
			//	_framesSkipped = 0;
			//	return;
			//}
			//else
			//{
			//	_framesSkipped++;
			//}

			if (!Visible)
				return;

			_effect = new BasicEffect(spriteBatch.GraphicsDevice);

			// The effect world matrix will handle all visual tranformations to our model
			// This is easier than transforming all the vertices of our model one-by-one
			_effect.World =
					// Handles Rotation
					Matrix.CreateFromYawPitchRoll(
						(float)RotationX.ToRadians(),
						(float)RotationY.ToRadians(),
						(float)RotationZ.ToRadians());

			if (ModifyInterfaceLayerFix)
			{
				// Handles Game Zoom set by the Zoom slider
				_effect.World *=
					Matrix.CreateScale(
						Main.GameViewMatrix.Zoom.X,
						Main.GameViewMatrix.Zoom.X,
						Main.GameViewMatrix.Zoom.Y);
			}

			var posWorldtoScreenCoords = Main.screenPosition - pos;

			// This is not respecting coordinates when put in Tile Draw
			// When this is ran through ModifyLayerInterface, it works appropriately
			// I have no clue what is going on. This is a major impedement.
			if (false)
			{
				float aspectRatio =
					Main.graphics.GraphicsDevice.Viewport.Width / (float)Main.graphics.GraphicsDevice.Viewport.Height;
				float fieldOfView = MathHelper.PiOver4;
				float nearClipPlane = 1;
				float farClipPlane = 10000;

				_effect.View = Matrix.CreateLookAt(
					new Vector3(0, 100, 0), // Camera Position
					Vector3.Zero, // Look-at Vector
					Vector3.UnitZ); // Up Axis is Z

				var x = (((float)Main.screenWidth / 2f)) + posWorldtoScreenCoords.X;
				var y = (((float)Main.screenHeight / 2f)) + posWorldtoScreenCoords.Y;
				var yOffset = -862f;

				if (ModifyInterfaceLayerFix)
				{
					x *= Main.GameZoomTarget;
					y *= Main.GameZoomTarget;
				}
				else
				{
					x += Main.GameZoomTarget;
					y += Main.GameZoomTarget;
				}

				_effect.World = _effect.World
					* Matrix.CreateTranslation(x, yOffset, y);

				_effect.Projection = Matrix.CreatePerspectiveFieldOfView(
					fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
			}
			else if (Projection == ProjectionType.Orthographic)
			{
				// We control the objects rotation by controlling the world
				// This way, we don't need to adjust every individual vertice

				_effect.View = Matrix.CreateLookAt(
					new Vector3(0, 10000, 0), // Camera Position
					Vector3.Zero, // Look-at Vector
					Vector3.UnitZ); // Up Axis is Z

				var x = ((float)Main.screenWidth / 2f + posWorldtoScreenCoords.X);
				var y = ((float)Main.screenHeight / 2f + posWorldtoScreenCoords.Y);

				if (ModifyInterfaceLayerFix)
				{
					x *= Main.GameZoomTarget;
					y *= Main.GameZoomTarget;
				}

				// Moves the object to the desired position
				// Multiplying the Matrices "adds" them together
				//
				// We translate the Y 3000 pixels towards us 
				// so we can see 3D models up to 3000 pixels large
				// We don't see the Y axis in Orthographic mode
				// so it doesn't matter how large we make it!
				_effect.World = _effect.World
					* Matrix.CreateTranslation(x, 3000, y);

				// Produces the "flat" style
				_effect.Projection = 
					Matrix.CreateOrthographic(
						Main.graphics.GraphicsDevice.Viewport.Width, 
						Main.graphics.GraphicsDevice.Viewport.Height, -10000, 10000);
			}
			spriteBatch.End();

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, _effect);
			{
				Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
				Main.graphics.GraphicsDevice.DepthStencilState = depthStencilState;
				Main.graphics.GraphicsDevice.BlendState = blendState;

				foreach (var face in Faces)
				{
					_effect.TextureEnabled = true;
					_effect.Texture = face.Texture ?? DefaultTexture;

					// Lighting
					var x = (int)(pos.X) / 16;
					var y = (int)(pos.Y) / 16;
					Vector3 color = Lighting.GetColor(x, y).ToVector3();

					_effect.LightingEnabled = true;
					_effect.DirectionalLight0.DiffuseColor = color;
					_effect.DirectionalLight1.DiffuseColor = color;
					_effect.DirectionalLight2.DiffuseColor = color;
					_effect.AmbientLightColor = color;
					_effect.DiffuseColor = color;
					_effect.EmissiveColor = color;

					foreach (var pass in _effect.CurrentTechnique.Passes)
					{
						pass.Apply();

						spriteBatch.GraphicsDevice.DrawUserPrimitives(
									PrimitiveType.TriangleList,
							face.Composition,
							0,
							face.PolygonCount);
					}
				}

				spriteBatch.End();
			}

			Main.graphics.GraphicsDevice.RasterizerState = previousRS;
			Main.graphics.GraphicsDevice.DepthStencilState = previousDS;
			Main.graphics.GraphicsDevice.BlendState = previousBS;
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
		}
	}
}
