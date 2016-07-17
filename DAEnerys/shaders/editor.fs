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

uniform vec3 diffuse;
uniform vec3 specular;
uniform float opacity;
uniform float shininess;

uniform bool isTextured;
uniform bool shaded;
uniform bool vertexColored;
uniform bool isNavLight;

out vec4 finalColor;

void main() 
{
	vec4 surfaceColor = vec4(outColor, 1.0);
	
	if (isTextured)
	{
		surfaceColor = texture(inTexMat, outUV0);
		
		//For navlights (billboards)
		if(isNavLight)
		{
			float blackness = (surfaceColor.x + surfaceColor.y + surfaceColor.z) / 3.0;
			surfaceColor = vec4(surfaceColor.xyz * 2, blackness * 2);
		}
	} else {
        if (vertexColored)
            surfaceColor = surfaceColor * vec4(diffuse.xyz, opacity);
        else
            surfaceColor = vec4(diffuse.xyz, opacity);
	}
	
	//final color (after gamma correction)
	vec3 gamma = vec3(1.0/2.2);
	finalColor = vec4(pow(surfaceColor.xyz, gamma), surfaceColor.a);
}