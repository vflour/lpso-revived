Shader "CustomRenderTexture/Test"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _BaseGrey ("Minimum Grey Percentage", Float) = 0.25
        _MinWhite ("Minimum White Threshold", Float) = 0.01
        _MaxBlack ("Maximum Black Threshold", Float) = 0.99
        [PerRendererData] _SatMulti ("Saturation Multiplier", Float) = 0 

        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0        
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
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
        Blend One OneMinusSrcAlpha
        
        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SwapFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            float _BaseGrey;
            float _SatMulti;
            float _MinWhite;
            float _MaxBlack;
            
            // Get the smallest of RGB values 
            int GetMin(fixed4 color)
            {
                int min1 = color.r < color.g ? 0 : 1;
                int min2 = color[min1] < color.b ? min1 : 1;
                return min2;
            }

            // Get the biggest of RGB values
            int GetMax(fixed4 color)
            {
                int max1 = color.r > color.g ? 0 : 1;
                int max2 = color[max1] > color.b ? max1 : 2;
                return max2;
            }

            // Modifies the sprite color on a per-pixel basis
            fixed4 SwapFrag(v2f IN) : SV_Target
            {
                half4 storedColor = IN.color;
                half4 originalColor = SpriteFrag(IN) / storedColor;
                if (originalColor.r <= _MinWhite || originalColor.r >= _MaxBlack) return originalColor; 
                
                if (storedColor.r == storedColor.g && storedColor.g == storedColor.b) return originalColor*storedColor;

                float alpha = originalColor.a;
                originalColor += _BaseGrey;
                
                half4 color = originalColor * ( storedColor + _BaseGrey );

                int minIndex = GetMin(color);
                int maxIndex = GetMax(color);
                // This is used to determine how much saturation should be added 
                float satMultiplier = (1-originalColor.r)*_SatMulti;
                
                // wtf?
                float delta = color[maxIndex] - _BaseGrey + ( color[minIndex] - _BaseGrey) * (satMultiplier - 1);
                float minColor = color[maxIndex] - delta;
                
                // test for performance later
                if (minIndex == 0)
                    color.r = minColor;
                else if (minIndex == 1)
                    color.g = minColor;
                else 
                    color.b = minColor;

                color.a = alpha * storedColor.a;
                return color;
            }

        ENDCG
        }
    }
}
