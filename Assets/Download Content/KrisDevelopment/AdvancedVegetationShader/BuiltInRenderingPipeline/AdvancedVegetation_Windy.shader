// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

//Shader by Hristo Mihailov Ivanov
Shader "Advanced Vegetation/Windy Vegetation" {
	Properties{
		_VegColor("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		[KeywordEnum(Off, Face, Back)] _Cull("Culling Mode", float) = 0
		[KeywordEnum(Off, On)] _A2C("A2C", float) = 0
		_BumpIntensity("Normal Intensity", Range(0,1)) = 1
		_Cutoff("Cutoff", Range(0,1)) = 0.5
		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Speed("Speed",Range(0.1,10)) = 1
		_Amount("Amount", Range(0.1,10)) = 3
		_Distance("Distance", Range(0, 0.5)) = 0.1
		_ZMotion("Z Motion", Range(0, 1)) = 0.5
		_ZMotionSpeed("Z Motion Speed", Range(0, 10)) = 10
		_OriginWeight("Origin Weight", Range(0, 1)) = 0

		[Space(8)]
		_UniformNormal("Uniform Normal", Range(0, 1)) = 0

		[Space(8)]
		// --- Character Interaction ---
		_CharacterInteraction("Character Interaction", Range(0, 1)) = 1
	}
		SubShader{
			Tags {
				"RenderQueue" = "AlphaTest"
				"RenderType" = "TransparentCutout"
				"IgnoreProjector" = "True"
				"RenderType" = "Grass"
				"DisableBatching" = "True"}
			LOD 100
			AlphaToMask[_A2C]
			Cull[_Cull]
			CGPROGRAM
			#pragma surface surf Standard vertex:vert addshadow
			#pragma target 3.0
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "../CGLib/AVS_Common_Lib.cginc"

			sampler2D _MainTex;
			sampler2D _BumpMap;

			UNITY_INSTANCING_BUFFER_START(Props)
			   UNITY_DEFINE_INSTANCED_PROP(half4, _VegColor)
	#define _VegColor_arr Props
			UNITY_INSTANCING_BUFFER_END(Props)

			struct Input {
				half2 uv_MainTex;
				half2 uv_BumpMap;
				float3 normal;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			half _BumpIntensity;
			half _Glossiness;
			half _Metallic;

			half _Speed;
			half _Amount;
			half _Distance;
			half _ZMotion;
			half _ZMotionSpeed;
			half _OriginWeight;
			half _Cutoff;
			half _CharacterInteraction;
			half _UniformNormal;

			void vert(inout appdata_full v)
			{
				UNITY_SETUP_INSTANCE_ID(v);
				half4 wp = mul(unity_ObjectToWorld, v.vertex);
				half4 originWp = mul(unity_ObjectToWorld, half4(0, 0, 0, 1));
				
				// animate the vertex
				half4 vertexMod;
				VertexMod_half(wp, originWp, _Speed, _Amount, _Distance, _ZMotion, _ZMotionSpeed, _OriginWeight, _CharacterInteraction, _Time.y, vertexMod);
				half4 newVert = mul(unity_WorldToObject, vertexMod);
				v.vertex = newVert;

				// make the normal uniform
				half3 _n;
				WorldNormalMod_half(mul(unity_ObjectToWorld, v.normal), _UniformNormal, _n);
				v.normal = mul(unity_WorldToObject, _n);
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				half4 tex = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(_VegColor_arr, _VegColor);
				o.Albedo = tex.rgb;
				half3 normal2 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				o.Normal = lerp(half3(0, 0, 1), normal2, _BumpIntensity);
				o.Smoothness = _Glossiness;
				o.Metallic = _Metallic;
				tex.a = (tex.a - _Cutoff) / max(fwidth(tex.a), 0.0001) + 0.5;
				clip(tex.a);

				o.Alpha = tex.a;
			}

			ENDCG
		}

			Fallback "Diffuse"
}
