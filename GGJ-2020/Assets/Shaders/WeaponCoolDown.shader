Shader "Custom/WeaponCoolDown"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _ReloadingColor("Reloading Color", Color) = (1,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [HideInInspector] _CurrentReloadingValue("Reloading Value", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _ReloadingColor;
        fixed _CurrentReloadingValue;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * lerp(_Color, _ReloadingColor, _CurrentReloadingValue);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
