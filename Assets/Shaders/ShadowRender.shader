Shader "Test/ShadowRender"
{
	Properties
	{
		_DepthTex("DepthTex",2D) = "white"{}
	}

		SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
		}

		Pass
		{
			Tags
			{
				//"LightMode" = "FowardBase"
			}

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex VertexFun
			#pragma fragment FragFun

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct VertexIn
			{
				float4 Pos : POSITION;
				float2 Tex : TEXCOORD0;
			};

			struct VertexOut
			{
				float4 Pos : SV_POSITION;
				float2 Tex : TEXCOORD0;
				float4 LPos : TEXCOORD1;
			};

			sampler2D _DepthTex;
			float4x4 _LightProject;
			  
			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				vOut.Tex = vIn.Tex;

				vOut.LPos = mul(unity_ObjectToWorld, vIn.Pos);
				vOut.LPos.w = 1.0;
				//vOut.LPos.w = 1.0;
				//vOut.LPos.xyz = lPos.xyz / lPos.w;
				return vOut;
			}

			fixed4 FragFun(VertexOut vOut) : SV_Target
			{
				float4 lPos = mul(_LightProject,vOut.LPos);
				//vOut.LPos = lPos;
				float2 depthCoord = lPos.xy / lPos.w;
				depthCoord = depthCoord * 0.5 + 0.5;
				fixed4 depthCor = tex2D(_DepthTex, depthCoord);
				float depthValue = DecodeFloatRGBA(depthCor);
				
				//return depthCor;
				float currDepth = (lPos.z / lPos.w) + 0.005;
				
				if (currDepth < depthValue)
					return fixed4(0.5, 0.5, 0.5, 1);
				else
					return fixed4(0,0,0,1);

			}


			ENDCG
		}
	}

		FallBack "diffuse"
}