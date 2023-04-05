Shader "Custom/ShadowShader"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Transparency;
            float _CircleSize;
            float _GradientIntensity;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                _CircleSize = 0.15; //Size of outer circle (0.25 is max size)
                _GradientIntensity = 64; //Intensity of shadowing effect

                fixed4 col = tex2D(_MainTex, i.uv);
                col = fixed4(0,0,0,1); //basic color of shader is black

                if (pow(i.uv.x-0.5,2) + pow(i.uv.y - 0.5,2) < _CircleSize) //draw circle, set to middle of shader (0.5, 0.5)
                {
                    col.r = 0;
                    col.g = 0;
                    col.b = 0;
                    col.a = (pow(i.uv.x-0.5,2) + pow(i.uv.y-0.5,2)) * _GradientIntensity; //set color alpha, make gradient that lightens towards the center, make in shape of circle
                }
                return col;
            }
            ENDCG
        }
    }
}