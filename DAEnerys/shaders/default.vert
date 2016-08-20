#version 330
layout (location = 0) in vec3 inPos;
// layout (location = 1) in vec3 inNorm;
// layout (location = 2) in vec3 inTan;
// layout (location = 3) in vec3 inBiNorm;
layout (location = 6) in vec2 inUV0;
layout (location = 7) in vec2 inUV1;

uniform mat4 inMatM;
uniform mat4 inMatV;
uniform mat4 inMatP;

smooth out vec2 outUV0;
smooth out vec2 outUV1;
// smooth out vec3 outNorm;
// smooth out vec3 outTan;
// smooth out vec3 outBiNorm;
// smooth out vec3 outPos_W;

void main()
{
	vec4 posW4 = inMatM*vec4(inPos, 1.0);
	//outPos_W = posW4.xyz;
	
	// outNorm = inNorm;
	// outTan = inTan;
	// outBiNorm = inBiNorm;
	outUV0 = inUV0;
	outUV1 = inUV1;
	
	gl_Position = inMatP*inMatV*posW4;			// Clip-space vert for geo/rasterize
}
