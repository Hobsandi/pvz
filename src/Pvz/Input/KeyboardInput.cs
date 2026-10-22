namespace Application
{
    public class KeyboardInput : IInput
    {
        public bool TryRead(out Command command)
        {
            while (Console.KeyAvailable)
                if (TryMap(Console.ReadKey(true).Key, out command)) return true;
            command = default;
            return false;
        }

        public Command Read()
        {
            while (true)
                if (TryMap(Console.ReadKey(true).Key, out Command command))
                    return command;
        }

        public void Flush()
        {
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        static bool TryMap(ConsoleKey key, out Command command)
        {
            Command? mapped = key switch
            {
                ConsoleKey.UpArrow or ConsoleKey.W => Command.Up,
                ConsoleKey.DownArrow or ConsoleKey.S => Command.Down,
                ConsoleKey.Enter or ConsoleKey.Spacebar => Command.Confirm,
                ConsoleKey.Escape or ConsoleKey.Backspace => Command.Back,
                _ => null,
            };
            command = mapped ?? default;
            return mapped != null;
        }
    }
}
