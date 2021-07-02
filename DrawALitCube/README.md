Draw A Lit Cube
===============

- Set up your game as per [Draw A Textured Cube](https://github.com/danielcrenna/FNA.Examples/blob/main/DrawATexturedCube/README.md)
- Rename the `DrawATexturedCube.cs` class and file to `DrawALitCube.cs`
- In the game class' `Initialize` method, after the `BasicEffect` creation call, add a call to `EnableDefaultLighting`:

```csharp
// Sets LightingEnabled to true
effect.EnableDefaultLighting();
```

- Change the vertices model from `VertexPositionTexture` to `VertexPositionNormalTexture`:

```csharp
model = new VertexPositionNormalTexture[3 * 2 * 6];

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
model[0] = new VertexPositionNormalTexture(tl + Vector3.UnitZ, Vector3.UnitZ, ttl);
model[1] = new VertexPositionNormalTexture(bl + Vector3.UnitZ, Vector3.UnitZ, tbl);
model[2] = new VertexPositionNormalTexture(tr + Vector3.UnitZ, Vector3.UnitZ, ttr);
model[3] = new VertexPositionNormalTexture(bl + Vector3.UnitZ, Vector3.UnitZ, tbl);
model[4] = new VertexPositionNormalTexture(br + Vector3.UnitZ, Vector3.UnitZ, tbr);
model[5] = new VertexPositionNormalTexture(tr + Vector3.UnitZ, Vector3.UnitZ, ttr);

//
// BACK:
model[6]  = new VertexPositionNormalTexture(tl + -Vector3.UnitZ, Vector3.UnitZ, ttr);
model[7]  = new VertexPositionNormalTexture(tr + -Vector3.UnitZ, Vector3.UnitZ, ttl);
model[8]  = new VertexPositionNormalTexture(bl + -Vector3.UnitZ, Vector3.UnitZ, tbr);
model[9]  = new VertexPositionNormalTexture(bl + -Vector3.UnitZ, Vector3.UnitZ, tbr);
model[10] = new VertexPositionNormalTexture(tr + -Vector3.UnitZ, Vector3.UnitZ, ttl);
model[11] = new VertexPositionNormalTexture(br + -Vector3.UnitZ, Vector3.UnitZ, tbl);

//
// TOP:
model[12] = new VertexPositionNormalTexture(tl +  Vector3.UnitZ, Vector3.UnitZ, tbl);
model[13] = new VertexPositionNormalTexture(tr + -Vector3.UnitZ, Vector3.UnitZ, ttr);
model[14] = new VertexPositionNormalTexture(tl + -Vector3.UnitZ, Vector3.UnitZ, ttl);
model[15] = new VertexPositionNormalTexture(tl +  Vector3.UnitZ, Vector3.UnitZ, tbl);
model[16] = new VertexPositionNormalTexture(tr +  Vector3.UnitZ, Vector3.UnitZ, tbr);
model[17] = new VertexPositionNormalTexture(tr + -Vector3.UnitZ, Vector3.UnitZ, ttr);

//
// BOTTOM:
model[18] = new VertexPositionNormalTexture(bl +  Vector3.UnitZ, Vector3.UnitZ, ttl);
model[19] = new VertexPositionNormalTexture(bl + -Vector3.UnitZ, Vector3.UnitZ, tbl);
model[20] = new VertexPositionNormalTexture(br + -Vector3.UnitZ, Vector3.UnitZ, tbr);
model[21] = new VertexPositionNormalTexture(bl +  Vector3.UnitZ, Vector3.UnitZ, ttl);
model[22] = new VertexPositionNormalTexture(br + -Vector3.UnitZ, Vector3.UnitZ, tbr);
model[23] = new VertexPositionNormalTexture(br +  Vector3.UnitZ, Vector3.UnitZ, ttr);

//
// LEFT:
model[24] = new VertexPositionNormalTexture(tl +  Vector3.UnitZ, Vector3.UnitZ, ttr);
model[25] = new VertexPositionNormalTexture(bl + -Vector3.UnitZ, Vector3.UnitZ, tbl);
model[26] = new VertexPositionNormalTexture(bl +  Vector3.UnitZ, Vector3.UnitZ, tbr);
model[27] = new VertexPositionNormalTexture(tl + -Vector3.UnitZ, Vector3.UnitZ, ttl);
model[28] = new VertexPositionNormalTexture(bl + -Vector3.UnitZ, Vector3.UnitZ, tbl);
model[29] = new VertexPositionNormalTexture(tl +  Vector3.UnitZ, Vector3.UnitZ, ttr);

//
// RIGHT:
model[30] = new VertexPositionNormalTexture(tr +  Vector3.UnitZ, Vector3.UnitZ, ttl);
model[31] = new VertexPositionNormalTexture(br +  Vector3.UnitZ, Vector3.UnitZ, tbl);
model[32] = new VertexPositionNormalTexture(br + -Vector3.UnitZ, Vector3.UnitZ, tbr);
model[33] = new VertexPositionNormalTexture(tr + -Vector3.UnitZ, Vector3.UnitZ, ttr);
model[34] = new VertexPositionNormalTexture(tr +  Vector3.UnitZ, Vector3.UnitZ, ttl);
model[35] = new VertexPositionNormalTexture(br + -Vector3.UnitZ, Vector3.UnitZ, tbr);
```

- Run the game
- Use the arrow keys to rotate the lit cube

Texture:
--------
The texture image provided in this example is from [OpenGameArt.org](https://opengameart.org/content/handpainted-stone-wall-textures).
