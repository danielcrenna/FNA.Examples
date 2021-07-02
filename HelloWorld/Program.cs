namespace HelloWorld
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap();
            using var game = new HelloWorld();
            game.Run();
        }
    }
}
