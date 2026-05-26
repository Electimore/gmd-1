# Timeless — Dev Update #3

## Intro
The goal for this milestone was to finally combine our standalone systems into an actual complete playthrough. We needed to wrap the core game in proper menus, add audio, hook up the ending sequence, and get our animal companion working in the scene _(petting the Cat is the most important feature)_.

## Menus and Scene Flow
We made `MainMenu.cs` and `PauseMenu.cs` to handle the basic scene switching. The pause menu was a bit problematic: freezing time with `Time.timeScale = 0` is needed for the time to not reset, but completely breaks the Input System callbacks. We had to set the UI inputs to update on unscaled time so players wouldn't get softlocked in a paused game. We also set up `IntroManager.cs`, which runs after we press the "Start Game" button in the main menu. It shows a disclaimer, runs an intro sequence featuring some text and an audio clip, and then loads you into the main game.

## Audio
We wrote `AudioManager.cs` to handle the background music. To make loading into the scene feel a bit more natural, we used a two-phase coroutine. When the level starts, the music fades all the way up to 100%, and then slowly fades back down to a target volume of 0.1. It gives the start of the loop a nice impact before settling into background noise. We kept the `AudioManager` in the main scene instead of making it a global object with do not destory on load, so it can reset every time the time loop restarts.

## Cat _(the most important feature)_
We added the cat using `PetAnimal.cs`. It relies on a `NavMeshAgent` to move around and an `Animator` for the walk and idle states. It has three modes: 
 - Wandering (picking a random spot, walking there, and waiting)
 - Noticing the player (stopping and turning to face you if you get close)
 - Following the player after being pet
Since the script uses our `IInteractable` interface, interacting with it plays a petting animation and makes the cat start following you. 

The tricky part was getting these states to switch cleanly. We had to make sure to call `agent.ResetPath()` right before the pet animation starts. If we didn't, the NavMesh agent kept trying to push the model forward while the animation forced it to stand still, which looked pretty broken. We also added footstep audio that plays from an AudioSource whenever the cat moves around.

## Win Condition and End Screen
To handle beating the game, we made `EndScreenController.cs`, which also uses `IInteractable`. The device you have to disarm is treated just like any other interactable object. Clicking it plays a cutscene using a `VideoPlayer` component. We wait for the `loopPointReached` callback (which triggers when the video finishes) and then swap the UI to the end screen. A really important step here was manually unlocking the cursor and making it visible again. Because the game locks the cursor during play, forgetting this meant you couldn't click the "Main Menu" button to actually leave the game.

## What Got Done and What Not
So far, we have first-person controls, the interaction system, working clues, the time loop, the clock puzzle, sliding doors, menus, audio, the cat, and the ending. As for what's left to do: adding NPCs with a dialogue system, creating a complete puzzle chain, and adding the "tool" mechanic.