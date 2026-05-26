# Timeless — The Final Game

## Intro
For anyone just catching up, *Timeless* is a first-person puzzle and exploration game set inside an alien museum. You are trapped in a 99-second (TBD, 99 seconds are here just for the demo) time loop. The core idea is that you aren't grinding stats or unlocking gear—the game is designed to be beaten in a single run purely through the knowledge you gain over multiple loops. 

## Play It Here
- [WebGL Build](https://electimore.github.io/gmd-1/demos/timeless-webgl-build/Timeless/index.html)
- [Universal Windows Platform Build](https://github.com/Electimore/gmd-1/blob/main/builds/uwp/Timeless.zip)
- Linux Build (Coming Soon)

If you're playing on a keyboard, it's the standard setup: WASD and mouse to move and look around, E to interact, Space to jump, and Escape to pause. We also hooked up full controller support (an Xbox controller works perfectly for testing), which was a strict requirement to make sure the game is compatible with the university's VIA arcade machine.

## A Look Inside
Here is how the final build came together visually:

![Main Menu](images/mainmenu.png)
![First look around the museum](images/museumentrance.png)
![One of the clues](images/clue.png)
![The CAT!!!](images/cat.png)
![Barnyan Clock](images/clock.png)
![Moving Doors](images/doors.png)

## What Got Built
To wrap everything up, here is a quick rundown of the systems we successfully integrated for this slice:
* A physics-based first-person controller with jumping.
* A clean `IInteractable` system for examining objects.
* Dynamic UI canvases for picking up and reading clues.
* A functioning 15-minute time loop that cleanly resets the active scene.
* The main rotational clock puzzle.
* Proximity-based sliding doors.
* Menu systems that safely pause the game loop.
* An ambient audio system and 3D sound effects.
* Our AI cat companion that wanders, faces you, and follows you when petted.
* The final video cutscene and win condition logic.

## What Didn't Make It
Deadlines are what they are, and we had to cut a few things to get a stable build out the door. We dropped the full NPC dialogue system, a longer, more elaborate puzzle chain, and the dedicated "tool" mechanic we originally planned. 

## Lessons Learned
Technically, leaning heavily on the IInteractable interface was a massive win. It kept our codebase incredibly clean and meant we didn't have to write custom spaghetti code every time we wanted the player to touch a new object. We also learned a lot about physics optimization along the way, like switching from rb.MovePosition to directly modifying linearVelocity to finally stop the player from glitching through walls. 

On the design side, committing strictly to a "no save files" constraint really shaped the entire game. It forced us to rely completely on the player's memory and knowledge carrying over between loops, which ultimately made the time loop mechanic feel much more meaningful.
