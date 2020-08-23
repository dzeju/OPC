#version 330 core
//out vec4 FragColor;
out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
    //FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}