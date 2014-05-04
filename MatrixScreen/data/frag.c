uniform sampler2D texture;
uniform float pixel_threshold;

uniform float sigma;
uniform float glowMultiplier;
uniform float width;

const int KERNEL_SIZE = 5;
float glow = glowMultiplier / (sigma * sqrt(2.0 * 3.14159));

float blurWeight(float x)
{
	return (glow * exp(-(x*x) / (2.0 * sigma * sigma)));
}

void main()
{
	vec4 color = vec4(0.0);
	vec2 texCoord = gl_TexCoord[0].xy;

	for (int i = -KERNEL_SIZE; i <= KERNEL_SIZE; i++)
	{
		texCoord.x = gl_TexCoord[0].x + (i / width);
		color += texture2D(texture, texCoord) * blurWeight(i);
	}

	gl_FragColor = color;
}

//uniform sampler2D texture;
//uniform float pixel_threshold;
//
//void main()
//{
//	float factor = 1.0 / (pixel_threshold + 0.001);
//	vec2 pos = floor(gl_TexCoord[0].xy * factor + 0.5) / factor;
//	gl_FragColor = texture2D(texture, pos) * gl_Color;
//}