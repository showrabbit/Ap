Shader "Test/BumpShader"
{
	Properties
	{
		_MainTex("MainTex",2D) = "white" {}
		_BumpTex("BumpTex",2D) = "white" {}
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
				"LightMode" = "FowardBase"
			}
			
			Cull Back

			CGPROGRAM
				
			#pragma target 3.0
			#pragma vertex VertexFun
			#pragma fragment FragFun

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct VertexIn
			{
				float4 Pos : POSITION;
				float3 Nor : NORMAL;
				float3 Tan : TANGENT;
				float2 Tex : TEXCOORD0;
			};

			struct VertexOut
			{
				float4 Pos : SV_POSITION;
				float2 Tex : TEXCOORD0;
				
				float3 TanX : TEXCOORD1;
				float3 TanY : TEXCOORD2;
				float3 TanZ : TEXCOORD3;

				float3 WPos : TEXCOORD4;
			};

			sampler2D _MainTex;
			sampler2D _BumpTex;

			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				vOut.WPos = mul(unity_ObjectToWorld, vIn.Pos);// UnityObjectToClipPos(vIn.Pos);
				float3 wNor = UnityObjectToWorldNormal(vIn.Nor);
				float3 wTan = UnityObjectToWorldNormal(vIn.Tan);
				float3 wBit = cross(wNor, wTan) * vOut.Pos.w * unity_WorldTransformParams.w;
				wBit = normalize(wBit);
				vOut.TanX = float3(wTan.x, wBit.x, wNor.x);
				vOut.TanY = float3(wTan.y, wBit.y, wNor.y);
				vOut.TanZ = float3(wTan.z, wBit.z, wNor.z);

				vOut.Tex = vIn.Tex;

				return vOut;
			}

			fixed4 FragFun(VertexOut vOut) : SV_Target
			{
				fixed4 texCol = tex2D(_MainTex, vOut.Tex);

				float3 wNor = tex2D(_BumpTex, vOut.Tex);

				wNor.x = wNor.x * vOut.TanX;
				wNor.y = wNor.y * vOut.TanY;
				wNor.z = wNor.z * vOut.TanZ;

				

				float3 dir = normalize( UnityWorldSpaceLightDir(vOut.WPos) );
				float dif = dot(dir, wNor);
				float3 difCol = 0.5 * dif * _LightColor0;

				return texCol * 0.5;//+ fixed4(difCol.x,difCol.y,difCol.z,1.0);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}