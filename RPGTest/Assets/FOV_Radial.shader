Shader "UI/FOV_Radial"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Angle ("��Ұ�Ƕ�", Range(0, 360)) = 90
        _EdgeSoftness ("��Ե�ữ", Range(0, 0.2)) = 0.1
        _Color ("��ɫ", Color) = (1,1,1,0.3)
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
                // �������ĵ�����
                float2 center = float2(0.5, 0.5);
                float2 dir = i.uv - center;
                
                // ת��Ϊ������Ƕȣ�0-360�ȣ�
                float angle = degrees(atan2(dir.y, dir.x)) + 90;
                if(angle < 0) angle += 360;
                
                // ����ɼ�����
                float halfFOV = _Angle * 0.5;
                float visible = 0;
                
                // ���Ե����
                visible += smoothstep(180 - halfFOV - _EdgeSoftness*360, 
                                     180 - halfFOV, 
                                     angle);
                // �ұ�Ե����
                visible -= smoothstep(180 + halfFOV, 
                                     180 + halfFOV + _EdgeSoftness*360, 
                                     angle);
                
                // ����͸���ȼ���
                fixed4 col = _Color;
                col.a *= saturate(visible);
                return col;
            }
            ENDCG
        }
    }
}