// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/RasterScroll"{
	Properties{
		_MainTex("MainTex", 2D) = "white" {}
		_Level("Level", Range(0, 1)) = 0.2
		_Speed("Speed", Range(0, 3)) = 0.5
		_RoundTrip("RoundTrip", Range(1, 100)) = 1
	}
		SubShader{
			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex:POSITION;
					float3 normal:NORMAL;
					float4 texcoord:TEXCOORD0;
				};

				struct v2f
				{
					float4 pos:POSITION;
					float2 uv:TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Level;
				float _Speed;
				float _RoundTrip;

				v2f vert(appdata_t v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}

				float4 frag(v2f i) :COLOR{
					//1�b�� _Speed �����Z�����^�C���̍쐬
					float time = _Time.y * _Speed;
				//y���W(0 ~ 1)�ɂ�����g�`�̃X�^�[�g�ʒu�̃Y��
				float dy = time - floor(time);
				//x���W(0 ~ 1)�̃Y��
				float dx = sin(radians((i.uv.y - dy) * 360 * floor(_RoundTrip))) * _Level;
				//�s�N�Z���̈ʒu���v�Z
				float2 uv = float2(i.uv.x + dx, i.uv.y);
				//x���W���͈͊O�ɂȂ��Ă���͍̂��œh��Ԃ�
				if (uv.x < 0 || 1 < uv.x)
					return float4(0, 0, 0, 0);
				return tex2D(_MainTex, uv);
			}
			ENDCG
		}
		}
}
