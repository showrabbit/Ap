Shader "Test/TestShader"
{
	Properties
	{

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
				"LightMode" = "ForwardBase"
			}
			  
			ZTest On

			CGPROGRAM
			
			#pragma target 3.0
			#pragma vertex VertexFun
			#pragma fragment FragFun

			struct VertexIn
			{
				float4 Pos : POSITION;
				float2 Tex : TEXCOORD0;

			};

			struct VertexOut
			{
				float4 Pos : SV_POSITION;
				float2 Tex : TEXCOORD0;
			};

			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				vOut.Tex = vIn.Tex;
				return vOut;
			}

			fixed4 FragFun(VertexOut vOut) :SV_Target
			{
				float4 vPos = vOut.Pos * 0.5 + 0.5;
				return fixed4(vPos.x, vPos.y, vPos.z,1);

			}
			 
			ENDCG
		}
	}
	FallBack "diffuse"
}