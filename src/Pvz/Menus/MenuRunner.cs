namespace Application
{
    // Kör en meny blockerande tills spelaren väljer. Går inte att avbryta.
    // Den som anropar bestämmer hur skärmen ritas (render), så samma
    // loop används för huvudmenyn, uppgraderingar och kanonval.
    public static class MenuRunner
    {
        public static T Choose<T>(Menu<T> menu, IInput input, Action render,
            Predicate<T>? canChoose = null) where T : IDescribable
        {
            input.Flush();
            while (true)
            {
                render();
                switch (input.Read())
                {
                    case Command.Up: menu.MoveUp(); break;
                    case Command.Down: menu.MoveDown(); break;
                    case Command.Confirm:
                        if (canChoose == null || canChoose(menu.Selected))
                            return menu.Selected;
                        break;
                }
            }
        }
    }
}
