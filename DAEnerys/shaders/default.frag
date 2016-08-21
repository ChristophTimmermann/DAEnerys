#version 330

smooth in vec2 outUV0;
uniform sampler2D inTexDiff;
uniform sampler2D inTexTeam;

uniform mat4 inMatM;
uniform vec4 inColTeam;
uniform vec4 inColStripe;

layout (location = 0) out vec4 finalCol0;

void main()
{
    vec4 texDiff = texture(inTexDiff, outUV0);
    vec3 texTeam = texture(inTexTeam, outUV0).xyz;
    float isPaint = 1.0 - texTeam.r*texTeam.g*(1.0-texTeam.b);

	// // Time to get painted (paint + diffuse curves logic)
	vec4 paint = mix(inColTeam, vec4(0.5, 0.5, 0.5, 1.0), texTeam.r);
	paint = mix(inColStripe, paint, texTeam.g);
    
	vec4 lumMap = vec4(0.299, 0.587, 0.114, 0.0);
	vec4 diffLum = vec4(texDiff.r, texDiff.g, texDiff.b, dot(texDiff, lumMap));
	vec4 d_hi = clamp(((diffLum*2)-1.0), 0.0, 1.0);
	vec4 d_lo = clamp((diffLum*2), 0.0, 1.0);
	paint = mix(d_lo * paint, vec4(1.0), d_hi);
    
	finalCol0 = vec4(paint.rgb, 1.0);
}
