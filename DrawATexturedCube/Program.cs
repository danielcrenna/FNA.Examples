namespace DrawATexturedCube
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap();
            using var game = new DrawATexturedCube();
            game.Run();
        }
    }
}
