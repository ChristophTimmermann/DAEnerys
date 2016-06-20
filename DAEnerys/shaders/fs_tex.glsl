#version 330

in vec2 f_texcoord;
in vec4 color;
out vec4 outputColor;

uniform sampler2D maintexture;
uniform bool textured;

void 
main()
{
	if(textured)
	{
		vec2 flipped_texcoord = vec2(f_texcoord.x, 1.0 - f_texcoord.y);
		outputColor = texture(maintexture, flipped_texcoord);
	}
    else
	{
		outputColor = color;
	}
}