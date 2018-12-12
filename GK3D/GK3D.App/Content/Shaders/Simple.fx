#if OPENGL
#define SV_POSITION POSITION sv_4_0_level_9_3
#else
#define VS_SHADERMODEL vs_4_0_level_9_3
#define PS_SHADERMODEL ps_5_0

float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 TextureMatrix;

float3 CameraPosition = float3(0, 0, 60);
float3 CameraUp = float3(0, 1, 0);

//float Ka = 0.3;
float4 AmbientColor = float4(1, 1, 1, 1);
float Ka = 0.08;
/* Lights */
#define MAXLIGHT 3
float LightPower[MAXLIGHT];

float3 LightPosition[MAXLIGHT];
float3 LightDirection[MAXLIGHT];
float3 LightColor[MAXLIGHT];

float LightKDiffuse[MAXLIGHT];
float LightKSpecular[MAXLIGHT];

float LightType[MAXLIGHT]; /*0 - directional, 1 - point, 2 - spot*/

float MaxCosValueForReflector = 0.7f;

int LightCount = 0;
float SpecularM = 12;

float MipMapLevelOfDetailBias = 0;

int LightsEnabled = 1;

int TextureLoaded = 0;
texture _Texture;
sampler TextureSampler = sampler_state {
	texture = (_Texture);
};

struct VertexShaderInput_Texture
{
	float4 Position : SV_Position;
	float3 Normal : NORMAL0;
	float2 UV : TEXCOORD0;
};

struct VertexShaderOutput_Texture
{
	float4 Position : SV_Position;
	float2 UV : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float4 WorldPosition : TEXCOORD2;
};

struct VertexShaderInput_Skybox
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL;
};

struct VertexShaderOutput_Skybox
{
	float4 Position : POSITION0;
	float3 TextureCoordinate : TEXCOORD0;
};

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

struct VertexShaderInput_Bilboard
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
};

VertexShaderOutput_Texture VertexShaderFunction_Bilboard(VertexShaderInput_Bilboard input)
{
	VertexShaderOutput_Texture output;

	float3 center = mul(input.Position, World);
	float3 eyeVector = (center - CameraPosition);
	output.Normal = normalize(CameraPosition - center);

	float3 upVector = normalize(CameraUp);
	float3 sideVector = cross(eyeVector, upVector);
	sideVector = normalize(sideVector);

	float3 finalPosition = center;
	finalPosition += (input.UV.x - 0.5f)*sideVector;
	finalPosition += (1.f - input.UV.y)*upVector;

	float4 finalPosition4 = float4(finalPosition, 1);

	float4x4 preViewProjection = mul(View, Projection);
	output.Position = mul(finalPosition4, preViewProjection);
	output.WorldPosition = output.Position;
	output.UV = input.UV;

	return output;
}

VertexShaderOutput_Skybox VertexShaderFunction_EnvMapping(VertexShaderInput_Texture input)
{
	VertexShaderOutput_Skybox output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	float4 VertexPosition = mul(input.Position, World);
	float4 normal = normalize(mul(input.Normal, World));
	output.TextureCoordinate = normal;

	return output;
}

VertexShaderOutput_Texture VertexShaderFunction_TextureMatrix(VertexShaderInput_Texture input)
{
	VertexShaderOutput_Texture output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	float4 normal = normalize(mul(input.Normal, World));
	output.Normal = normal;
	output.UV = mul(input.Position, TextureMatrix).xy;
	output.WorldPosition = worldPosition;
	return output;
}

VertexShaderOutput_Skybox VertexShaderFunction_Skybox(VertexShaderInput_Skybox input)
{
	VertexShaderOutput_Skybox output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	float4 VertexPosition = mul(input.Position, World);
	output.TextureCoordinate = VertexPosition - CameraPosition;

	return output;
}

float4 PixelShaderFunction_Skybox(VertexShaderOutput_Skybox input) : COLOR0
{
	//return AmbientColor;
	return float4(texCUBE(TextureSampler, normalize(input.TextureCoordinate)).rgb,1);
}

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
VertexShaderOutput_Texture VertexShaderFunction_Texture(VertexShaderInput_Texture input)
{
	VertexShaderOutput_Texture output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	float4 normal = normalize(mul(input.Normal, World));
	output.Normal = normal;
	output.UV = input.UV;
	output.WorldPosition = worldPosition;
	return output;
}

float3 CalculateParticularColor(int lightId, float3 normal, float3 view, float4 worldPos) :COLOR0
{
	float3 lightDir, diffuse, _reflect, specular, toLightDir;
	float rr = 1.f,lightType = LightType[lightId],cosVal;
	lightDir = normalize(LightDirection[lightId]);
	toLightDir = lightDir;

	if (lightType != 0) {/* not directional */
		toLightDir = normalize(LightPosition[lightId] - worldPos);
	}

	diffuse = saturate(dot(toLightDir, normal));

	if (lightType == 2) {//Reflector

		cosVal = saturate(dot(-toLightDir, lightDir));
		rr = 0;
		if (MaxCosValueForReflector < cosVal) {
			rr = pow(cosVal,30);
		}
	}
	_reflect = normalize(reflect(toLightDir, normal));
	specular = pow(saturate(dot(_reflect, view)), SpecularM);

	return rr * (LightKDiffuse[lightId] * diffuse + LightKSpecular[lightId] * specular)*LightColor[lightId];
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 c = AmbientColor;
	float3 view = normalize(input.WorldPosition - CameraPosition);
	if (LightsEnabled == 1) {
		c = c * Ka;
		for (int i = 0; i < LightCount; i++)
		{
			c = c + CalculateParticularColor(i, input.Normal, view, input.WorldPosition);
		}
	}
	return float4(saturate(c*input.Color),1);
}

float4 PixelShaderFunction_Texture(VertexShaderOutput_Texture input) : COLOR0
{
	float3 c = AmbientColor;
	float4 color = tex2D(TextureSampler, input.UV);
	if (TextureLoaded == 0) {
		color = float4(1, 1, 1,1);
	}
	float3 view = normalize(input.WorldPosition - CameraPosition);
	if (LightsEnabled == 1) {
		c = c * Ka;
		for (int i = 0; i < LightCount; i++)
		{
			c = c + CalculateParticularColor(i, input.Normal, view, input.WorldPosition);
		}
	}
	return float4(saturate(c*color), color.a);
}

technique TechColor
{
	pass Color
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction();
	}
}

technique TechTexture
{
	pass Texture
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction_Texture();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction_Texture();
	}
}

technique TechTextureMatrix
{
	pass TextureMatrix
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction_TextureMatrix();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction_Texture();
	}
}

technique EnvMapping
{
	pass Pass1
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction_EnvMapping();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction_Skybox();
	}
}

technique Skybox
{
	pass Pass1
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction_Skybox();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction_Skybox();
	}
}

technique Bilboard
{
	pass Pass1
	{
		VertexShader = compile vs_4_0_level_9_3 VertexShaderFunction_Bilboard();
		PixelShader = compile ps_4_0_level_9_3 PixelShaderFunction_Texture();
	}
}