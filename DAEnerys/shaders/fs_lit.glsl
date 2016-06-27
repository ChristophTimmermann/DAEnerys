#version 150

uniform mat4 model;
uniform vec3 cameraPosition;

// material settings
uniform sampler2D materialTex;
uniform sampler2D glowTex;
uniform sampler2D thrusterOffDiff;
uniform sampler2D thrusterOffGlow;
uniform sampler2D specularTex;

uniform float thrusterInterpolation;
uniform bool disableLighting;

uniform vec3 materialDiffuseColor;
uniform float materialOpacity;
uniform float materialShininess;
uniform vec3 materialSpecularColor;

uniform bool textured;
uniform bool shaded;
uniform bool vertexColored;
uniform bool emissive;
uniform bool discreteGlow;
uniform bool thruster;
uniform bool specular;

uniform bool blackIsTransparent;

//array of lights
#define MAX_LIGHTS 64
uniform int numLights;
uniform struct Light
{
	bool enabled;
    vec4 position;
    vec3 intensities; //a.k.a the color of the light
    float attenuation;
    float ambientCoefficient;
} 
allLights[MAX_LIGHTS];

in vec2 fragTexCoord;
in vec3 fragNormal;
in vec3 fragColor;
in vec3 fragVert;

out vec4 finalColor;

vec3 ApplyLight(vec4 lightPos, vec3 lightColor, float lightAtten, float lightAmbientCoefficient, vec3 surfaceColor, vec3 normal, vec3 surfacePos, vec3 surfaceToCamera, float specularIntensity) 
{
    vec3 surfaceToLight;
    float attenuation = 1.0;
    if(lightPos.w == 0.0) 
	{
        //directional light
        surfaceToLight = normalize(lightPos.xyz);
        attenuation = 1.0; //no attenuation for directional lights
    } 
	else 
	{
        //point light
        surfaceToLight = normalize(lightPos.xyz - surfacePos);
        float distanceToLight = length(lightPos.xyz - surfacePos);
        attenuation = 1.0 / (1.0 + lightAtten * pow(distanceToLight, 2));
    }

    //ambient
    vec3 ambient = lightAmbientCoefficient * surfaceColor.rgb * lightColor;

    //diffuse
    float diffuseCoefficient = max(0.0, dot(normal, surfaceToLight));
    vec3 diffuse = diffuseCoefficient * surfaceColor.rgb * lightColor;
	
    //specular
    float specularCoefficient = 0.0;
	
    if(diffuseCoefficient > 0.0)
        specularCoefficient = pow(max(0.0, dot(surfaceToCamera, reflect(-surfaceToLight, normal))), materialShininess);
    vec3 specular = specularCoefficient * materialSpecularColor * lightColor * specularIntensity * 3;

    //linear color (color before gamma correction)
    return ambient + attenuation *(diffuse + specular);
}

void main() 
{
	vec4 surfaceColor = vec4(fragColor, 1.0);
	float blackness = 1.0;
	
	if(textured)
	{
		surfaceColor = texture(materialTex, fragTexCoord);
		
		//For navlights (billboards)
		if(blackIsTransparent)
		{
			blackness = (surfaceColor.x + surfaceColor.y + surfaceColor.z) / 3.0;
			surfaceColor = vec4(surfaceColor.xyz * 2, blackness * 2);
		}
		
		if(thruster)
		{
			vec4 thrusterOffColor = texture(thrusterOffDiff, fragTexCoord);
			surfaceColor = (1.0 - thrusterInterpolation) * thrusterOffColor + thrusterInterpolation * surfaceColor;
		}
	}
	
	if(vertexColored)
		surfaceColor = surfaceColor * vec4(materialDiffuseColor.xyz, materialOpacity);
	else
		surfaceColor = vec4(materialDiffuseColor.xyz, materialOpacity);
	
	vec3 linearColor = vec3(1, 0, 0);
	if(shaded && !disableLighting)
	{
		vec3 normal = normalize(fragNormal);
		vec3 surfacePos = vec3(model * vec4(fragVert, 1));
		
		vec3 surfaceToCamera = normalize(cameraPosition - surfacePos);
		
		float specularIntensity = 1;
		if(specular)
		{
			vec4 specularMap = texture(specularTex, fragTexCoord);
			specularIntensity = specularMap.x;
		}
		
		for(int i = 0; i < numLights; ++i)
		{
			if(allLights[i].enabled)
				linearColor += ApplyLight(
                    allLights[i].position,
                    allLights[i].intensities,
                    allLights[i].attenuation,
                    allLights[i].ambientCoefficient,
                    surfaceColor.rgb, normal, surfacePos, surfaceToCamera, specularIntensity);
		}
	
		//GLOW
		// if(emissive)
		// {
			// vec4 glowMap = texture(glowTex, fragTexCoord);
			// if(thruster)
			// {
				// vec4 thrusterOffColor = texture(thrusterOffGlow, fragTexCoord);
				// glowMap = (1.0 - thrusterInterpolation) * thrusterOffColor + thrusterInterpolation * glowMap;
			// }
			
			// if(!discreteGlow)
			// {
				// float glowValue = glowMap.x;
				// linearColor += vec3(surfaceColor.r, surfaceColor.g, surfaceColor.b) * glowValue;
			// }
			// else
			// {
				// linearColor += glowMap.xyz;
			// }
		// }
	}
	else
	{
		finalColor = surfaceColor;
		linearColor = finalColor.xyz;
	}
	
	//final color (after gamma correction)
	vec3 gamma = vec3(1.0/2.2);
	finalColor = vec4(pow(linearColor, gamma), surfaceColor.a);
}