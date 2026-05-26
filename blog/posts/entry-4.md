# Timeless — Dev Update #2

## Intro
The primary goal for this part was implementing the interaction system and getting the central time loop to work right. We needed to turn our walking simulator into a playable environment where you can actually solve puzzles. 

## Interaction System
We built the interaction framework around an `IInteractable` interface requiring `Interact()` and `Dismiss()` methods. We chose this over hardcoding the interaction logic per object to keep the architecture of  the game clean. Any object in the scene can become interactive simply by implementing the interface into its script, making adding future props much easier. We also need to change the object's layer to "Interactable" so the SphereCasts can properly detect it. `PlayerInteraction.cs` handles this on the player's side, checking the environment every frame and highlighting targets near the player. When we press the "interact" button from our input system, the PlayerInteraction script sends an `Interact()` action to the highlighted object.

## SphereCast vs Raycast
Originally, we used a standard Raycast for the interaction scanner, but aiming precisely at small objects felt a bit imprecise without a crosshair (and we wanted to go with minimal UI for the game). We swapped it to a `SphereCast`, which projects a thick cylinder forward for a much more forgiving detection area. To give clear feedback on what is being targeted, we added the QuickOutline asset from the asset store. When the cast hits an interactable object, the script adds and enables the outline component at runtime.

## The Clues
We implemented `Clue.cs` using the new `IInteractable` interface. Picking up a note pauses the game (`Time.timeScale = 0`) and brings up a 2D UI with text and an optional image. Rather than having every clue spawn its own UI elements, we used a shared static canvas pattern. All instances reuse one canvas, preventing too many components accumulating in the hierarchy. Calling `Dismiss()` hides the canvas and resumes time.

## The Time Loop
The core loop logic lives in `TimeController.cs`. We end the loop by reloading the active scene, guaranteeing a clean state reset without complex save logic. We synced the audio by triggering an explosion sound five seconds before the loop ends (`Time.timeSinceLevelLoad >= loopDuration - 5`). We also tied an animator trigger to a screen-fade right before reloading so the transition does not feel visually abrupt. `RespawnOnDomeHit.cs` sits on the map boundary—falling off calls `EndTimeEarly()` to reset immediately.

## The Clock Object
The main puzzle (for now) involves a clock controlled by `ClockController.cs`. It defines 9 specific rotation states (which needed to be found manually), using `Quaternion.Slerp` to smooth transitions between them. One of these rotation states eventually reveals a hidden compartment and a clue.

## Sliding Doors
`SlidingDoor.cs` handles proximity triggers, using `Vector3.MoveTowards` to slide doors sideways. We needed to fix an issue with them at some point: distance check on the door's original `closedPosition` rather than its `transform.position`. Using the current position meant the reference point moved every frame, causing the door to stall halfway if the player stopped walking. Audio plays on state changes.

## State of Game
The loop ticks down automatically, clues work, doors open correctly, and leaving the map kills you. The goal for the next milestone is a full playthrough from the launch screen to an ending.