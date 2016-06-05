#version 150

uniform mat4 camera;
uniform mat4 model;
uniform mat4 modelview;

in vec3 vert;
in vec2 vertTexCoord;
in vec3 vertNormal;
in vec3 vertColor;

out vec3 fragVert;
out vec3 fragColor;
out vec2 fragTexCoord;
out vec3 fragNormal;

void main() {
    gl_Position = modelview * vec4(vert, 1.0);
	
    fragTexCoord = vertTexCoord;
	fragColor = vertColor;
	
	mat3 normMatrix = transpose(inverse(mat3(model)));
	fragNormal = normMatrix * vertNormal;
	fragVert = (model * vec4(vert, 1.0)).xyz;
}