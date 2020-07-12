Shader "Shaders/TestLight"
{
	Properties
	{
		_MainTex("MainTex",2D) = "white"{}
		_BumpTex("BumpTex",2D) = "white"{}
	}

		SubShader
	{
		Tags
		{
			"RenderMode" = "Opaque"
			"Quene" = "Geometry"
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
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
				float4 Nor : NORMAL;
				float3 Tan : TANGENT;
			};

			struct VertexOut
			{
				float4 Pos : SV_POSITION;
				float2 Tex : TEXCOORD0;
				fixed4 Col : COLOR0;
			}; 

			sampler2D  _MainTex;

			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				vOut.Tex = vIn.Tex;
				vOut.Col = fixed4(0, 0, 0, 0);

				float3 diff;
				float3 spec;

				float3 wPos = normalize( UnityWorldToObjectDir(vIn.Pos));
				float3 wNor = normalize( UnityWorldToObjectDir(vIn.Nor));
				float3 lightDir = normalize( UnityWorldSpaceLightDir(wPos));

				float kD = dot(wNor, lightDir);

				if (kD > 0.01)
				{
					diff = kD * _LightColor0;
					//diff = kD * unity_LightColor[0];
					float3 wEye = normalize(UnityWorldSpaceViewDir(wPos));
					float kS = pow(max(dot(reflect(-lightDir, wNor), wEye),0), 2);
					spec = kS * fixed4(1,1,1,1);
					vOut.Col.xyz = diff + spec;
				}
				else
				{

				}
				return vOut; 
			}

			fixed4 FragFun(VertexOut vOut) : SV_Target
			{
				return 0.4 * vOut.Col + 0.6 * tex2D(_MainTex,vOut.Tex);
				//return tex2D(_MainTex,vOut.Tex);
				//return fixed4(1,1,1,1);
			}
				 
			ENDCG
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardAdd"
			}

			Blend one one

			CGPROGRAM

			#pragma target 3.0
			#pragma vertex VertexFun
			#pragma fragment FragFun
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct VertexIn
			{
				float4 Pos : POSITION;
				float4 Nor : NORMAL;
				float4 Tan : TANGENT;
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

			sampler2D _BumpTex;

			VertexOut VertexFun(VertexIn vIn)
			{
				VertexOut vOut;
				vOut.Pos = UnityObjectToClipPos(vIn.Pos);
				vOut.Tex = vIn.Tex;
				float3 wNor = UnityObjectToWorldDir(vIn.Nor);
				float3 wTan = UnityObjectToWorldDir(vIn.Tan);
				float3 wBit = cross(wNor, wTan) * vIn.Tan.w * unity_WorldTransformParams.w;
				wBit = normalize(wBit);

				vOut.TanX = float3(wTan.x, wBit.x, wNor.x);
				vOut.TanY = float3(wTan.y, wBit.y, wNor.y);
				vOut.TanZ = float3(wTan.z, wBit.z, wNor.z);
				vOut.WPos = mul(unity_ObjectToWorld,vIn.Pos);
				return vOut;
			}

			fixed4 FragFun(VertexOut vOut) : SV_Target
			{
				fixed3 nor = tex2D(_BumpTex,vOut.Tex) * 2 - 1;
				
				float3 wNor;
				wNor.x = dot(nor, vOut.TanX);
				wNor.y = dot(nor, vOut.TanY);
				wNor.z = dot(nor, vOut.TanZ);
				wNor = normalize(wNor);
				float3 wLight = normalize( _WorldSpaceLightPos0 - vOut.WPos );

				float kD = dot(wLight, wNor);
				
				if (kD > 0.01)
				{
					fixed4 diff = kD * _LightColor0;

					float3 wEye = normalize(UnityWorldSpaceViewDir(vOut.WPos));
					float kS = pow(max(0, dot(reflect(-wLight, wNor), wEye)), 2);
					fixed4 spec = kS * _LightColor0;

					return (diff + spec) * 1.0;
					//return fixed4(1, 0, 0, 0);
				}
				else
				{

				}

				return fixed4(0, 0, 0, 0);
				
			}



			ENDCG

		}
	}

		Fallback "Diffuse"
}