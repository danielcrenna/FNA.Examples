using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace HelloWorld
{
    internal sealed class HelloWorld : Game
    {
        // ReSharper disable once NotAccessedField.Local
        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Retained")]
        private readonly GraphicsDeviceManager graphics;

        public HelloWorld()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}