[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-f059dc9a6f8d3a56e377f745f24479a46679e63a5d9fe6f495e02850cd0d8118.svg)](https://classroom.github.com/online_ide?assignment_repo_id=454330&assignment_repo_type=GroupAssignmentRepo)


**The University of Melbourne**
# COMP30019 – Graphics and Interaction

Final Electronic Submission (project): **4pm, November 1**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, October 17**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).


**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully.

- [x] Brief explanation of the game.

- [x] How to use it (especially the user interface aspects).

- [x] How you designed objects and entities.

- [x] How you handled the graphics pipeline and camera motion.

- [x] The procedural generation technique and/or algorithm used, including a high level description of the implementation details.

- [x] Descriptions of how the custom shaders work (and which two should be marked).

- [x] A description of the particle system you wish to be marked and how to locate it in your Unity project.

- [x] Description of the querying and observational methods used, including a description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [x] Document the changes made to your game based on the information collected during the evaluation.

- [x] References and external resources that you used.

- [x] A description of the contributions made by each member of the group.

## Table of contents
* [Brief explanation of the game](#Brief-explanation-of-the-game)
* [How to use it](#How-to-use-it)
* [How you designed objects and entities](#How-you-designed-objects-and-entities)
* [How you handled the graphics pipeline and camera motion](#How-you-handled-the-graphics-pipeline-and-camera-motion)
* [A description of the particle system](#A-description-of-the-particle-system)
* [Description of the querying and observational methods](#Description-of-the-querying-and-observational-methods)
* [Changes made to your game based on the information collected during the evaluation](#Changes-made-to-our-game-based-on-the-information-collected-during-the-evaluation)
* [Procedural generation](#Procedural-generation)
* [Shader description](#Shader-description)
* [References and external resources](#References-and-external-resources)
* [Team Members](#team-members)
* [Technologies](#technologies)


## Brief explanation of the game.
The game is a 2D side scroller made using 3d models. The goal of the game is for the player to reach the teleporter at the end of each level and complete the game with as little death as possible. With each progressing level, it introduced more threats and complexity so that later on players will be faced with levels that take a lot of attempts to complete. 

## How to use it (especially the user interface aspects).

Players can start the game, select the game level or quit the game through the main menu. In the UI of the level, you can see how many times the player has died so far at the upper left corner of the page. When it comes to the level where items need to be collected, the UI will prompt the player how many items need to be collected.
The button in the upper right corner can pause the game and choose whether to return to the main menu, restart the game or continue the game. After passing all the levels, the player can choose whether to return to the main menu or quit the game directly.
In the first level or when encountering a new game mechanism, the player can see the detailed operation introduction on the game background.

## How you designed objects and entities.

As our game is a 2d side scroller, the functionality of the objects and entities can simply be inspired by other great 2d side scrollers and all we have to do is to decide which part from what games do we want into our games. 

For our player control, we decide to include the functionality of double jump, wall climbing and dashing. While dashing isn’t commonly used in 2d side scrollers, we think it's a good fit as it opens doors for more complex level design and movement controls. 

For enemies, we decide to include enemies that shoot from the current position or seek projectiles that are harder to dodge. As well enemies that kill the player if they touch you. 

We also implemented some entities that have been commonly seen from other games such as jumping blocks, moving platforms, blocks that stomps you if you go beneath it etc. 




## How you handled the graphics pipeline and camera motion.
For the graphic pipeline, we did not have to worry about the graphic pipeline . For camera motion the camera first keeptrack of the offset between the player and whenever the player moves from the x and y position the camera mimics the movement. 


## A description of the particle system 
###  A description of the particle system you wish to be marked and how to locate it in your Unity project.

The missile smoke effect is located within asset/prefab/Rocket smoke effect

The smoke effect is a trailing effect that is attached to the missile. The trailing effect can be made by stopping particles emit over time but rather emit based on distance traveled. Other changes are accompanied so that the effect has some resemblance to missile smoke. Such as the smoke shape, colour over time (orange to grey to invisible) and a linear velocity to simulate the spread of the smoke over time. 

## Description of the querying and observational methods used
### Description of the querying and observational methods used, including a description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

We observed 5 people, all of them are male, 1 of which is a casual gamer, 3 are frequent gamer, and the other 1 is a gaming expert. We use the observational method of cooperative evaluations, we ask the participants to play the game and we encourage them to actively criticise the game and propose any changes they want to make in the game. And we as the experimenter would respond to their complaints and suggestions and provide alternative solutions to the problem and ask for their opinion, likewise, the participants would ask for clarifications when problems arise, to check if the situation is intentionally created by us or if it is a bug. In the meantime, we will record the conversation in text.

We also queried 5 people, 4 of them are male 1 of them is female, 3 of which are frequent gamers, 1 of which is casual gamer and the other 1 only player mobile game. We ask them to play the game, then we give them the questionnaire and ask them to answer, 7 of the questions are closed questions, and 1 open question. 

The questionnaire and the raw feedback is too long to present here, so here is a [link to feedback document](feedback.docx)
Here I will summarize the feedback and observations:
Other than the obvious bugs, most players found different types of block difficult to distinguish(regular blocks, trap blocks, climbable blocks), and they like to have the up key for the jump key, and feels the difficulty curve is a bit steep, and some participants doesn’t like the visual, and they would like to know the plot for the game.


## Changes made to your game based on the information collected during the evaluation
### Document the changes made to your game based on the information collected during the evaluation.
Not all of the participant’s suggestions were taken into consideration but below are some notable changes that were implemented which are either directly observed or suggested by the participants. 

Observation: Most players were not able to recognise which tiles are you able to perform wall jumps and which cannot.Previously the only distinction is that the tile you were able to wall jump had a black lining on the top. (to be specific the 2 tiles are at external asset/ AurynSky/ Dungeon pack/prefab/General/BlockS01D and BlockS02D). Most players at the start didn’t even know tiles are jumpable before we told them so.

Implementation: at some cost of visual consistency, change the wall jumping tiles to be much darker than the non-jumpable tiles, as well as providing further explanation in UI on which tiles you can perform wall jump and how to do so.


Observation: With the last 2 level’s paths not being linear, players are unable to navigate around the level as they don’t know where to go. Additionally, entities which shoot the projectile are often out of sight from the player's vision making it very difficult to dodge.

Implementation: significantly increase the player vision(moving the camera way back) for the ladder levels.


Observation: Players cannot correlate the gem as a key to unlock doors, and since gems are not always on the screen, players can get stuck thinking that the level is flawed. 

Implementation: Due to the lack of free assets which resemble keys. The solution is to implement a UI that tells the player gems are keys and to show on the screen how many gems in the levels that need to be collected.


Feedback: Some participants were not happy about the pure visual element with the game. Their problems are 1. The terrain generation can be quite distraction or out of place(stuck inside the tiles, and the texture with the terrain does not fit with the lava surrounding. 2. The ending of each level was marked by asset prefab/window which the participants did not like as it looks weird and you can run through it. 

Solution: For issue 1. Reworked terrain generation and set boundaries so that terrain could not spawn between the levels and only besides it. Additionally we changed the texture of the rocks to fit with the surrounding terrains. For issue2 we made the ending place into a teleporter (asset/prefab/teleporter) which looks to be a lot more natural with each level transition.


Feedback: Player feels that level 8 is too difficult,. (most people didn’t make it to level 10 due to being stuck on level8’s fire traps jumps to get the gems).

Solution: slightly reduce the collision area for fire traps in level8, reduce the speed for spike traps and spear traps across all levels. Removed traps that feel strangely out of place for the sake of adding difficulties. 

## Procedural generation
The procedural generation technique and/or algorithm used, including a high level description of the implementation details.

The main algorithm used for the procedurally generated terrain is marching square. First we create a 2 dimensional matrix with specified width and length, then we randomly assign 1 or 0 with the specified ratio to create a random noise map.

Then we use cellular automata to smooth the generated noise map, cellular automata works as we treat each 1 as a living cell and each 0 as a dead cell, then we check the surrounding of each cell, if the surrounding has less than a critical number of living cell, the cell would dead, otherwise if  the surrounding has more than a critical number of living cell, the cell would be alive, and we can iterate this process until it reaches the desirable state for the map.

Then we divide the matrix into a square grid, with every 2x2 points forming a square, and create a midpoint on each side of the square, so we will end up with a grid full of identical squares, with each square containing 8 points, each square would share points with other squares.  Then we encode each square according to the binary value of the corner of the square, since there are 4 corners on each square, the total number of encoded configurations of the square is 24 = 16. Then we triangulate the squares according to the configuration, the points with value 1 indicate this point should be land, so we would draw triangles to connect up the points with value 1, so pieces of land would be formed.

Lastly we added some random displacement both vertically and horizontally, so the terrain looks more natural.

## Shader description

### Shader1: Assets/Scripts/WaveShader.shader

The objective of this shader is to display the wave effect for the lava in the game, this is achieved by changing the coordinates of vertices of the lava plane, similar to the waving plane in the workshop. While the shader in the workshop only replaces vertex according to the x coordinates, this shader takes z-coordinates into account. 

Firstly, multiple waves on a single lava surface is desired since it is more realistic, for simplicity two waves are adopted. For each wave the wave’s moving direction, amplitude and wavelength is needed for vertex calculations, these information are specified in the properties tag.

In the vertex shader, the parameters for final vertex calculation are computed and declared as variables. Amplitude and wavelength can be accessed directly from the properties. K is the variable that takes wavelength into account and produces the wavelength that is used in the sine function. D is a directional vector containing information about how x and z components contribute to the wave calculations, and D is used as dot products to compute the sum of wave direction contributed by x and z components. F is the combination of above parameters and can be directly used in the sine and cosine functions for clarity. It combines the wavelength, x and z contributions and also the speed of the wave (controlled by wavelength and gravity) according to the time factor. Then, the new vertex is calculated, the y component is replaced and also takes amplitude into account to achieve the wave effect, x and z components are also changed to align with wave direction. Finally, the vertex is converted to screen space, and the converted vertex and uv coordinates of the lava texture are passed to the fragment shader.

In the fragment shader, since the lighting of the game is auto generated, only the colour of texture is sampled and returned. 

In deployment, amplitude of the wave is set to be very small since lava is more thick and dense than water, also large amplitude would result in the edges of lava planes being clearly visible. The scale of x and z direction is also limited to prevent two lava planes overlapping. 




### Shader2: Assets/Scripts/DissolveShader.shader

The objective of this shade is to display a fading/dissolving effect of blocks. The reason for developing such a shader is that the trap blocks (intended to be destroyed after a while when the player stands on top of it) in the game just disappear when destroyed, leaving players with confusion and unexpected difficulty. As a result, this shader does not merely produce a visual effect but also provides a visual transition for players, allowing them to be prepared and informed that the block they are standing on is going to disappear. 

The core idea of this shader is to use noise to simulate a dissolving effect of the trap blocks. In the properties tag, following properties are specified:

Burn amount - how much of the block is burning or faded away (0 being perfect still, 1 being completely destroyed)
Burn width - used to control the line width when simulating, higher burn width indicates wider spread of burning area. 
Primary and secondary colour - indicates the colour used for the burning area and its edge. 
Burn map - the noise texture used for the simulation, adopted from the same external source. 

Also, the culling is turned off in this shader because the dissolve will reveal the internal structure of the tile model, rendering the front face only will lead to incorrect results. 


The vertex shader is relatively straightforward, the position of the block remains the same, therefore the vertex is converted to the screen space straightaway. Also the uv coordinates of model texture and noise texture need to be computed and passed to the fragment shader. 

In the fragment shader, the noise texture is sampled and stored in the burn variable, and uses the result to subtract the burn amount. The result of subtraction is used in the clip function to skip all pixels with result less than 0, meaning that the pixel is dissolved/destroyed, therefore saving the computational resources. It is desired to simulate a colour change in the area bounded by the burn width, therefore a mixed coefficient t is computed using smoothstep function. When t is 1, it means that the pixel is at the edge of the dissolve, 0 means that the pixel should be the same as the original colour of the model. And then t is used in the lerp function to mix the primary and secondary colour to calculate the actual burn colour. The pow function is also used to process this result to achieve a more realistic effect. Finally, t is used again to mix the colour of the model texture to obtain the final colour of the fragment shader, the step function is used again to prevent the dissolve effect from displaying when the burn amount is 0. 

To control the burn amount according to time factor, the burn amount is obtained in TrapBlock.cs script and incremented by a fixed amount, known by burn speed, in each update. And it is calculated in the FixedUpdate to ensure a consistent increment. Since the game's obstacles are based on lava, red, fire burning colour is chosen as the primary colour to suit the style of the game. 

## References and external resources


https://www.youtube.com/watch?v=j111eKN8sJw&t=23s (variable jumping)

https://www.youtube.com/watch?v=0v_H3oOR0aU (seeking missle)

Unity Shader Getting Essentials, Authored by FENG LE LE, ISBN number: 978-7-115-42305-4 (DissolveShader)

Waves, made by Jasper Flick https://catlikecoding.com/unity/tutorials/flow/waves/ (WaveShader)

https://www.youtube.com/watch?v=yOgIncKp0BE (marching square)

https://www.youtube.com/watch?v=9lPCv9kkbSI (scene switch)

https://www.youtube.com/watch?v=YYD_tBBS4FY (UI fade)

https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html (detect button name)

https://github.com/Graphics-and-Interaction-COMP30019-2021/Workshop-5-Solution (projectile)








## Team Members

| Name | Contribution |
| :---         |     :---      |
| Zhiyuan Chen | Trap, level design, quering, movement control, particle effect, camera motion|
| Jiachen Ping | Shader, level design, obeservational evaluation, movement control|
| Haichi Long  | procedural generation, level design, obeservational evaluation      |
| Yixin Zhong  | UI, dead animation, level design, quering              |

	
## Technologies
Project is created with:
* Unity 2021.1.13f1
* Ipsum version: 2.33
* Ament library version: 999





