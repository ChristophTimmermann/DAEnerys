// EDITOR
#version 330

uniform mat4 inMatM;
uniform mat4 inMatV;
uniform mat4 inMatP;

layout (location = 0) in vec3 inPos;
layout (location = 1) in vec3 inNorm;
layout (location = 2) in vec3 inColor;
layout (location = 3) in vec2 inUV0;

smooth out vec2 outUV0;
smooth out vec3 outNorm;
smooth out vec3 outColor;
smooth out vec3 outPos_W;
smooth out vec3 outEye_W;

void main() 
{
	vec4 usePos = vec4(inPos, 1.0);

	mat4 invV = inverse(inMatV);
	vec4 posW4 = inMatM*usePos;
	outPos_W = posW4.xyz;
	outEye_W = outPos_W-invV[3].xyz;	// World-space for reflections/etc
    
	outColor = inColor;
	outNorm = inNorm;
	outUV0 = inUV0;
    
	mat4 VP = inMatP*inMatV;
	gl_Position = VP*posW4;			// Clip-space vert for geo/rasterize
}