# Roadmap

A rough order to build the game in, so the end result looks roughly like the
`claude-version` branch. These are guidelines, not instructions. Make your own
choices along the way.

## How to work

- **One step at a time.** Run the game after each step. It should always work.
- **Commit after each step**, so you can always go back.
- **Look at `claude-version` when you get stuck**, but write the code yourselves:
  `git show claude-version:src/Pvz/Entities/Magazine.cs` shows a file without
  switching branches.
- **Write the FALL comment while you build**, not afterwards. You remember
  *why* best when you just did it.
- **Keep `Game` as the one that connects things, not the one that does
  everything.** Pass values in through constructors. Classes shouldn't ask
  `Game` for things.

---

## 0. Clean up
- `Magazine` crashes (`spells` is never created). Fix it or leave it until step 1.
- Remove empty or unused files.
- **Done when:** the game builds without warnings.

## 1. Magazine and the basic cannon
The goal of the game: every cannon starts with the same simple spell, and gets
more through upgrades.
- Make `Magazine` (or `IMagazine`) hold a cannon's spells, with a maximum size.
  When it's full, the first spell is removed.
- Let `Cannon` take a magazine instead of its own list.
- Think about how the cannon picks its *next* spell, and what happens when the
  list changes in the middle of a round.
- All cannons start with the same basic spell. A factory for it fits well.
- **Done when:** four identical cannons fire the basic spell.
- *Report:* Iterator (different firing orders), Factory.

## 2. Kill counter
- A zombie should count as dead **once**, whether it's killed or reaches the
  base. Watch out for zombies that take damage from several spells in the same tick.
- A class that counts kills, and that others can *subscribe to* (more things
  will need to know about kills later).
- Show kills below the board, like HP is shown now.
- **Done when:** the number goes up correctly.
- *Report:* Observer.

## 3. Difficulty that changes during a round
- An object per round that holds the current difficulty, which is **passed on**
  (not copied as numbers) to whoever needs it.
- Something that raises the difficulty: time (an `IUpdater`) or kills (an
  observer from step 2).
- Zombie factories: the spawner decides *when* and *where*, and a factory
  decides *what*. Zombies get more health at higher difficulty.
- **Done when:** the game gets noticeably harder the longer you survive.
- *Report:* Factory, possibly Template Method or Decorator.

## 4. Upgrades (pauses the game)
- After X kills (X should grow each time) the game pauses and shows four
  choices: three spells and one heal/extra slot.
- Reuse your `Menu<T>`. Think about what the choices have in common:
  something that can describe itself and *do* something.
- Spell or slot: then choose which cannon gets it. Heal: no cannon needed.
- Open the menu *after* the board has updated, not in the middle of an update.
- **Done when:** you can choose an upgrade and see that the cannon changes.
- *Report:* Command, generics with a constraint (`Menu<T> where T : ...`).

## 5. Screen layout and colours
- Split the screen into areas: the field in the middle, HP and kills beside it,
  and room for cannon info.
- An area you can draw into without knowing where on the screen it is, so
  everything that draws today can keep its own coordinates.
- Colours: let sprites have a colour, and let the canvas store a colour per cell.
- HP as a number.
- **Done when:** the board is centred and the information is around it.

## 6. Cannon info alongside the game
- Up/down selects a cannon while the game keeps running, and its spells are
  shown on the side.
- Mark the selected cannon on the field, e.g. `[C]`.
- Read keys *without waiting* during the game (`Console.KeyAvailable`).
- Long spell names need to wrap onto several lines.
- **Done when:** you can browse the cannons while zombies keep walking.

## 7. More lanes
- After Y kills a new lane opens with a basic cannon, up to a maximum.
- Everything that uses `ROWS` today needs to ask the board how many lanes there are.
- **Done when:** a fifth lane appears in the middle of a round.

## 8. Rarity
- Spells come in common, rare and epic, and become rarer the better they are.
- A table where each option has a weight, and the weights can change during
  the round (more epics later).
- Show rarity with colour in the upgrade menu.
- Gather all balancing in one place, so it's easy to tweak.
- **Done when:** better spells show up more often the further you get.
- *Report:* generics (a weighted table works for spells *and* zombies).

## 9. Main menu, game over and highscore
- Instructions and lore in the main menu (you've already started).
- Game over: show the score, enter a name, and an empty name means it isn't saved.
- Top 10 saved to a file, which shouldn't crash if the file is missing or broken.
- **Done when:** the highscore list is still there after restarting the game.
- *Report:* State (screens that hand over to the next screen).

## 10. Finish
- Go through all the FALL comments: VAD, HUR, VARFÖR.
- Balance: play and adjust the numbers.
- Remove old code and comments that are no longer true.

---

### If you run out of time
Steps 1–4 are the core of the game. 5–6 make it nicer to play, and 7–9 are
extras. A working game with steps 1–4 is better than a half-finished one with
everything.
