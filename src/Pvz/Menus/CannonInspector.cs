namespace Application
{
    // Panelen till höger: visar vald kanons spells medan spelet kör.
    // Samma Menu<Cannon> används när en uppgradering ska få en kanon.
    public class CannonInspector(Board board)
    {
        public Menu<Cannon> Menu { get; } = new("Cannons", board.Cannons);

        public void Handle(Command command)
        {
            if (command == Command.Up) Menu.MoveUp();
            if (command == Command.Down) Menu.MoveDown();
        }

        public void Draw(Layout layout)
        {
            Cannon cannon = Menu.Selected;

            layout.Gutter.Plot(0, cannon.Lane, '[', ConsoleColor.Yellow);
            layout.Gutter.Plot(1, cannon.Lane, 'C', ConsoleColor.Yellow);
            layout.Gutter.Plot(2, cannon.Lane, ']', ConsoleColor.Yellow);

            ICanvas panel = layout.Right;
            panel.Write(0, 0, cannon.Describe(), ConsoleColor.Yellow);
            int y = 2;
            int number = 1;
            foreach (ISpell spell in cannon.Spells)
                y += panel.WriteWrapped(0, y, panel.Width,
                    $"{number++}. {spell.Describe()}", indent: 3);

            if (cannon.IsFull)
                panel.WriteWrapped(0, y + 1, panel.Width,
                    "Full: a new spell replaces #1", ConsoleColor.DarkGray);
        }
    }
}
