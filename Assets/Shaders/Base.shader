//Shader "Text/Base"
//{
//	Properties
//	{
//		_MainTex("MainTex",2D) = {} "white"
//		_ClipRect("ClipRect",Vector) = (0,0,0,0)
//	}
//
//	SubShader
//	{
//		Tags
//		{
//			"Quene" = "Geometry" 
//			"RenderType" = "Opaque"
//		}
//
//		Pass
//		{
//			Tags
//			{
//				"LightMode" = "FowardBase"
//			}
//			
//			ColorMask RGBA // 颜色蒙版
//
//			Cull Back // 剪裁背面
//
//					  //Offset -1,1 // 偏移Z
//
//			ZTest LEqual // 预Z 测试方式
//			ZWrite On // 预Z 写入
//
//			Stencil
//			{
//				Ref 2 // 参考值
//				ReadMask 255 // 读取掩码 value & 255
//				WriteMask 1	// 写入掩码 Ref & 1
//				Comp always // 比较方式
//				Pass keep // 蒙版和深度测试都通过的操作
//				Fail zero // 蒙版和深度测试都失败的操作
//				ZFail keep // 蒙版成功和测试失败的操作
//			}
//
//			
//			CGPROGRAM
//			
//			#target 3.0
//			#pragma veretex VertexFun
//			#pragma FragFun
//
//			#include "UnityCG.cginc"
//			
//			struct VertexIn
//			{
//				float3 Pos : POSITION;
//				float3 Nor : NORMAL;
//				float3 Tan : TANGANT;
//				float2 Tex : TEXCOORD0;
//			};
//
//			struct VertexOut
//			{
//				float3 Pos : SV_POSITION;
//				float3 Nor : NORMAL;
//				float3 Tan : TANGANT;
//				float2 Tex : TEXCOORD0;
//			};
//
//
//			sampler2D _MainTex;
//			float4 _ClipRect;
//
//			float4 CalLightEffect()
//			{
//				float4 col;
//
//				return col;
//			}
//
//			VertexOut VertexFun(VertexIn vIn)
//			{
//
//			}
//
//			fixed4 FragFun(VeretexOut vOut) : SV_Target
//			{
//
//			}
//			
//			ENDCG
//		}
//	}
//
//	Fallback "Diffuse"
//}