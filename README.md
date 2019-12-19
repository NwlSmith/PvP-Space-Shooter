Date: 12/18/2019

**SPACE WAR!** was a solo project of mine that I took on as an exercise in refinement;
I wanted to create a game that showcased my programming prowess and ability to produce a fully fleshed-out and functional experience,
while focusing less on the actual gameplay mechanics, and more on the interfaces and interactions.
Admittedly, the gameplay is not particularly engaging, but that was the choice I made so I could focus on creating a polished product.

I had a lot of code that needed to perform similar functions, so I decided to break up my code into small classes.
While there are a good deal of files in this project, many of them have very little code.

One thing of note to this project is that I wanted to prototype and test a large variety of UI options and control schemes.
To accomplish this, I made each MenuController find its own UI elements rather than dragging and dropping them individually.
As a result, Canvas objects are easily interchangeable and UI elements are easily added and removed.

	- To see this in action, I would recommend looking at the MenuController and OptionsController classes, located in Scripts -> Utilities.

One of my future goals for this project is to create a game where the control scheme changes at certain intervals.
To accomplish this, I separated the movement, shooting, and central control classes so I could modify each.
There are currently three control schemes (located in Scripts -> Gameplay -> Player -> Movement):

	- PlayerMovementHorizontal: the one in the "Game" scene where left and right pivot the player horizontally).

	- PlayerMovementTank: the one in "1PlayerAsteroidsTank" scene where left and right rotate the player and forward propels it forward.

	- PlayerMovementJoystick: the one in "1PlayerAsteroids" scene the player is controlled with a virtual joystick.
