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
 *      Jordan Partridge - Nov 26, 2022: _
 */

using var game = new BeansBrewsBaristas.GameManager();
game.Run();
