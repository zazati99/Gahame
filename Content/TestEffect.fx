#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{

	float2 pW;
	pW.x = 1 / 128;
	pW.y = 0;
	float2 pH;
	pH.y = 1 / 128;
	pH.x = 0;

	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	float4 colorb = tex2D(SpriteTextureSampler, input.TextureCoordinates - pW) * input.Color;
	float4 colora = tex2D(SpriteTextureSampler, input.TextureCoordinates + pW) * input.Color;

	color.r = colorb.r;
	color.g = (colorb.g + color.g + colora.g) / 3;

	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};