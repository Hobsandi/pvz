using static Application.GameConfig;

namespace Application
{
    public class CannonFactory(ISpellFactory startingSpell)
    {
        public Cannon Create(int lane)
        {
            Magazine magazine = new(START_CAPACITY, MAX_CAPACITY);
            magazine.Load(startingSpell.GenerateSpell());
            return new Cannon(new Position(0, lane), lane, magazine,
                new SymbolSprite('C', ConsoleColor.White));
        }
    }
}
