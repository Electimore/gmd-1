# Timeless — Dev Update #3

## Intro
- milestone: a complete playthrough

## Menus and Scene Flow
- `MainMenu.cs` and `PauseMenu.cs`: standard scene management, but the pause menu required freezing time (`Time.timeScale = 0`) without breaking the Input System callbacks
- `IntroManager.cs`: disclaimer screen → coroutine-driven intro sequence synced to an audio clip → scene load into the main game

## Audio
- `AudioManager.cs`: music fades in on scene load and then settles at a lower ambient target volume
- Two-phase coroutine: fade up to 100% first, then fade back down to the target level of 0.1 — gives the arrival into the scene a bit of oomph before settling into background ambience
- `AudioManager` needs to be in the right scene so it reinitializes cleanly each loop

## Cat (the most important feature)
- `PetAnimal.cs` — has `NavMeshAgent` for movement and `Animator` for walk/idle states
- Three behavior modes:
  - **Wandering**: picks a random destination within a radius, waits, picks another
  - **Noticing the player**: stops wandering, rotates to face the player when they get close
  - **Following**: after being petted, follows the player for a bit
- Implements `IInteractable` — petting triggers a coroutine (`PetRoutine`) that plays the animation
- Technical challenge: cleanly transitioning between AI states — particularly calling `agent.ResetPath()` before the pet animation plays, otherwise the NavMesh agent keeps trying to path while the animation runs and the two fight each other
- Footstep audio: plays a clip from the `AudioSource` while `agent.velocity.magnitude > 0.1f`

## Win Condition and End Screen
- `EndScreenController.cs` also implements `IInteractable` — the "device" the player must disarm is just another object in the world
- Interacting with it plays a `VideoPlayer` cutscene; the `loopPointReached` callback fires when it finishes and swaps to the end screen UI
- Returns the cursor and loads the main menu — important to unlock the cursor since the game runs with `Cursor.lockState = CursorLockMode.Locked` during play

## What Got Done and What Not
- Done: first-person controls, interaction system, clues, time loop, clock, sliding doors, menus, audio, animal companion, end screen
- To do: NPCs with dialogue system, a complete puzzle chain, the "tool" mechanic,