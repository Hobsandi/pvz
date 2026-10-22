using static Application.GameConfig;

namespace Application
{
    public class WalkingMovement : IMovement
    {
        public Position Move(Position from) => from.Shift(-PIXEL_SIZE);
    }
}
