#version 330

smooth in vec2 outUV0;
smooth in vec2 outUV1;
// smooth in vec3 outNorm;
// smooth in vec3 outTan;
// smooth in vec3 outBiNorm;
// smooth in vec3 outPos_W;

uniform sampler2D inTexDiff;
// uniform sampler2D inTexGlow;
uniform sampler2D inTexTeam;
// uniform sampler2D inTexNorm;
uniform sampler2D inTexBadge;

uniform mat4 inMatM;
uniform vec4 inColTeam;
uniform vec4 inColStripe;

uniform vec4 inGammaScale;
// uniform ivec2 inLightCounts;
// uniform vec4 inLightShip[96];	// Pos/Diff/Spec (or just Pos/Diff) + attenuations in W
// uniform vec4 inLightCore[7];

layout (location = 0) out vec4 finalCol0;

void main()
{
    vec4 texDiff = texture(inTexDiff, outUV0);
    // vec4 texGlow = texture(inTexGlow, outUV0);
	// vec3 texNorm = normalize(texture(inTexNorm, outUV0).rgb*2.0-1.0);
	vec3 texTeam = texture(inTexTeam, outUV0).xyz;
	// vec4 texBadge = texture(inTexBadge, outUV1);
    float isPaint = 1.0 - texTeam.r*texTeam.g*(1.0-texTeam.b); //*(1.0-texBadge.a);

	// // Time to get painted (paint + diffuse curves logic)
	vec4 paintBase = vec4(0.5, 0.5, 0.5, 1.0);
	vec4 paint = mix(inColTeam, paintBase, texTeam.r);
	paint = mix(inColStripe, paint, texTeam.g);
    
	vec4 lumMap = vec4(0.299, 0.587, 0.114, 0.0);
	vec4 diffLum = vec4(texDiff.r, texDiff.g, texDiff.b, dot(texDiff, lumMap));
	vec4 d_hi = clamp(((diffLum*2)-1.0), 0.0, 1.0);
	vec4 d_lo = clamp((diffLum*2), 0.0, 1.0);	// Slight overdark for badge contouring
	paint = mix(d_lo * paint, vec4(1.0), d_hi);
	// diffLum.rgb = d_lo.w*texBadge.rgb;
	// d_lo = mix(diffLum, vec4(1.0), d_hi.w);
	// paint = mix(paint, d_lo, texBadge.a);

	// float mapGlowMask = texGlow.g;
	// vec3 diffRGB = paint.rgb*(1.0-mapGlowMask);
	// vec3 glowRGB = paint.rgb*mapGlowMask;
	
	// // Seed lighting with Ambient+Black
	// vec3 diffAll = inLightCore[0].rgb;
	// vec3 specAll = vec3(0,0,0);
	// vec3 tempTW = mat3(outTan, outBiNorm, outNorm)*texNorm;
	// vec3 worldNorm = normalize(mat3(inMatM)*tempTW);
    
	// // Key Light
	// vec3 litNorm = normalize(inLightCore[1].rgb);
	// float litDif = clamp(dot(litNorm, worldNorm), 0.0, 1.0);
	// diffAll += inLightCore[2].rgb*litDif;
	
	// // Fill Light
	// litNorm = normalize(inLightCore[4].rgb);
	// litDif = clamp(dot(litNorm, worldNorm), 0.0, 1.0);
	// diffAll += inLightCore[5].rgb*litDif;
	
	// // Point lights...
	// int inP = inLightCounts.x;	// Points?
	// int inL = 0;
	// for (; inL<inP; inL++)
	// {
		// int inS = inL*3;
		// vec3 litVec = inLightShip[inS].rgb-outPos_W;
		// vec3 atenVec = vec3(inLightShip[inS].w, inLightShip[inS+1].w, inLightShip[inS+2].w);
		// float litLen = length(litVec);
		// litVec = normalize(litVec);
		// litDif = clamp(dot(litVec, worldNorm), 0.0, 1.0);
		// float atenVal = 1.0/(atenVec.x+(atenVec.y*litLen)+(atenVec.z*litLen*litLen));
		// diffAll.rgb += inLightShip[inS+1].rgb*litDif*atenVal*2.0;
	// }
    
	// // Apply final lighting 
	// paint.rgb = (diffRGB*diffAll)+glowRGB;
	paint.rgb = pow(paint.rgb, vec3(inGammaScale)) / inGammaScale.a;
	
	finalCol0 = vec4(paint.rgb, 1.0);
}
