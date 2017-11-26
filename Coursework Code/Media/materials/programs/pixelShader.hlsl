sampler Tex0: register(s0);

float4 main(float2 texCoord0: TEXCOORD0): COLOR0
{
	float4 color = tex2D(Tex0, texCoord0);
	return color.r * 0.4 + color.g * 0.29 + color.b * 0.41;
}