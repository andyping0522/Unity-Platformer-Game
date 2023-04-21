//UNITY_SHADER_NO_UPGRADE
// This shader is inspired by Waves, made by Jasper Flick
// https://catlikecoding.com/unity/tutorials/flow/waves/

// The template code of this shader is adopted from COMP30019 Workshops

Shader "WaveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _PrimaryWave ("First wave (dir, amplitude, wave length)", Vector) = (0.4,0.1,0.04,10)
        _SecondaryWave ("Second wave (dir, amplitude, wave length)", Vector) = (0.4,0.1,0.04,10)
	}
	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;	
            float4 _PrimaryWave;
            float4 _SecondaryWave;

			struct vertIn
			{
				float4 vertex : POSITION;
                
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				
				
				float3 points = v.vertex.xyz;
                float3 tangent = float3(1, 0, 0);
                float3 binormal = float3(0, 0, 1);
                float primaryAmplitude = _PrimaryWave.z;
                float primaryWaveLength = _PrimaryWave.w;
                float secondaryAmplitude = _SecondaryWave.z;
                float secondaryWaveLength = _SecondaryWave.w;
                float primaryK = 2 * UNITY_PI / primaryWaveLength;
                float2 primaryD = normalize(_PrimaryWave.xy);
                float primaryF = primaryK * (dot(primaryD, points.xz) - sqrt(9.81/primaryK) * _Time.y);
                float secondaryK = 2 * UNITY_PI / secondaryWaveLength;
                float2 secondaryD = normalize(_SecondaryWave.xy);
                float secondaryF = secondaryK * (dot(secondaryD, points.xz) - sqrt(9.81/secondaryK) * _Time.y);

                
                
                
                v.vertex.xzy += float3 (
                    primaryD.x * ((primaryAmplitude / primaryK) * cos(primaryF)),
                    (primaryAmplitude / primaryK) * sin(primaryF),
                    primaryD.y * ((primaryAmplitude / primaryK) * cos(primaryF))
                );

                v.vertex.xzy += float3 (
                    secondaryD.x * ((secondaryAmplitude / secondaryK) * cos(secondaryF)),
                    (secondaryAmplitude / secondaryK) * sin(secondaryF),
                    secondaryD.y * ((secondaryAmplitude / secondaryK) * cos(secondaryF))
                );

                

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;

				
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, v.uv);
				return col;
			}
			ENDCG
		}
	}
}