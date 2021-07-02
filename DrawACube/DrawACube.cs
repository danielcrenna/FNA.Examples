using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrawACube
{
    internal sealed class DrawACube : Game
    {
        // ReSharper disable once NotAccessedField.Local
        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Retained")]
        private readonly GraphicsDeviceManager graphics;

        private readonly VertexPositionColor[] model;
        
        public DrawACube()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };

            var face = new Vector3[2 * 3]; // two triangles = 1 face or quad
            face[0] = new Vector3(-1.0f,  1.0f,  0.0f);  // TL
            face[1] = new Vector3(-1.0f, -1.0f,  0.0f);  // BL
            face[2] = new Vector3( 1.0f,  1.0f,  0.0f);  // TR
            face[3] = new Vector3(-1.0f, -1.0f,  0.0f);  // BL (redundant)
            face[4] = new Vector3( 1.0f, -1.0f,  0.0f);  // BR
            face[5] = new Vector3( 1.0f,  1.0f,  0.0f);  // TR (redundant)

            var tl = face[0];
            var bl = face[1];
            var tr = face[2];
            var br = face[4];

            model = new VertexPositionColor[3 * 2 * 6];
            
            //
			// FRONT:
			model[0] = new VertexPositionColor(face[0] + Vector3.UnitZ, Color.Red);
			model[1] = new VertexPositionColor(face[1] + Vector3.UnitZ, Color.Red);
			model[2] = new VertexPositionColor(face[2] + Vector3.UnitZ, Color.Red);
			model[3] = new VertexPositionColor(face[3] + Vector3.UnitZ, Color.Red);
			model[4] = new VertexPositionColor(face[4] + Vector3.UnitZ, Color.Red);
			model[5] = new VertexPositionColor(face[5] + Vector3.UnitZ, Color.Red);

			//
			// BACK:
			model[6]  = new VertexPositionColor(tl + -Vector3.UnitZ, Color.Green);
			model[7]  = new VertexPositionColor(tr + -Vector3.UnitZ, Color.Green);
			model[8]  = new VertexPositionColor(bl + -Vector3.UnitZ, Color.Green);
			model[9]  = new VertexPositionColor(bl + -Vector3.UnitZ, Color.Green);
			model[10] = new VertexPositionColor(tr + -Vector3.UnitZ, Color.Green);
			model[11] = new VertexPositionColor(br + -Vector3.UnitZ, Color.Green);

			//
			// TOP:
			model[12] = new VertexPositionColor(tl +  Vector3.UnitZ, Color.Blue);
			model[13] = new VertexPositionColor(tr + -Vector3.UnitZ, Color.Blue);
			model[14] = new VertexPositionColor(tl + -Vector3.UnitZ, Color.Blue);
			model[15] = new VertexPositionColor(tl +  Vector3.UnitZ, Color.Blue);
			model[16] = new VertexPositionColor(tr +  Vector3.UnitZ, Color.Blue);
			model[17] = new VertexPositionColor(tr + -Vector3.UnitZ, Color.Blue);

			//
			// BOTTOM:
			model[18] = new VertexPositionColor(bl +  Vector3.UnitZ, Color.Yellow);
			model[19] = new VertexPositionColor(bl + -Vector3.UnitZ, Color.Yellow);
			model[20] = new VertexPositionColor(br + -Vector3.UnitZ, Color.Yellow);
			model[21] = new VertexPositionColor(bl +  Vector3.UnitZ, Color.Yellow);
			model[22] = new VertexPositionColor(br + -Vector3.UnitZ, Color.Yellow);
			model[23] = new VertexPositionColor(br +  Vector3.UnitZ, Color.Yellow);

			//
			// LEFT:
			model[24] = new VertexPositionColor(tl +  Vector3.UnitZ, Color.Purple);
			model[25] = new VertexPositionColor(bl + -Vector3.UnitZ, Color.Purple);
			model[26] = new VertexPositionColor(bl +  Vector3.UnitZ, Color.Purple);
			model[27] = new VertexPositionColor(tl + -Vector3.UnitZ, Color.Purple);
			model[28] = new VertexPositionColor(bl + -Vector3.UnitZ, Color.Purple);
			model[29] = new VertexPositionColor(tl +  Vector3.UnitZ, Color.Purple);

			//
			// RIGHT:
			model[30] = new VertexPositionColor(tr +  Vector3.UnitZ, Color.Pink);
			model[31] = new VertexPositionColor(br +  Vector3.UnitZ, Color.Pink);
			model[32] = new VertexPositionColor(br + -Vector3.UnitZ, Color.Pink);
			model[33] = new VertexPositionColor(tr + -Vector3.UnitZ, Color.Pink);
			model[34] = new VertexPositionColor(tr +  Vector3.UnitZ, Color.Pink);
			model[35] = new VertexPositionColor(br + -Vector3.UnitZ, Color.Pink);
        }

        private RasterizerState rasterizerState;
        private BasicEffect effect;

        protected override void Initialize()
        {
            var aspectRatio = Window.ClientBounds.Width / (float) Window.ClientBounds.Height;

            effect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                Projection = Matrix.CreatePerspectiveFieldOfView((float) Math.PI / 4.0f, aspectRatio, 1f, 10000f),
                View = Matrix.CreateTranslation(0f, 0f, -10f)
            };

            rasterizerState = new RasterizerState { CullMode = CullMode.CullClockwiseFace };

            // Calls LoadContent
            base.Initialize();
        }

        private float rotateX;
        private float rotateY;

        protected override void Update(GameTime gameTime)
        {
            // Calls FrameworkDispatcher
            base.Update(gameTime); 
			
            Input.Update(IsActive);

            var changed = false;

            if (Input.IsKeyDown(Keys.Right))
            {
                changed = true;
                rotateY += 0.05f;
            }
            if (Input.IsKeyDown(Keys.Left))
            {
                changed = true;
                rotateY -= 0.05f;
            }
            if (Input.IsKeyDown(Keys.Up))
            {
                changed = true;
                rotateX += 0.05f;
            }
            if (Input.IsKeyDown(Keys.Down))
            {
                changed = true;
                rotateX -= 0.05f;
            }

            if (changed)
            {
                effect.View = Matrix.CreateRotationY(rotateY) * Matrix.CreateRotationX(rotateX) * Matrix.CreateTranslation(0f, 0f, -10);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                // TriangleList is drawing each triangle in isolation.
                // This gives us face-independent culling, which means the
                // GPU relies on winding order defined in RasterizerState

                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, model, 0, 12 /* 2 triangles per face, 6 faces in a cube */);
            }

            base.Draw(gameTime);
        }
    }
}