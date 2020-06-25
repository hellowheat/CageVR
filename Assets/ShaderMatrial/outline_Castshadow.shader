// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/outline_2"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_Width("Width", Range(0,1)) = 1
		_Color("Color", Color) = (1,1,1,1)
		_Shadow1_Depth("Shadow 1 Color Depth", Range(0,1)) = 1
		_Shadow1_Radio("Shadow 1 Radio", Range(-1,1)) = 0.5
		_Shadow2_Depth("Shadow 2 Color Depth", Range(0,1)) = 1
		_Shadow2_Radio("Shadow 2 Radio", Range(-1,1)) = -0.5
		_Shadow3_Depth("Shadow 3 Color Depth", Range(0,1)) = 1
		_CastShadow("is Cast Shadow" , Range(-1,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}
        LOD 100

		 Pass
		{
			Cull front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float _Width;
			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.xyz += v.normal.xyz * _Width;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
			
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
			#pragma multi_compile_fog
			#pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				SHADOW_COORDS(3)
                float4 pos: SV_POSITION;
				float3 objectLightPos : TEXCOORD1;
				float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Shadow1_Radio;
			float _Shadow2_Radio;
			float _Shadow1_Depth;
			float _Shadow2_Depth;
			float _Shadow3_Depth;


            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				TRANSFER_SHADOW(o);
				o.normal = v.normal;
				o.objectLightPos = normalize(mul(unity_WorldToObject, _WorldSpaceLightPos0));
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float3 N = normalize(i.normal);
				float3 L = i.objectLightPos;
				float diffuse = dot(N, L);

                fixed4 col = tex2D(_MainTex, i.uv);
				if (diffuse >= _Shadow1_Radio)col *= _Shadow1_Depth;
				else if (diffuse >= _Shadow2_Radio)col *= _Shadow2_Depth;
				else col *= _Shadow3_Depth;

				float shadow = 0.7 + 0.3 * (SHADOW_ATTENUATION(i).x >=0.9?1:0);

                return col * shadow;
            }
            ENDCG
        }
    }
	Fallback "Specular"
}
