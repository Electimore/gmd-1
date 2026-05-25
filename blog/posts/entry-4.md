# Timeless — Dev Update #2

## Intro
- milestone: implement interactivity and get the time loop work

## Interaction System
- The `IInteractable` interface (`Interact()` / `Dismiss()`) — why an interface instead of hardcoding per-object logic
- Any object in the scene can become interactable just by implementing it; makes adding future puzzles and props easier
- `PlayerInteraction.cs` wires it all together: scan for interactables every frame, highlight what's in range, fire on button press

## SphereCast vs Raycast
- A pure raycast didn't work all that well — you had to aim precisely at small objects, `SphereCast` gives a wider, more forgiving detection cone
- The QuickOutline highlight: the hovered object gets an outline component added/enabled at runtime so the player always has clear feedback on what they're about to interact with

## The Clues
- `Clue.cs` implements `IInteractable` — picking up a note pauses the game (`Time.timeScale = 0`) and shows a paper-style UI with text and an optional image
- The shared static canvas pattern: all `Clue` instances in the scene reuse one single canvas rather than each spawning their own — cleaner hierarchy, no duplicated UI
- Dismissing a clue resumes time and returns control to the player

## The Time Loop
- `TimeController.cs`: the loop ends by reloading the active scene — all state resets so that there is no need for save files
- Coordinating the explosion sound: it fires 5 seconds before the loop ends `Time.timeSinceLevelLoad >= loopDuration - 5` so that it fits the audio format
- The screen-fade transition animator trigger before the scene reload so it doesn't feel abrupt
- `RespawnOnDomeHit.cs`: if the player falls off the map boundary, `EndTimeEarly()` is called — the loop resets immediately

## The Clock Object
- `ClockController.cs` sets 9 rotation states for clock, interpolating with `Slerp` that hides a clue for the game puzzle
- getting the index math right (`(int)(timeSinceLevelLoad / 10) % 9`) and making sure it doesn't snap between states

## Sliding Doors
- `SlidingDoor.cs`: proximity-based trigger, moves toward open/closed position with `MoveTowards` and slides it to the side, due to how object is positioned it needs the towards, and plays a sound on state change
- the distance check uses the door's original `closedPosition`, not `transform.position` — otherwise the door moves every frame and will be stuck halfway if player stops moving

## State of Game
- The loop ticks, clues can be read, doors open, leaving the map kills you, do not do it
- Goal for the next milestone: a full playthrough from launch screen to ending