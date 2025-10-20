Shader "UI/FOV_Radial"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Angle ("视野角度", Range(0, 360)) = 90
        _EdgeSoftness ("边缘柔化", Range(0, 0.2)) = 0.1
        _Color ("颜色", Color) = (1,1,1,0.3)
    }
    
    SubShader
    {
        Tags { 
            "Queue"="Transparent" 
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Angle;
            float _EdgeSoftness;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算中心点坐标
                float2 center = float2(0.5, 0.5);
                float2 dir = i.uv - center;
                
                // 转换为极坐标角度（0-360度）
                float angle = degrees(atan2(dir.y, dir.x)) + 90;
                if(angle < 0) angle += 360;
                
                // 计算可见区域
                float halfFOV = _Angle * 0.5;
                float visible = 0;
                
                // 左边缘渐变
                visible += smoothstep(180 - halfFOV - _EdgeSoftness*360, 
                                     180 - halfFOV, 
                                     angle);
                // 右边缘渐变
                visible -= smoothstep(180 + halfFOV, 
                                     180 + halfFOV + _EdgeSoftness*360, 
                                     angle);
                
                // 最终透明度计算
                fixed4 col = _Color;
                col.a *= saturate(visible);
                return col;
            }
            ENDCG
        }
    }
}