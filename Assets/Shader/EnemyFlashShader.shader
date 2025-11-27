Shader "Custom/EnemyFlashShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (0,0,1,1)     // Biru dasar (Lauren)
        _MainTex ("Main Texture", 2D) = "white" {}   // Tetap tampilkan tekstur pesawat
    }

    SubShader
    {
        Tags {"RenderType"="Opaque"}
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

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 texCol = tex2D(_MainTex, i.uv);

                // warna akhir = tekstur * biru
                return texCol * _Color;
            }

            ENDHLSL
        }
    }
}
