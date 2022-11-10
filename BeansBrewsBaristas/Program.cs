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
 */

using var game = new BeansBrewsBaristas.GameManager();
game.Run();
