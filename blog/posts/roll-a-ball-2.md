# Roll-a-Ball

The process of making Roll-a-Ball was pretty fun and easy, Unity tutorials are a nice starting point.

### Physics Engine: FixedUpdate vs. Update
In Unity Update() runs once per frame and FixedUpdate() runs on a fixed timer. Since it is applying forces to a Rigidbody component, calculating movement in FixedUpdate ensures that the ball’s physics don’t depend on FPS.

### Collisions: OnTriggerEnter vs. OnCollisionEnter
Triggers (by checking the "Is Trigger" box on the Collider) and the OnTriggerEnter function. This is a performance and gameplay choice. Using a Trigger tells Unity’s physics engine not to calculate a physical "rebound" force, which saves processing power. It’s the standard way to handle items that should be "collected" rather than "hit."

### Camera Offset
Calculating a Vector3 offset in the Start() method and applying it in LateUpdate(). If the camera were a child of the ball, it would rotate as the ball rolls, which not ideal. By using a script and LateUpdate(), the camera calculates its position after the player has moved for that frame, resulting in a stable third-person point of view.

### Prefabs & Tags
Using a Prefab for the "Pick Up" objects and a Tag to identify them in code. Prefabs allow for efficient updating objects. Using Tags (e.g., if (other.gameObject.CompareTag("Pick Up"))) is also much more efficient than checking the name of every object the ball touches.

### Extra
The tutorial is a good baseline but it was missing some things. The ball would escape the plane after hitting a wall with enough speed. I fixed it by adding invisible walls - I duplicated all the normal walls, made them taller and turned off the mesh renderer while leaving the box colliders on.