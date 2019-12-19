/*
 * Mechanics:
 *      - 2 players, one at top of screen one at bottom
 *      - each has 3 buttons. Up, down, and fire
 *      - player hits other player with bullet, they die, other player gets point
 *      - also, occasionally have coin appear at side of screen furthest from each player, if they get it they get another point
 *      - first player to 50 points wins
 *      - pressing up/down / left/right will push the player with a force in that direction.
 *          - Be able to charge button press?
 *          - otherwise, have it go on button down
 *      - if get to edge, the player can go out of the area but then are pushed back in
 *      - must limit velocity of player
 *      - no limit to number of bullets you can shoot, but it will be onbuttondown
 *          - could change this?
 *      - have woosh sound when bullet close to player?
 *      
 *      - Do I want the player's rotation to effect the angle of the bullet?
 *      - movement needs to be more responsive
 *      
 * Will need:
 *      - music ---Done
 *      - points indicators ---Done
 *      - pause button ---Done
 *      - main menu ---Done
 *      - need to detect device and change UI to fit. :(
 *      - camera moving with events ---Done
 *      
 *      
 *  Music:
 *  
 *          - intro screen? https://www.youtube.com/watch?v=rGc3VUbeix8
 *          - could chop up: https://www.youtube.com/watch?v=u0n1xg2RjMk
 *          
 *          - 125 bpm https://www.looperman.com/loops/detail/137059/trippie-redd-x-juice-wrld-type-drums-140bpm-trap-drum-loop goes more with the others, very slow/chill
 *          & 160 bpm https://www.looperman.com/loops/detail/136888/metro-boomin-type-beat-loop-160bpm-trap-drum-loop
 *          & 140 bpm https://www.looperman.com/loops/detail/136905/aggressive-trap-drums-by-mulaofficial-140bpm-trap-drum-loop this could be the intro? https://www.looperman.com/loops/detail/136910/aggressive-808-by-mulaofficial-140bpm-trap-drum-loop
 *          & 130 bpm https://www.looperman.com/loops/detail/136876/overdriven-808-trap-drum-130bpm-trap-drum-loop intense with this intro 140 bpm https://www.looperman.com/loops/detail/136874/go-f-k-yourself-2-140bpm-trap-drum-loop and this? https://www.looperman.com/loops/detail/136806/stay-alive-drums-146bpm-trap-drum-loop
            MULAOFFICIAL
 *
 * Camera: -- DONE
 *          - always have it swaying a little bit
 *          - on player getting hit, do a little shake
 *          - more stuff
 * Pause Menu:
 *          - Resume game -- DONE
 *          - Options -- DONE
 *          - Quit -- DONE
 * Extras:
 *  - ability to choose ship, choose color.
 * Tweaks to try:
 *  - movement
 *      - set velocity or add to velocity instead of adding force?
 *      
 *      
 * Have separate canvases? 
 * stage announcement vertical?
 * pause button delay, goes under stage change ui
 * endgame needs to be finished
 *  - say who won, blow up other player
 *  - slowly go to main menu
 *  - main menu needs title screen to shrink
 *  - explode bullets, stop movement on mainmenu, set time to 1
 *  
 *  
 *  Game where controls switch?
 *  -   could be like asteroids but have many different control schemes?
 *      1. tank controls, right moves forward and to the left, right moves forward and to the right, can press both to move forward
 *      2. joystick, have turn and move in that direction
 *      3. jave 4 directional buttons but can only press 1 at a time
 *      4. inverted joystick
 *      5. 
 *      
 *  - how to handle switching?
 *      - just different control UIs that swap in and out?
 *  - tutorial:
 *      - https://www.youtube.com/watch?v=nGw_UBJQPDY
        - https://docs.unity3d.com/Manual/HOWTO-UIMultiResolution.html
        - https://docs.unity3d.com/Manual/UIBasicLayout.html
        - https://unity3d.com/learn/tutorials/topics/mobile-touch/how-submit-ios-app-store-overview
 *      
 */