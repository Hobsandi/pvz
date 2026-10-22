namespace Application
{
    public class Game(Board board, PlayerBase playerBase, Canvas canvas)
    {
        public void Run(int tickMs)
        {
            Console.Write("\x1b[2J");
            while (!playerBase.IsDestroyed())
            {
                board.Update();
                canvas.Clear();
                board.Draw(canvas);
                canvas.Render();
                Console.WriteLine(playerBase.Describe());
                Thread.Sleep(tickMs);
            }
            Console.WriteLine("Game over!");
        }
    }
}
