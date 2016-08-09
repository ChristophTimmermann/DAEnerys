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
uniform bool isNavLight;

out vec4 finalColor;

void main() 
{
	vec4 vertexColor = matDiffuse;
	finalColor = vertexColor;
    
	if (isTextured)
	{
		vec4 texColor = texture(inTexMat, outUV0);
		
		//For navlights (billboards)
		if(isNavLight)
		{
			vec3 navcolor = matDiffuse.xyz * texColor.xyz;
			float alpha = (navcolor.x + navcolor.y + navcolor.z) / 3.0;
            finalColor = vec4(navcolor, alpha);
		}
        else
        {
            finalColor = texColor;
        }
	}
    if (vertexColored) {
        //finalColor.xyz = finalColor.xyz * outColor;
	}
	
	//final color (after gamma correction)
	vec3 gamma = vec3(1.0/2.2);
	finalColor = vec4(pow(finalColor.xyz, gamma), finalColor.a);
}