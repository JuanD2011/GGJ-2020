Shader "Custom/PavementTrail"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _DestroyedColor("Destroyed Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _CurrentReloadingValue("Reloading Value", Range(0, 1)) = 0
        _Splat("Splat Render Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Splat;
        };

        sampler2D _MainTex;
        sampler2D _Splat;
        fixed4 _Color;
        fixed4 _DestroyedColor;
        fixed _CurrentReloadingValue;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half  amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0)).r;
            
            fixed4 c = lerp(tex2D (_MainTex, IN.uv_MainTex) * lerp(_DestroyedColor, _Color, _CurrentReloadingValue), fixed4(0,0,0,1), amount);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
