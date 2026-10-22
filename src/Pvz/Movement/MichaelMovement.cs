using static Application.GameConfig;

namespace Application
{
    public class MichaelMovement : IMovement
    {
        int counter = 0;
        public Position Move(Position from)
        {
            counter = (counter + 1) % 5;
            if (counter == 2)
            {
                return from.Shift(PIXEL_SIZE);
            }
            else
            {
                return from.Shift(-PIXEL_SIZE);
            }
        }
    }   
}