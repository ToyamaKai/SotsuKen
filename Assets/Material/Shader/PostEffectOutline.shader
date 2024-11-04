Shader "Custom/OutlineEffect"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DepthTex("Depth Texture", 2D) = "white" {}
        _NormalTex("Normal Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness("Outline Thickness", Float) = 1.0
        _DepthThreshold("Depth Threshold", Float) = 0.01
        _NormalThreshold("Normal Threshold", Float) = 0.1
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert_img
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _DepthTex;
                sampler2D _NormalTex;

                float4 _OutlineColor;
                float _OutlineThickness;
                float _DepthThreshold;
                float _NormalThreshold;

                float GetDepth(sampler2D tex, float2 uv)
                {
                    return tex2D(tex, uv).r;
                }

                float3 GetNormal(sampler2D tex, float2 uv)
                {
                    return tex2D(tex, uv).rgb * 2.0 - 1.0;
                }

                float4 frag(v2f_img i) : SV_Target
                {
                    float2 uv = i.uv;

                    float depthCenter = GetDepth(_DepthTex, uv);
                    float3 normalCenter = GetNormal(_NormalTex, uv);

                    float edge = 0.0;

                    float2 offsets[4] = {
                        float2(_OutlineThickness, 0),
                        float2(-_OutlineThickness, 0),
                        float2(0, _OutlineThickness),
                        float2(0, -_OutlineThickness)
                    };

                    for (int j = 0; j < 4; j++)
                    {
                        float depthSample = GetDepth(_DepthTex, uv + offsets[j]);
                        float3 normalSample = GetNormal(_NormalTex, uv + offsets[j]);

                        // エッジ検出のしきい値を利用
                        edge += step(_DepthThreshold, abs(depthCenter - depthSample));
                        edge += step(_NormalThreshold, length(normalCenter - normalSample));
                    }

                    if (edge > 0.0)
                        return _OutlineColor;
                    else
                        return tex2D(_MainTex, uv);
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
