#version 330

in vec3 vPosition;
in vec3 vColor;
in vec2 vTexture;

out vec4 color;
out vec2 f_texcoord;

uniform mat4 modelview;

void
main()
{
    gl_Position = modelview * vec4(vPosition, 1.0);
	
    f_texcoord = vTexture;
	color = vec4( vColor, 1.0);
}