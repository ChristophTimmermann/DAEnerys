#version 150

uniform mat4 model;
uniform vec3 cameraPosition;

// material settings
uniform sampler2D materialTex;
uniform sampler2D glowTex;
uniform sampler2D thrusterOffDiff;
uniform sampler2D thrusterOffGlow;
uniform sampler2D specularTex;
uniform sampler2D teamTex;
uniform sampler2D stripeTex;
uniform vec3 teamColor;
uniform vec3 stripeColor;

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
uniform bool team;
uniform bool stripe;

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

vec3 RGBToHSL(vec3 color)
{
	vec3 hsl; // init to 0 to avoid warnings ? (and reverse if + remove first part)

	float fmin = min(min(color.r, color.g), color.b); //Min. value of RGB
	float fmax = max(max(color.r, color.g), color.b); //Max. value of RGB
	float delta = fmax - fmin; //Delta RGB value

	hsl.z = (fmax + fmin) / 2.0; // Luminance

	if (delta == 0.0)	//This is a gray, no chroma...
	{
		hsl.x = 0.0;	// Hue
		hsl.y = 0.0;	// Saturation
	}
	else //Chromatic data...
	{
		if (hsl.z < 0.5)
			hsl.y = delta / (fmax + fmin); // Saturation
		else
			hsl.y = delta / (2.0 - fmax - fmin); // Saturation

		float deltaR = (((fmax - color.r) / 6.0) + (delta / 2.0)) / delta;
		float deltaG = (((fmax - color.g) / 6.0) + (delta / 2.0)) / delta;
		float deltaB = (((fmax - color.b) / 6.0) + (delta / 2.0)) / delta;

		if (color.r == fmax )
			hsl.x = deltaB - deltaG; // Hue
		else if (color.g == fmax)
			hsl.x = (1.0 / 3.0) + deltaR - deltaB; // Hue
		else if (color.b == fmax)
			hsl.x = (2.0 / 3.0) + deltaG - deltaR; // Hue

		if (hsl.x < 0.0)
			hsl.x += 1.0; // Hue
		else if (hsl.x > 1.0)
			hsl.x -= 1.0; // Hue
	}

	return hsl;

}

float HueToRGB(float f1, float f2, float hue)
{
	if (hue < 0.0)
		hue += 1.0;
	else if (hue > 1.0)
		hue -= 1.0;
	
	float res;
	if ((6.0 * hue) < 1.0)
		res = f1 + (f2 - f1) * 6.0 * hue;
	else if ((2.0 * hue) < 1.0)
		res = f2;
	else if ((3.0 * hue) < 2.0)
		res = f1 + (f2 - f1) * ((2.0 / 3.0) - hue) * 6.0;
	else
		res = f1;
	
	return res;
}

vec3 HSLToRGB(float hue, float sat, float lum)
{
	vec3 rgb;

	if (sat == 0.0)
		rgb = vec3(lum); // Luminance
	else
	{
		float f2;

		if (lum < 0.5)
			f2 = lum * (1.0 + sat);
		else
			f2 = (lum + sat) - (sat * lum);

		float f1 = 2.0 * lum - f2;

		rgb.r = HueToRGB(f1, f2, hue + (1.0/3.0));
		rgb.g = HueToRGB(f1, f2, hue);
		rgb.b = HueToRGB(f1, f2, hue - (1.0/3.0));
	}

	return rgb;
}

// Color mode uses the hue and saturation of the upper mask layer and the value of the lower image layer to form the resulting image.
vec3 ApplyTeamStripe(vec3 imageColor, vec3 maskColor)
{
	vec3 imageHSL = RGBToHSL(imageColor);
	vec3 maskHSL = RGBToHSL(maskColor);
	return HSLToRGB(maskHSL.x, maskHSL.y, imageHSL.z);
}

void main() 
{
	vec4 surfaceColor = vec4(fragColor, 1.0);
	
	if(textured)
	{
		surfaceColor = texture(materialTex, fragTexCoord);
		
		//For navlights (billboards)
		if(blackIsTransparent)
		{
			float blackness = (surfaceColor.x + surfaceColor.y + surfaceColor.z) / 3.0;
			surfaceColor = vec4(surfaceColor.xyz * 2, blackness * 2);
		}
		
		if(thruster)
		{
			vec4 thrusterOffColor = texture(thrusterOffDiff, fragTexCoord);
			surfaceColor = (1.0 - thrusterInterpolation) * thrusterOffColor + thrusterInterpolation * surfaceColor;
		}
		
		// TEAM
		if (team)
		{
			vec4 tColor = texture(teamTex, fragTexCoord);
			float maskValue = tColor.a / 2;
			vec3 maskColor = teamColor * (maskValue / 255);
			
			if (maskValue > 0)
				surfaceColor = vec4(vec3(ApplyTeamStripe(vec3(surfaceColor), maskColor)), 1);
		}
		
		// STRIPE
		if (stripe)
		{
			vec4 sColor = texture(stripeTex, fragTexCoord);
			float maskValue = sColor.a / 2;
			vec3 maskColor = stripeColor * (maskValue / 255);
			
			if (maskValue > 0)
				surfaceColor = vec4(vec3(ApplyTeamStripe(vec3(surfaceColor), maskColor)), 1);
		}
	}
	
	if(vertexColored)
		surfaceColor = surfaceColor * vec4(materialDiffuseColor.xyz, materialOpacity);
	else
		surfaceColor = vec4(materialDiffuseColor.xyz, materialOpacity);
	
	
	vec3 linearColor = vec3(0);
	if (shaded && !disableLighting) {
		
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
		if(emissive)
		{
			vec4 glowMap = texture(glowTex, fragTexCoord);
			if(thruster)
			{
				vec4 thrusterOffColor = texture(thrusterOffGlow, fragTexCoord);
				glowMap = (1.0 - thrusterInterpolation) * thrusterOffColor + thrusterInterpolation * glowMap;
			}
			
			if(!discreteGlow)
			{
				float glowValue = glowMap.x;
				linearColor += vec3(surfaceColor.r, surfaceColor.g, surfaceColor.b) * glowValue;
			}
			else
			{
				linearColor += glowMap.xyz;
			}
		}
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