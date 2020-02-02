Shader "Custom/TrailShading"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Coordinate("Coordinate", Vector) = (0,0,0,0)
        _Color ("Draw Color", Color) = (1,0,0,0)
        _Size("Size", Range(1, 500)) = 1
        Strength("Strength", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Coordinate, _Color;
            half _Size, _Strength;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2Dlod(_MainTex, float4(i.uv, 0, 0));
                float draw = pow(saturate(1 - distance(i.uv, _Coordinate.xy)), 500 / _Size);
                fixed4 drawCol = _Color * (draw * _Strength);
                return saturate(col + drawCol);
            }
            ENDCG
        }
    }
}