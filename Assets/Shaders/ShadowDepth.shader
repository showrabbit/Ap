Shader "Test/ShadowDepth"
{
	Properties
	{

	}

	SubShader
	{
		Tags
		{
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		Pass
		{
			Tags
			{
		 
			}

			CGPROGRAM

			#pragma target 3.0
			#pragma vertex VertexFun
			#pragma fragment FragFun

			#include "UnityCG.cginc"

			struct VertexIn
			{
				float4 Pos : POSITION;
			};

			struct VertexOut
			{
				float4 Pos : SV_POSITION;
				float4 WPos : TEXCOORD0;
			};

			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				//float3 wPos = UnityObjectToViewPos(vIn.Pos);
				//vOut.WPos.w = -wPos.z * _ProjectionParams.w;
				vOut.WPos.xyz = vOut.Pos.xyz / vOut.Pos.w;
				return vOut;
			}

			fixed4 FragFun(VertexOut vOut) : SV_Target
			{
				return EncodeFloatRGBA(vOut.WPos.z);
			}
			ENDCG

		}
	}

		Fallback "diffuse"
}