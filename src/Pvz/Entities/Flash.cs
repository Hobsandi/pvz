namespace Application
{
    public class Flash(ISprite sprite, Position position, int ticks) : IUpdater, IDrawable
    {
        private bool alive = true;
        
        public void Draw(Canvas canvas) => sprite.Draw(canvas, position);

        public bool IsAlive() => alive;

        public void Update()
        {
            ticks--;
            if (ticks < 1)
            {
                alive = false;
            }
        }
    }
}