#version 330
layout (location = 0) in vec3 inPos;
layout (location = 6) in vec2 inUV0;

uniform mat4 inMatM;
uniform mat4 inMatV;
uniform mat4 inMatP;

smooth out vec2 outUV0;
void main()
{
	outUV0 = inUV0;
	
	gl_Position = inMatP*inMatV*inMatM*vec4(inPos, 1.0);			// Clip-space vert for geo/rasterize
}
