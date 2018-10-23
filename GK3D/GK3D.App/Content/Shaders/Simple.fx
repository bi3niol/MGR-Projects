float4x4 World;
float4x4 View;
float4x4 Projection;
float3 CameraPosition = float3(0, 0, 60);

float4 AmbientColor = float4(1, 1, 1, 1);
float Ka = 0.3;
/* Lights */
#define MAXLIGHT 10
float LightPower[MAXLIGHT];
float3 LightPosition_Direction[MAXLIGHT];

float4 LightDiffuseColor[MAXLIGHT];
float LightKDiffuse[MAXLIGHT];

float4 LightSpecularColor[MAXLIGHT];
float LightKSpecular[MAXLIGHT];

int LightType[MAXLIGHT]; /*0 - directional, 1 - point, 2 - spot*/
bool LightEnabled[MAXLIGHT];

int LightCount = 0;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float4 Normal : NORMAL;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float4 Normal : NORMAL;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	float4 normal = normalize(mul(input.Normal, World));
	output.Normal = normal;
	output.Color = input.Color;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 c = float4(0,0,0,0);
	float4 d;
	for (int i = 0; i < LightCount; i++)
	{
		if (LightEnabled[i] == true) {
			float3 lightDir = LightPosition_Direction[i];
			int lightType = LightType[i];
			if (lightType != 0) {/* not directional */
				lightDir = normalize(lightDir - input.Position);
			}

			float4 diff = saturate(input.Color*LightDiffuseColor[i] * LightKDiffuse[i] * dot(input.Normal , lightDir));
			float4 spec = float4(0, 0, 0, 0);
			d = (diff + spec)*LightPower[i];
			c = c + d;
		}
	}
	c = c + input.Color*AmbientColor*Ka;
	return saturate(c);
}

//float4 CalculateParticularColor(int lightId, float4 position, float4 normal, float4 baseColor) :COLOR0
//{
//	float3 lightDir = LightPosition_Direction[lightId];
//	int lightType = LightType[lightId];
//	if (lightType != 0) {/* not directional */
//		lightDir = normalize(lightDir - position);
//	}
//
//	float4 diff = saturate(baseColor*LightDiffuseColor[lightId] * LightKDiffuse[lightId] * dot(normal, lightDir));
//
//	return LightPower[lightId] * (diff);
//}

technique Ambient
{
	pass Pass1
	{
		VertexShader = compile vs_4_0_level_9_1 VertexShaderFunction();
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}