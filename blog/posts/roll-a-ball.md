# First project on the crouse - the Unity Roll a Ball tutorial

We're supposed to write some blog posts for the GMD-1 course at VIA. I'm not exactly sure what to write here. but here we go.

### Small project - [Roll-a-Ball](https://learn.unity.com/project/roll-a-ball)

During the first class, we were supposed to get accustomed to the Unity. A good way to do that was one of the starting Unity projects called "Roll-a-Ball", which goes through all of the basics that are needed to comfortably get around Unity. I downloaded the Unity Hub app, the right version of the editor and got to setting up Visual Studio Code to work well with the engine. I used Unity before, so this part was quite quick for me - the most challenging bit was finding the little details that got changed when going to Unity 6.0

### Part 1: Setting up the game

Nothing too complicated. Creating the project, making the first scene, setting up objects on it. Then the Game Objects: the ground, the player, walls, giving them all materials so they can have some colors. I also added some emissions to the materials so they glow a little :>

### Part 2: Moving the player

That's where we started to see the first components and scripts. The player got its Rigidbody to make them react to the game's physics, as well as a Player Input, which (as far as I know) acts as the newest iteration of Unity's input systems - for the game to be able to get the various button presses from the user(s). Then came our first script - PlayerController. For now, all it's gonna do is to... well, roll our little ball. We did that by applying physical force to the player (and multiplying that by speed so we won't roll at a terribly slow rate). 

### Part 3, 4 and 5: Camera Movement, Play Area and Collectibles

In the 3rd step we set up the camera with another script. We make it reference the player and essentially "glue" its `transform.position` component to the camera (without changing its `transform.rotation`, as our ball... well, *rolls* - and the camera would roll along with it). The 4th part was all about preparing the "environemnt" the player can "explore". In my case, that was the 4 walls around the plane and the 4 rocks on the plane. Finally, there were the collectibles. The tutorial wanted us to just place them around, and tie the win condition for the game to them. This is where I decided to slightly deviate from the tutorial and went for an infinite gameplay loop. First, I made the prefab (as required) and made the cubes rotate themselves. Then, I made an empty game object, put it in the center of the scene and gave it a script to instantiate cubes:  
```
public void SpawnCube()
{
    var spawnPosition = new Vector3(Random.Range(-rangeX, rangeX), 0.5f, Random.Range(-rangeY, rangeY));
    var spawnRotation = new Quaternion(45, 0, 45, 0);
    Instantiate(pickup, spawnPosition, spawnRotation);
    spawnerTimer = 0;
}
```

> ##### Explained: 
> We create a random position for the new collectible cube, and a fixed rotation for all cubes (they'll start rotating once they appear).  
> Then, we use the `pickup` prefab and pass that into an Instantiate method - by giving it its new position and rotation.  
> Finally we reset the timer, so the next collectible can spawn when the time passes.  

They are spawned in every `delayBetweenSpawns` seconds (5s by default):
```
void Update()
{
    spawnerTimer += Time.deltaTime;

    if (spawnerTimer >= delayBetweenSpawns && gameRunning){
        SpawnCube();
    }
}
```
> ##### Explained: 
> Each game tick, the game will add the time delta between the last 2 frames to the variable, and once that goes over 5s, it'll spawn a new cube and zero out the timer.  
> This also gave me some headaches with the little collectible cubes spawning inside walls or rocks, so I made a crude (but functional) fix for it - as soon as the cube spawns, it checks if it collides with anything.  
> If that thing has the `Terrain` tag, it spawns a new cube and destroys itself, as you can see here: 
> ```
> void OnTriggerStay(Collider other)
> {
>    if (other.tag == "Terrain")
>    {
>        GameObject.Find("PickupableSpawner").GetComponent<Spawner>().SpawnCube();
>        Debug.Log("Collectible spawned in a wall. Respawning...");
>        Destroy(this.gameObject);
>    }
> }
> ```
> In-game, you can sometimes notice that one frame the cubes are not where they're supposed to be (if they'd clip through a wall, for example), but for that simple project I didn't think it was something that I really had to fix - I wanted to transition to the next part of the course, which is making our own games. 

### Part 6 and 7: Collisions and Scoring

Those parts focused on two things: checking if we made touched a collectible cube, and adding points to a counter in the top-left corner of the screen:  
![A screenshot showing a blue counter (currently showing "0")](images/couter-screenshot.png)
That's our overall score. The more cubes we pick up, the higher our score. 

### Part 8: Enemies and Win/Lose Condition

The enemy AI system was something I don't think I ever did in Unity before. It is a very simple system to learn (or at least to learn the basics of). I baked the navmesh on top of the plane, which automatically excluded the parts of the terrain the enemy would collide with. Then the enemy was given a simple script and... yea. That's about it. Easy-peasy. It spawns in and follows the player indefinitely. 

### Part 9: Building the game

The last part of the tutorial. This is the first time I managed to actually make a valid WebGL build for a game, and it wasn't as bad as I thought it was... The whole process just required me to checking a few boxes (like keeping compatibility with browsers that can't handle the default compression algorithm) and that was that. It took its time and I had a working game!

### Part 10: *~~(you just lost)~~* The Game

The game itself is very simple. There's no main or pause menu. As soon as it loads, you're thrown into the game:
![A screenshot showing the initial state of the game. There's the player (a blue ball), the enemy (a red cube), and the terrain.](images/gameplay-start-screenshot.png)  
You can use the **W/A/S/D keys or arrows to move around** (a controller *should* also work, but I didn't test that). Your first and main objective is to *survive*. The second one is optional: collect as many glowing boxes as you can while you stay alive. They spawn every 5 seconds and will keep spawning until you lose - that is, hit the enemy. You can also use the **space** key to "dash" - get a speed boost in the direction of the currently held arrow/key.

The game can resolve in one of two states:
1. You *"win"* by simply never loosing - by dropping out of the map.
2. You *"lose"* by getting hit by the enemy. The game will print out how many points you managed to collect like so:
    ![A screenshot of the game showing the end screen. There's a counter showing 69 points](images/game-over-screenshot.png)

My Roll-a-Ball game can be played right [**here**](demos/roll-a-ball-webgl-build/index.html)

Hope you like it ^^