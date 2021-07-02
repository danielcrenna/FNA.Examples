Hello, World:
=============

- Create a new Console Application
- Install NuGet Package `FNA.Package` into the Console Application project
- Change `Program.cs` to partial and invoke the bootstrap method:

```csharp
namespace HelloWorld
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Bootstrap();
        }
    }
}
```

- Create a simple `HelloWorld.cs` game class from a sub-class of `Microsoft.Xna.Framework.Game`:

```csharp
using Microsoft.Xna.Framework;

namespace HelloWorld
{
    internal sealed class HelloWorld : Game
    {
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
```

- Run the game after the bootstrapping method in `Program.cs`:

```csharp
namespace HelloWorld
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Bootstrap();
            using var game = new HelloWorld();
            game.Run();
        }
    }
}
```

- Run the Console Application