Shader "FX/Laser"
{
    Properties
    {
        _MainTex ("Main", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _NoiseContrast ("Noise Contrast", Range(0.0, 1.0)) = 0.0
        [HDR] _Color ("Color", Color) = (1, 1, 1, 1)
        _Thickness ("Thickness", Range(-5.0, 50.0)) = 0.5
        _Alpha ("Alpha", Range(0.0, 1.0)) = 1.0
        _Speed ("Speed", Float) = 1.0
        _Distort ("Distort", Range(0.0, 0.5)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "RenderQueue"="Transparent" }
        ZWrite Off
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 noise_uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _NoiseTex;
            float _NoiseContrast;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float4 _NoiseScale;
            fixed4 _Color;
            float _Thickness;
            float _Alpha;
            float _Speed;
            float _Distort;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.noise_uv = TRANSFORM_TEX(v.uv, _NoiseTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float noise_value = tex2D(_NoiseTex, i.noise_uv + float2(-_Time.x * _Speed, _Time.x * _Speed * 0.1)).r;
                float noise_remapped = (1.0 - noise_value * 2.0);
                float noise_norm = (_NoiseContrast + noise_value) / (1.0 + _NoiseContrast);
                float center_gradient = pow(1.0 - abs(i.uv.y - 0.5 + noise_value * _Distort), _Thickness);

                return center_gradient * _Color * noise_norm * _Alpha;
            }
            ENDCG
        }
    }
}
