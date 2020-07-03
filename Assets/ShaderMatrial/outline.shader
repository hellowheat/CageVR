// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/outline"
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
				UNITY_FOG_COORDS(1)
			};

			float _Width;
			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.xyz += v.normal.xyz * _Width;
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Color;
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
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
                float4 pos: SV_POSITION;
				UNITY_FOG_COORDS(1)
				float3 objectLightPos : TEXCOORD4;
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
				o.normal = v.normal;
				o.objectLightPos = normalize(mul(unity_WorldToObject, _WorldSpaceLightPos0));
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float3 N = normalize(i.normal);
				float3 L = i.objectLightPos;
				float diffuse = dot(N, L);

                fixed4 col = tex2D(_MainTex, i.uv);
				if (diffuse >= _Shadow1_Radio)col.xyz *= _Shadow1_Depth;
				else if (diffuse >= _Shadow2_Radio)col.xyz *= _Shadow2_Depth;
				else col.xyz *= _Shadow3_Depth;
				
				UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
	Fallback "Specular"
}
