Shader "Custom/AntiTilingAdvanced"
{
    Properties
    {
        _MainTex1("Texture 1", 2D) = "white" {}
        _MainTex2("Texture 2", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _Tiling("Tiling", Float) = 5.0
        _NoiseStrength("Noise Strength", Float) = 0.5
        _BlendStrength("Blend Strength", Float) = 0.2
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex1;
        sampler2D _MainTex2;
        sampler2D _NoiseTex;
        float _Tiling;
        float _NoiseStrength;
        float _BlendStrength;

        struct Input
        {
            float2 uv_MainTex1;
            float2 uv_MainTex2;
            float2 uv_NoiseTex;
        };

        // Fonction pour mélanger les textures et casser la répétition
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Calcul des UVs avec le tiling
            float2 uv1 = IN.uv_MainTex1 * _Tiling;
            float2 uv2 = IN.uv_MainTex2 * _Tiling;
            
            // Appliquer le bruit pour perturber les UVs
            float noise = tex2D(_NoiseTex, IN.uv_NoiseTex * 0.5).r;
            uv1 += (noise - 0.5) * _NoiseStrength;
            uv2 += (noise - 0.5) * _NoiseStrength;

            // Récupérer les couleurs des deux textures
            fixed4 col1 = tex2D(_MainTex1, uv1);
            fixed4 col2 = tex2D(_MainTex2, uv2);

            // Mélanger les deux textures en fonction du bruit
            float blendFactor = noise * _BlendStrength;
            fixed4 mixedColor = lerp(col1, col2, blendFactor);

            // Appliquer le résultat
            o.Albedo = mixedColor.rgb;
            o.Alpha = mixedColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
