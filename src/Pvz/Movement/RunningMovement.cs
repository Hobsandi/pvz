using static Application.GameConfig;

namespace Application
{
    // Två steg i taget, alltså dubbelt så snabb som WalkingMovement
    public class RunningMovement : IMovement
    {
        public Position Move(Position from) => from.Shift(-2 * PIXEL_SIZE);
    }
}
