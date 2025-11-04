Shader "Unlit/RoundedJellyShader" // Renamed for clarity
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _CornerRadius ("Corner Radius", Range(0, 0.5)) = 0.1
        
        // --- NEW JELLY PROPERTIES ---
        _ShineColor ("Shine Color", Color) = (1,1,1,0.5)
        _ShinePosition ("Shine Position (UV)", Vector) = (0.7, 0.7, 0, 0)
        _ShineSize ("Shine Size", Range(0.01, 1.0)) = 0.15
        // -----------------------------

        [HideInInspector] _Size ("Size", Vector) = (1,1,1,1) 
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex; 
            fixed4 _Color;
            float _CornerRadius;
            float4 _Size;

            // --- NEW JELLY PROPERTIES (Shader side) ---
            fixed4 _ShineColor;
            float2 _ShinePosition;
            float _ShineSize;
            // ----------------------------------------

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                OUT.worldPos = IN.vertex.xy * _Size.xy; 
                return OUT;
            }

            float sdRoundBox(float2 p, float2 b, float r)
            {
                float2 q = abs(p) - b + r;
                return min(max(q.x, q.y), 0.0) + length(max(q, 0.0)) - r;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // 1. Get base color from texture
                fixed4 col = tex2D(_MainTex, IN.texcoord) * IN.color;

                // 2. Calculate rounded corners
                float2 p = IN.worldPos.xy / _Size.xy;
                float2 b = float2(0.5, 0.5);
                float d = sdRoundBox(p, b, _CornerRadius);
                float aa = fwidth(d) * 0.5;
                float corner_alpha = 1.0 - smoothstep(-aa, aa, d);

                // --- 3. NEW: Calculate Shine ---
                // Get distance from the pixel's UV to the shine's position
                float shine_dist = distance(IN.texcoord, _ShinePosition);
                
                // Create a soft circle. Invert _ShineSize so slider feels right.
                float shine_value = 1.0 - smoothstep(0.0, _ShineSize, shine_dist);
                
                // Add the shine color to the base color
                // We multiply by col.a to make sure shine doesn't appear outside the sprite
                col.rgb += _ShineColor.rgb * shine_value * _ShineColor.a;
                // -----------------------------

                // 4. Apply final alpha
                col.a *= corner_alpha;

                return col;
            }
            ENDCG
        }
    }
}