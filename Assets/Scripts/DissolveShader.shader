// This shader is inspired by Unity Shader Getting Essentials, Authored by FENG LE LE
// Including the noise texture used
// ISBN number: 978-7-115-42305-4



// The template code of this shader is adopted from COMP30019 Workshops

Shader "DissolveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Amount ("Burn Amount", Range(0, 1)) = 0
        _Width ("Burn Width", Range(0, 0.2)) = 0.1
        _PrimaryColor("Primary Color", Color) = (1, 0, 0, 1)
        _SecondaryColor("Secondary Color", Color) = (1, 0, 0, 1)
        _BurnMap("Burn Map", 2D) = "white" {}
		_Color("Tile colour", Color) = (1, 0, 0, 1)
	}
	SubShader
	{
		Pass
		{
            Tags { "LightMode" = "ForwardBase"}
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
            uniform sampler2D _BurnMap;
            float _Amount;
            float _Width;
            fixed4 _PrimaryColor;
            fixed4 _SecondaryColor;
			fixed4 _Color;
            float4 _BurnMap_ST;
            

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                 
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                float2 uvBurnMap : TEXCOORD1;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				
				

				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                o.uvBurnMap = TRANSFORM_TEX(v.uv, _BurnMap);
				return o;

				
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
                fixed4 burn = tex2D(_BurnMap, v.uvBurnMap);
				fixed4 col = tex2D(_MainTex, v.uv);
                clip(burn.r - _Amount);

                // simulate burn color
                float t = 1 - smoothstep(0, _Width, burn.r - _Amount);
                float3 burnCol = lerp(_PrimaryColor, _SecondaryColor, t);
                burnCol = pow(burnCol, 5);

                fixed4 finalCol = lerp(col, float4(burnCol, 1), t * step(0.0001, _Amount));

				return finalCol;
			}
			ENDCG
		}
	}
}
