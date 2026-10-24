namespace Application
{
    // Översätter ConsoleColor till ANSI-koder, som terminalen tolkar som
    // "byt färg" i stället för att skriva ut dem. Canvas använder den när
    // en bild skrivs ut.
    public class AnsiPalette
    {
        // En kod per ConsoleColor, i samma ordning som enumen: Black,
        // DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow,
        // Gray, DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
        readonly int[] codes =
            [30, 34, 32, 36, 31, 35, 33, 37, 90, 94, 92, 96, 91, 95, 93, 97];

        // T.ex. ConsoleColor.Red -> "\x1b[91m"
        public string Encode(ConsoleColor color)
            => $"\x1b[{codes[(int)color]}m";

        // Tillbaka till terminalens vanliga färger
        public string Reset() => "\x1b[0m";
    }
}
