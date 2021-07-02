Draw A Cube
===========

- Set up your game as per [Draw A Cube](https://github.com/danielcrenna/FNA.Examples/blob/main/DrawACube/README.md)
- Rename the `DrawACube.cs` class and file to `DrawATexturedCube.cs`
- In the game class' `Initialize` method, change the `BasicEffect` creation to use `TextureEnabled`, not `VertexColorEnabled`:

```csharp
effect = new BasicEffect(GraphicsDevice)
{
    TextureEnabled = true,
    Projection = Matrix.CreatePerspectiveFieldOfView((float) Math.PI / 4.0f, aspectRatio, 1f, 10000f),
    View = Matrix.CreateTranslation(0f, 0f, -10f)
};
```

- Create a new directory inside the project directory, and call it `Content`
- Add a texture file to the `Content` folder (_in this example, we're using a file called `handpaintedwall2.png'_)
- In the class constructor, add the following line to set the game's asset directory:

```csharp
Content.RootDirectory = "Content";
```

- Add `LoadContent` and `UnloadContent` override methods to load and unload the image file as a `Texture2D` on the `BasicEffect`:

```csharp
protected override void LoadContent()
{
    effect.Texture = Content.Load<Texture2D>("handpaintedwall2");
}

protected override void UnloadContent()
{
    effect.Texture.Dispose();
}
```

- Change the vertices model from `VertexPositionColor` to `VertexPositionTexture`:

```csharp
private readonly VertexPositionTexture[] model;
```

- Modify the model vertices creation in the game constructor:

```csharp
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
```
- Run the game
- Use the arrow keys to rotate the textured cube
