# Timeless — Dev Update #1

## Intro
The goal for was to get a playable first-person prototype running in the scene, something we could move through, look around, and jump in, even if the map was mostly empty.

## Set Up
The first thing we made was set up input system. Since the game needs to run on the VIA Arcade Machine, which requires controller support, the new Input System was the way to go. We also installed two third-party packages: TextMesh Pro for text rendering and QuickOutline for highlighting interactable objects. We planned the scene structure as three scenes: MainMenu, Intro, a main game scene and maybe some outro if we had enough time left.

## First-Person Controller
We implemented the first-person controller in FirstPersonPlayer.cs. Movement input is read as a Vector2 from the OnMovement callback and converted to a world-space direction with transform.TransformDirection. Rather than applying force with Rigidbody.AddForce, we write the result directly to rb.linearVelocity, preserving the y-component from the existing velocity so gravity is not cancelled mid-air. Writing velocity directly makes the player start and stop in response to input, which is the right feel for a walking simulator where predictable movement matters more than physical momentum. We handle jumping in the OnJump callback, which calls rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse) and sets isGrounded to false. OnCollisionEnter resets isGrounded to true when the player lands on anything tagged "Ground".

We split camera look into two code paths. Mouse input arrives as a per-event delta, so OnMouseX and OnMouseY are callback-driven and only fire when the mouse moves. The right stick holds a continuous axis value, so OnStickLook stores the current direction and we apply the rotation every FixedUpdate, multiplied by Time.deltaTime and stickRotationSpeed to keep it smooth.

## Camera Pitch Clamping
Clamping the camera's vertical pitch introduced a specific problem. Unity's eulerAngles always returns values in the 0–360 range, so a camera tilted slightly upward might have an X angle of 350 rather than −10. A naive Clamp on that value immediately snaps the camera to the upper limit on the first frame. We fixed it by shifting the angle by +180 before clamping, then subtracting 180 afterward: (Math.Clamp((newRotation.x + 180) % 360, 92, 240) - 180) % 360. This moves the 0/360 wrap point well outside the valid pitch range so the clamp never sees the discontinuity.

## Assets
We imported three asset packs to populate the scene: the Free LowPoly SciFi Pack, the LOWPOLY Spaceship pack, and the Cosmos SkyDome. We assigned the SkyDome as the scene skybox to set the alien planet -ish atmosphere. We also placed some initial game objects to approximate the museum layout, giving it a draft intended scale and structure. The low-poly assets work perfect for for a small size project.

## State of Game
At the end of this milestone, we have a player that can move, look around, and jump, and a scene with a visual direction that matches the concept.