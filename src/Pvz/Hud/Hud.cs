using static Application.GameConfig;

namespace Application
{
    public class Hud(PlayerBase playerBase, Board board, KillCounter kills,
        UpgradeTrigger upgrades, int best)
    {
        // Visas i stället för poängen när spelaren måste välja något
        public string? Prompt { get; set; }

        public void Draw(Layout layout)
        {
            if (Prompt != null)
                layout.Top.WriteCentered(0, Prompt, ConsoleColor.Yellow);
            else
                layout.Top.WriteCentered(0,
                    $"KILLS {kills.Total}   NEXT UPGRADE {upgrades.NextThreshold}"
                    + $"   BEST {Math.Max(best, kills.Total)}",
                    ConsoleColor.White);

            ConsoleColor health = playerBase.Health <= 1
                ? ConsoleColor.Red : ConsoleColor.Green;
            layout.Left.Write(0, 0, playerBase.Describe(), health);
            layout.Left.Write(0, 2, $"Lanes {board.LaneCount}/{MAX_LANES}");

            layout.Help.WriteCentered(0,
                Prompt == null ? "↑↓ select cannon" : "↑↓ choose   Enter confirm",
                ConsoleColor.DarkGray);
        }
    }
}
