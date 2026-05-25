# Timeless — Dev Update #1

## Intro
- goal for this milestone: have a playable first-person prototype in the scene

## Set Up
- Deciding on the new Input System (`InputAction` callbacks) over the legacy one — mention it's needed for controller support (VIA Arcade Machine)
- Third-party packages installed: QuickOutline, TextMesh Pro
- Scene structure plan: MainMenu, Intro, main game scene

## First-Person Controller
- Overview of `FirstPersonPlayer.cs`
- Physics-based movement: writing velocity directly to `Rigidbody.linearVelocity` instead of using `AddForce` — explain why (snappier, more predictable feel for a walking sim)
- Jumping with a ground check (`isGrounded` flag + `OnCollisionEnter`)
- Supporting mouse/keyboard AND controller simultaneously (two separate look handlers: `OnMouseX`/`OnMouseY` for mouse, `OnStickLook` + `FixedUpdate` for the right stick)

## Camera Pitch Clamping
- The problem: Unity stores Euler angles in the 0–360 range, so naively clamping the X rotation breaks at the 0/360 seam (camera snaps or locks up)
- The fix: shift the angle by +180, clamp, shift back — walk through the logic briefly

## Assets
- Importing asset packs: Low-poly Sci-Fi Pack, LOWPOLY Spaceship, Cosmos SkyDome
- Setting up the skybox to give the alien-planet atmosphere
- Early scene layout — placing geometry to match the museum concept from the GDD
- First impressions of the visual direction

## State of Game
- What works: the player moves, looks around, jumps, and the world looks vaguely like the concept
- Goal for the next milestone: make the world actually respond to the player