float4x4 World;
float4x4 View;
float4x4 Projection;
float3 CameraPosition = float3(0, 0, 60);

float4 AmbientColor = float4(1, 1, 1, 1);
float Ka = 0.3;
/* Lights */
#define MAXLIGHT 10
float LightPower[1];
float4 LightPosition_Direction[1];

float4 LightDiffuseColor[1];
float LightKDiffuse[1];

float4 LightSpecularColor[1];
float LightKSpecular[1];

int LightType[1]; /*0 - directional, 1 - point, 2 - spot*/
bool LightEnabled[1];

int LightCount = 0;
float SpecularM = 15;


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
	float4 Position1 : POSITION1;
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
	output.Position1 = float4(output.Position);
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 c = float4(0,0,0,0);
	float4 lightDir;
	int lightType;
	float4 view = normalize(float4(CameraPosition, 1.0) - input.Position1);;
	for (int i = 0; i < LightCount; i++)
	{
		if (LightEnabled[i] == true) {
			lightDir = LightPosition_Direction[i];
			lightType = LightType[i];
			if (lightType != 0) {/* not directional */
				lightDir = normalize(lightDir - input.Position1);
			}
			float4 diffuse = saturate(dot(-lightDir, input.Normal));
			float4 reflect = normalize(2 * diffuse*input.Normal - lightDir);
			float4 specular = pow(saturate(dot(reflect, view)), SpecularM);
			c = c + (LightDiffuseColor[i] * LightKDiffuse[i] * diffuse + LightSpecularColor[i] * LightKSpecular[i] * specular)*input.Color*LightPower[i];
		}
	}
	return saturate(c + AmbientColor * input.Color*Ka);
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