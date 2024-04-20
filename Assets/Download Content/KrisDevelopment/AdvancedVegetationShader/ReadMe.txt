Created by Hristo Ivanov (ASP: KrisDevelopment)

This foliage shader package is designed to help you better animate vegetation in your scenes.

GUIDE:

Built-In Rendering Pipeline Shader:
	Albedo Texture - self explanatory
	Normal Map Texture - also self explanatory
	Culling Mode - Select from one of 3 culling modes
	A2C - Alpha to Coverage (or Alpha to Mask). Helps with aliasing in some cases, however does not work well with real-time shadows.
	Normal intensity slider - determine the strength of the normal map
	Cutoff - self explanatory
	Smoothness - the roughness/smoothness of the surface
	Metallic - how metallic the surface is
	Speed - the speed of the wind effect
	Amount - the distance between the sine waves, or simply put, the amount of varied movement the surface makes
	Distance - the strength of the wind, or it's ability to bend the surface
	Z Motion - by default the wind is applied to the global X axis, this applies it to the Z axis as well
	Z Motion Speed - how quickly the wind changes direction from X to Z axis
	Origin weight - it allows to prevent any unwanted movement at the base of the mesh (it's based on the height distance from the origin)

SRP Shader:
	Albedo Texture - self explanatory
	Normal Map Texture - also self explanatory
	Culling Mode - Select from one of 3 culling modes
	Normal intensity slider - determine the strength of the normal map
	Cutoff - self explanatory
	Smoothness - the roughness/smoothness of the surface
	Metallic - how metallic the surface is
	Speed - the speed of the wind effect
	Amount - the distance between the sine waves, or simply put, the amount of varied movement the surface makes
	Distance - the strength of the wind, or it's ability to bend the surface
	Direction Bias X - desired amount of movement on the world X axis
	Direction Bias Z - desired amount of movement on the world Z axis
	Origin weight - it allows to prevent any unwanted movement at the base of the mesh (it's based on the height distance from the origin)

Character Interaction:
	Add the character interaction script to the vegetation mesh. Then link the character transform and set the radius.
	Make sure your material has "Character interaction" set to the desired valie (0 will appear as no character onteraction).
	You can programatically set values to the AVS_CharacterInteraction component via the SetCharacter method.

For support and questions: krisdevmail@gmail.com