Shader "Custom/ToonShaderWithFrostbite"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _FrostbiteTex("Frostbite Effect Texture", 2D) = "white" {}
        _Color("Effect Color", Color) = (1,0,0,0)
        _Opacity("Opacity", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : POSITION;
                    float2 uv : TEXCOORD0;
                };

                sampler2D _MainTex;
                sampler2D _FrostbiteTex;
                float _Opacity;
                float4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    half4 baseColor = tex2D(_MainTex, i.uv);
                    half4 frostbiteColor = tex2D(_FrostbiteTex, i.uv) * _Color;
                    frostbiteColor.a = _Opacity; // ìßâﬂìxÇê›íË

                    // ê‘Ç›ÇèdÇÀÇƒìßâﬂÇêßå‰
                    return lerp(baseColor, frostbiteColor, frostbiteColor.a);
                }
                ENDCG
            }
        }
}
