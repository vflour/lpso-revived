Shader "CustomRenderTexture/Test"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _CColor ("Coat Color", Color) = (1,1,1,1)
        _PColor ("Patch Color", Color) = (1,1,1,1)
        _EColor ("Eye Color", Color) = (1,1,1,1)
        _BaseGrey ("Minimum Grey Percentage", Float) = 0.25
        _MinWhite ("Minimum White Threshold", Float) = 0.01
        _MaxBlack ("Maximum Black Threshold", Float) = 0.99
        [PerRendererData] _CSatMulti ("Coat Saturation", Float) = 1 
        [PerRendererData] _PSatMulti ("Patch Saturation", Float) = 1 
        [PerRendererData] _ESatMulti ("Eye Saturation", Float) = 1 
        
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
            float _CSatMulti;
            float _PSatMulti;
            float _ESatMulti;
            float _MinWhite;
            float _MaxBlack;
            half4 _CColor;
            half4 _PColor;
            half4 _EColor;
            
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
            
            float isNear(float a, float b)
            {
                return abs(b - a) < 0.001 ? 1 : 0;
            }

            void Unity_Branch_float(float Predicate, float True, float False, out float Out)
            {
                Out = lerp(False, True, Predicate);
            }

            uint GetColorType(half4 color)
            {
                //half bound = 0.5019608;
                half incr = 0.01;
                half rsub = color.r - color.g; 
                half gsub = color.g - color.b; 
                half bsub = color.b - color.r; 
                
                uint val = 3;
                Unity_Branch_float(rsub <= incr && rsub >= 0 && color.g == color.b, 0, val, val);
                Unity_Branch_float(gsub <= incr && gsub >= 0 && color.r == color.b, 1, val, val);
                Unity_Branch_float(bsub <= incr && bsub >= 0 && color.g == color.r, 2, val, val);
                
                return val;
            }

            half4 GetColor(uint colorType)
            {
                half4 color = _Color;
                color = lerp(color, _CColor, colorType == 0);
                color = lerp(color, _PColor, colorType == 1);
                color = lerp(color, _EColor, colorType == 2);
                return color;
            }

            float GetSaturation(uint colorType)
            {
                float saturation = 1;
                saturation = lerp(saturation, _CSatMulti, colorType == 0);
                saturation = lerp(saturation, _PSatMulti, colorType == 1);
                saturation = lerp(saturation, _ESatMulti, colorType == 2);
                return saturation;
            }
            
            half4 newColor;
            
            void ApplyValue(uint i, half value)
            {
                newColor.r = lerp(newColor.r, value, i == 0);
                newColor.g = lerp(newColor.g, value, i == 1);
                newColor.b = lerp(newColor.b, value, i == 2);
            }

            // Modifies the sprite color on a per-pixel basis
            float4 SwapFrag(v2f IN) : SV_Target
            {
                
                half4 originalColor = SpriteFrag(IN);

                if (originalColor.r == originalColor.g && originalColor.g == originalColor.b)
                    return originalColor;
                // get color based on r g b values
                int colorType = GetColorType(originalColor);
                if (colorType == 3) return originalColor;

                uint other = colorType == 2 ? 0 : colorType + 1;
                fixed4 storedColor = GetColor(colorType);                   

                float alpha = originalColor.a;
                originalColor = originalColor[other];
                originalColor += _BaseGrey;
                originalColor.a = alpha;

                half4 color = originalColor * ( storedColor + _BaseGrey );

                int minIndex = GetMin(color);
                int maxIndex = GetMax(color);
                
                float colorZone = color[maxIndex] - color[minIndex];
                float middle = color[minIndex] + colorZone/2;

                float satMultiplier = (1-originalColor.r) * GetSaturation(colorType);
                float increasedZone = satMultiplier;
                float deltaMin = (middle - color[minIndex]) * increasedZone;
                float deltaMax = (color[maxIndex] - middle) * increasedZone;
               
                newColor = color;
                ApplyValue(minIndex, color[minIndex] - deltaMin);
                ApplyValue(maxIndex, color[maxIndex] + deltaMax);               
                // This is used to determine how much saturation should be added 
                
                // test for performance later
                
                newColor -= 0.25;
                newColor.a = alpha * storedColor.a;
                return newColor;
            }

        ENDCG
        }
    }
}
