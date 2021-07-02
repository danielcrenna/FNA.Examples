using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrawATexturedCube
{
    internal sealed class DrawATexturedCube : Game
    {
        // ReSharper disable once NotAccessedField.Local
        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Retained")]
        private readonly GraphicsDeviceManager graphics;

        private readonly VertexPositionTexture[] model;
        
        public DrawATexturedCube()
        {
            Content.RootDirectory = "Content";

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

            model = new VertexPositionTexture[3 * 2 * 6];

            // these dimensions should match the texture area you want to display
            var textureOffset = Point.Zero;
            var textureSize = new Point(250, 250);
            var shapeSize = new Vector3(250, 250, 250);

            var pixelWidth = 1f / textureSize.X;
            var pixelHeight = 1f / textureSize.Y;
            var x1 = pixelWidth * textureOffset.X;
            var x2 = pixelWidth * (textureOffset.X + shapeSize.X);
            var y1 = pixelHeight * textureOffset.Y;
            var y2 = pixelHeight * (textureOffset.Y + shapeSize.Y);
			
            var ttl = new Vector2(x1, y1);
            var ttr = new Vector2(x2, y1);
            var tbl = new Vector2(x1, y2);
            var tbr = new Vector2(x2, y2);
            
            //
			// FRONT:
			model[0] = new VertexPositionTexture(tl + Vector3.UnitZ, ttl);
			model[1] = new VertexPositionTexture(bl + Vector3.UnitZ, tbl);
			model[2] = new VertexPositionTexture(tr + Vector3.UnitZ, ttr);
			model[3] = new VertexPositionTexture(bl + Vector3.UnitZ, tbl);
			model[4] = new VertexPositionTexture(br + Vector3.UnitZ, tbr);
			model[5] = new VertexPositionTexture(tr + Vector3.UnitZ, ttr);

			//
			// BACK:
			model[6]  = new VertexPositionTexture(tl + -Vector3.UnitZ, ttr);
			model[7]  = new VertexPositionTexture(tr + -Vector3.UnitZ, ttl);
			model[8]  = new VertexPositionTexture(bl + -Vector3.UnitZ, tbr);
			model[9]  = new VertexPositionTexture(bl + -Vector3.UnitZ, tbr);
			model[10] = new VertexPositionTexture(tr + -Vector3.UnitZ, ttl);
			model[11] = new VertexPositionTexture(br + -Vector3.UnitZ, tbl);

			//
			// TOP:
			model[12] = new VertexPositionTexture(tl +  Vector3.UnitZ, tbl);
			model[13] = new VertexPositionTexture(tr + -Vector3.UnitZ, ttr);
			model[14] = new VertexPositionTexture(tl + -Vector3.UnitZ, ttl);
			model[15] = new VertexPositionTexture(tl +  Vector3.UnitZ, tbl);
			model[16] = new VertexPositionTexture(tr +  Vector3.UnitZ, tbr);
			model[17] = new VertexPositionTexture(tr + -Vector3.UnitZ, ttr);

			//
			// BOTTOM:
			model[18] = new VertexPositionTexture(bl +  Vector3.UnitZ, ttl);
			model[19] = new VertexPositionTexture(bl + -Vector3.UnitZ, tbl);
			model[20] = new VertexPositionTexture(br + -Vector3.UnitZ, tbr);
			model[21] = new VertexPositionTexture(bl +  Vector3.UnitZ, ttl);
			model[22] = new VertexPositionTexture(br + -Vector3.UnitZ, tbr);
			model[23] = new VertexPositionTexture(br +  Vector3.UnitZ, ttr);

			//
			// LEFT:
			model[24] = new VertexPositionTexture(tl +  Vector3.UnitZ, ttr);
			model[25] = new VertexPositionTexture(bl + -Vector3.UnitZ, tbl);
			model[26] = new VertexPositionTexture(bl +  Vector3.UnitZ, tbr);
			model[27] = new VertexPositionTexture(tl + -Vector3.UnitZ, ttl);
			model[28] = new VertexPositionTexture(bl + -Vector3.UnitZ, tbl);
			model[29] = new VertexPositionTexture(tl +  Vector3.UnitZ, ttr);

			//
			// RIGHT:
			model[30] = new VertexPositionTexture(tr +  Vector3.UnitZ, ttl);
			model[31] = new VertexPositionTexture(br +  Vector3.UnitZ, tbl);
			model[32] = new VertexPositionTexture(br + -Vector3.UnitZ, tbr);
			model[33] = new VertexPositionTexture(tr + -Vector3.UnitZ, ttr);
			model[34] = new VertexPositionTexture(tr +  Vector3.UnitZ, ttl);
			model[35] = new VertexPositionTexture(br + -Vector3.UnitZ, tbr);
        }

        private RasterizerState rasterizerState;
        private BasicEffect effect;

        protected override void Initialize()
        {
            var aspectRatio = Window.ClientBounds.Width / (float) Window.ClientBounds.Height;

            effect = new BasicEffect(GraphicsDevice)
            {
                TextureEnabled = true,
                Projection = Matrix.CreatePerspectiveFieldOfView((float) Math.PI / 4.0f, aspectRatio, 1f, 10000f),
                View = Matrix.CreateTranslation(0f, 0f, -10f)
            };

            rasterizerState = new RasterizerState { CullMode = CullMode.CullClockwiseFace };

            // Calls LoadContent
            base.Initialize();
        }

        protected override void LoadContent()
        {
            effect.Texture = Content.Load<Texture2D>("handpaintedwall2");
        }

        protected override void UnloadContent()
        {
            effect.Texture.Dispose();
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