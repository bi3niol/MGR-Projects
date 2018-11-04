float4x4 World;
float4x4 View;
float4x4 Projection;
float3 CameraPosition = float3(0, 0, 60);

float4 AmbientColor = float4(1, 1, 1, 1);
float Ka = 0.3;
/* Lights */
#define MAXLIGHT 1
float LightPower[MAXLIGHT];

float3 LightPosition[MAXLIGHT];
float3 LightDirection[MAXLIGHT];
float3 LightColor[MAXLIGHT];

float LightKDiffuse[MAXLIGHT];

float LightKSpecular[MAXLIGHT];

float LightType[MAXLIGHT]; /*0 - directional, 1 - point, 2 - spot*/

int LightCount = 0;
float SpecularM = 12;


struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float3 Normal : NORMAL;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float3 Normal : NORMAL;
	float4 WorldPosition : POSITION1;
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
	output.WorldPosition = worldPosition;
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 c = float3(0,0,0);
	float3 lightDir;
	int lightType;
	float3 view = normalize(input.WorldPosition - CameraPosition);
	for (int i = 0; i < LightCount; i++)
	{
			lightDir = LightDirection[i];
			lightType = LightType[i];
			if (lightType != 0) {/* not directional */
				lightDir = normalize(LightPosition[i] - input.WorldPosition);
			}
			float3 diffuse = saturate(dot(lightDir, input.Normal));
			float3 _reflect = normalize(reflect(lightDir, input.Normal));
			float3 specular = pow(saturate(dot(_reflect, view)), SpecularM);
			c = c + (LightKDiffuse[i] * diffuse + LightKSpecular[i] * specular)*LightColor[i]*input.Color*LightPower[i];
	}
	return float4(saturate(c + AmbientColor * input.Color*Ka),1);
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

technique Color
{
	pass Pass1
	{
		VertexShader = compile vs_4_0_level_9_1 VertexShaderFunction();
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}