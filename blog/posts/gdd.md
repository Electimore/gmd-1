# Game Design Document - Timeless

This document is supposed to outline the concept of the game that'll be my final project for the GMD-1 course.

## Hight Level Concept/Design

### Working Title

I decided to call it "Timeless" due to it taking place in (well preserved) ruins, as well as the time loop that will be occuring as the core gameplay loop (literally)

### Concept Statement

The game will take place on an alien planet, on an ancient dig site/ruin-museum. You are a tourist that's there for holidays. Because of an unforseen event and pure bad luck, you trigger an emergency system that will evaporate everything within a 10km radius. You have 15 minutes to find, decipher and disarm the device through a mix of exploration and puzzles. 

### Genres

Puzzle, exploration, first-person camera, 3D

### Target audience

People that would like to solve a bigger mystery, but don't have enough time for longer play sessions. The entire game will be played in 15-minute (or shorter) chunks.  
Other than that, Timeless is designed to be played without a game save file to better accomodate the VIA Arcade Machine - so many different people can play the game wihout juggling multiple saves.  
Most likely people aged 13+, and for now I'm planning only English localization.

### Unique Selling Points

What makes it stand out is the unique "you are the save file" approach - the game will be fully beatable within a single cycle, as long as you know what you're doing. Once you solve it, that's it. It will be intended to be played and experienced only once - which will contrast with most modern games aiming for infinite replayablility. My main inspiration to make this game is a game called "Outer Wilds". I loved its combination of puzzles and exploration. I know I cannot do it justice and make something similar in scope over three months, but I want to try.

## Product Design

### Player Experience and Game POV

The player is a tourist in a futuristic museum built amongst alien ruins. They will not be human - the entire setting will be set in space, on a distant planet, without humanity in the picture. I want the player to feel curious about the exhibits and to challenge what lies in the various rooms and buildings of the museum. The player should be kept engaged by exploration at first, and about the mystery once all the events are set in place.

### Visual and Audio Style

The game will most likely be made out of low- or medium-poly 3D assets, and I'd like to try making a couple of them (even though I am fully aware that using the Asset Store will be unavoidable)
[Insert some images that picture the vibe I'm going for here].  
The vibe should be alien, retrofitted for our character's species.

### Game World Fiction

Worldbuilding status - W.I.P.

### Monetization

It would be nice to publish Timeless on a platform such as Steam as a <10 Eur one-time purchase (price depending on the total length of the game). No microtransations, no free-to-play with ads or premium subscriptions. Otherwise completely free-to-play with no monetization. It's a university project after all :>

### Platform(s), Technology and Scope

Timeless will be available for PC with a native Windows and Linux builds, as well as a working WebGL build. It will be optimized to run on lower-end hardware, and require minimal input (such as the VIA Arcade Machine). It'll be made in the Unity Game Engine. I'm planning to spend around 70h and make the game alone, unless another student joins me (then the time gets bumped to 140h across two people). First playable prototype should be available mid-April, and the full game should be completed by May 2026-05-26 (with smaller patches possible later on).  
Major risks:
- *scope creep*💀
- lack of time due to other classes

## Detailed Game & Systems Design

### Core Loops

- The aformentioned time loop keeping the plot going
- Exploration -> Gathering Informations -> Solving small puzzles -> More Exploration -> ...

I personally love this kind of gameplay, as it makes you focus on the world of the game, and I hope more people will find it engaging as well. The player's goals are to find and disable the *plot device*. The only elements they need to do that are the informations about its position and usage - all of that can be obtained through the main gameplay loop. Puzzles are there to keep things interesting and players engaged through minor challenges. 

### Objectives and Progression

The entire world of the game will be fully explorable from the moment the player first launches the game. It will be fully possible to finish the game in the first time loop, without ever doing all of the puzzles. The idea is, it should be *highly* unlikely for the player to do so, as that will require knowledge they will not posess - which buttons to push, which places to go, in which order - and so on.  
The player's goals are simple:
- Find the device
- Disable the device

### Game Systems

- A fixed timeline of events - unless interrupted by the player, all events in each time loop iteration will remain the same. NPC movements, seemingly "random" events, etc...
- Player/Camera controls
- Basic interactions with the environment
- System for dialogues with NPCs
- I'm planning to incorporate some kind of a tool into all this, but not sure what exactly [W.I.P.]
- Petting system for a dog/cat/local equivalent (this is the most important one!)

### Interactivity

??? I'll figure it out™

## Stay tuned!

This documents is in dire need of additional content. Also, if you're one of the other students from the course and would like to join me in making of this game, shoot me a message on my discord: [electimore](https://discord.com/users/549612764977561602)