Shader "Custom/EnemyFlashShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,0,1)   
        _MainTex ("Main Texture", 2D) = "white" {}
        _FlashAmount ("Flash Amount", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float _FlashAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 texCol = tex2D(_MainTex, i.uv);

                float4 baseColor  = _Color;
                float4 flashColor = float4(1,0,0,1);

                float4 finalColor = lerp(baseColor, flashColor, _FlashAmount);
                return texCol * finalColor;
            }
            ENDHLSL
        }
    }
}
