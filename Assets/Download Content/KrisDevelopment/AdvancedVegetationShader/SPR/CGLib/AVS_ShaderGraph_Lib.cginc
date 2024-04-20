#ifndef AVS_SHADERGRAPH_LIB
#define AVS_SHADERGRAPH_LIB

void ApplyNoiseToWorldPosition_half(half3 worldPos, half originDistance, half originWeight, half dirX, half dirZ, half2 noise, out half3 output)
{
	half3 result = worldPos;
	half distanceFactor = lerp(1, originDistance, originWeight);

	result += half3(noise.x * dirX, 0, noise.y * dirZ) / 1.4 * distanceFactor;
	 
	output = result;
}

#endif