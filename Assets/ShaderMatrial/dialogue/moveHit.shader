Shader "Unlit/moveHit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MoveSpeed("MoveSpeed",Range(0.01,1)) = 1
		_OffsetX("_OffsetX",Range(0,0.5)) = 0.5
		_OffsetY("_OffsetY",Range(0,0.5)) = 0.5
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 100

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _MoveSpeed;
			float _OffsetX;
			float _OffsetY;

            v2f vert (appdata v)
            {
                v2f o;
				v.vertex.xy += float2(_OffsetX, _OffsetY) * sin(_SinTime.x / _MoveSpeed);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
