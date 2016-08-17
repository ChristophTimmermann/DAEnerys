// EDITOR
#version 330

uniform mat4 inMatM;

smooth in vec2 outUV0;
smooth in vec3 outNorm;
smooth in vec3 outColor;
smooth in vec3 outPos_W;
smooth in vec3 outEye_W;

// material settings
uniform sampler2D inTexMat;

uniform vec4 matDiffuse;
uniform vec4 matSpecular;
uniform float shininess;

uniform bool isTextured;
uniform bool shaded;
uniform bool vertexColored;
uniform bool blackIsTransparent;

out vec4 finalColor;

void main() 
{
	finalColor = matDiffuse;
    
	if (isTextured)
	{
		vec4 texColor = texture(inTexMat, outUV0);
		
		vec3 color = matDiffuse.xyz * texColor.xyz;
		finalColor = vec4(color, texColor.w * matDiffuse.w);
		
		//For navlights (the in-game sprite)
		if(blackIsTransparent)
		{
			float alpha = (finalColor.x + finalColor.y + finalColor.z) / 3.0;
		}
	}
	
	if(vertexColored)
	{
		finalColor = vec4(outColor * matDiffuse.xyz, matDiffuse.w);
	}
	
	//final color (after gamma correction)
	vec3 gamma = vec3(1.0/2.2);
	finalColor = vec4(pow(finalColor.xyz, gamma), finalColor.a);
}