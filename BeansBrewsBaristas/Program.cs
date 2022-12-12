/*
 *  Program.cs | Final Project [PROG 2370]
 *  A cat cafe simulator to test your skills and reaction times!
 *  
 *  Revision History:
 *      Jordan Partridge - Nov 08, 2022: Created BBB & GameManager & Global
 *                       -             : Created Sprite, AnimatedSprite, UIElement, TextElement components
 *                       - Nov 09, 2022: Created Audio/Input/Scene Managers
 *                       -             : Created ScoreCalculator
 *           Thomas Heal -             : Added music assets
 *                       -             : Fixed InputManager's detecting OnKey/MouseDown/Up
 *                       - Nov 10, 2022: Added Customer and the option to color Sprite
 *      Jordan Partridge -             : Added Debug class to assist in development
 *                       -             : Created CustomerManager to create and destroy Customers
 *                       -             : Removed Managers from Global; transitioned to singletons
 *           Thomas Heal - Nov 15, 2022: Refactored AnimatedSprite with a working version of frameIndexes
 *                       - Nov 16, 2022: Customer is now represented by an AnimatedSprite instead of a ball texture
 *      Jordan Partridge - Nov 18, 2022: Refactored Customer and AnimatedSprite; removed debug code from GameManager
 *                       -             : Customer will stop animating when they it has finished moving to target position
 *           Thomas Heal - Nov 19, 2022: Added cafe environment art to Scene 1
 *      Jordan Partridge - Nov 26, 2022: AudioManager now supports songs and sound effects
 *           Thomas Heal - Nov 30, 2022: Added cafe environment Scene 2, updated songs to .mp3 and updated customer TravelToPos function
 *                       - Dec 01, 2022: Added TravelToPos functionality, currently grabs from Vector2 Queue
 *                       -             : Fixed TravelToPos; it works as an async task, outside of Update
 *                       -             : Added MenuComponent and Main Menu functionality
 *      Jordan Partridge -             : MenuComponent is now controlled from InputManager
 *                       -             : TravelToPos now correctly offsets Customer based on index of Queue
 *                       -             : Added skeleton for Credits and Help scenes
 *                       -             : Added placeholder Menu UI
 *                       - Dec 08, 2022: Customers now have randomly generated names
 *                       -             : Swapped Customer placeholder art with release candidates
 *                       -             : Customers now hold randomly generated orders with modifications
 *                       - Dec 09, 2022: Customer queues not offset in scene
 *                       -             : Order information now displays on screen
 *                       - Dec 10, 2022: Incomplete: Adding in-game font and controls
 *                       -             : Controls are displayed - Order is user friendly
 *           Thomas Heal - Dec 10, 2022: Updated Hierarchy to new folder structure
 *                       -             : Cafe3 Background and bar added
 *       Jordan & Thomas - Dec 11, 2022: Finalization and documentation of project     
 *
 */

using var game = new BeansBrewsBaristas.Managers.GameManager();
game.Run();