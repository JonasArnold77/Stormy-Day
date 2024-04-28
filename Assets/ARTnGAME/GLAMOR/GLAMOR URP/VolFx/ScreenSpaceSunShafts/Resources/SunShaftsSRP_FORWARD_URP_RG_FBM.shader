﻿Shader "Unlit/SunShaftsSRP_FORWARD_URP_RG_FBM"
{

	Properties
	{
		[HideInInspector] _MainTex("Base (RGB)", 2D) = "white" {}
	//[HideInInspector]_ColorBuffer("Base (RGB)", 2D) = "white" {}

		//[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
		//_Delta("Line Thickness", Range(0.0005, 0.0025)) = 0.001
		//[Toggle(RAW_OUTLINE)]_Raw("Outline Only", Float) = 0
		//[Toggle(POSTERIZE)]_Poseterize("Posterize", Float) = 0
		//_PosterizationCount("Count", int) = 8

		_SunThreshold("sun thres", Color) = (0.87, 0.74, 0.65,1)
		_SunColor("sun color", Color) = (1.87, 1.74, 1.65,1)
		_BlurRadius4("blur", Color) = (0.00325, 0.00325, 0,0)
		_SunPosition("sun pos", Vector) = (111, 11,339, 11)

			//NOISE
		_MainTexFBM("Texture", 2D) = "white" {}
		_Tex2("_Tex2", 2D) = "white" {}
		//_MaskTex("_MaskTex", 2D) = "white" {}
		_Distort("_Distort", Float) = 0.5
		_HighLight("_HighLight", Color) = (1,1,1,1)
		_noiseColor("_Color", Color) = (1,1,1,1)
		_Pow("_Pow", Float) = 0.5
		brightnessContrast("Brightness-Contrast ", Vector) = (1, 1, 1, 1)
			//_CloudSpeed("Cloud Speed", Vector) = (0.001, 0, 0, 0)
			cloudSpeed("Increase cloud Speed ", Vector) = (1, 1, 1, 1)

	}

		HLSLINCLUDE

			//#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl" //unity 2018.3
	//#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl" 
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		//#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/SurfaceInput.hlsl"
		//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		//#include "PostProcessing/Shaders/StdLib.hlsl" //unity 2018.1-2
		//#include "UnityCG.cginc"


			//v0.2
			float _Distort;
		float4 _HighLight;
		float4 _noiseColor;
		float _Pow;
		float4 brightnessContrast;
		//sampler2D _MainTexFBM;
		TEXTURE2D(_MainTexFBM);
		SAMPLER(sampler_MainTexFBM);
		float4 _MainTexFBM_ST;
		//sampler2D _Tex2;
		TEXTURE2D(_Tex2);
		SAMPLER(sampler_Tex2);
		float4 _Tex2_ST;
		//sampler2D _MaskTex;
		//float4 _MaskTex_ST;
		//float4 _CloudSpeed;
		float4 cloudSpeed;



			TEXTURE2D(_MainTexA);
		SAMPLER(sampler_MainTexA);

		TEXTURE2D(_MainTex);
		TEXTURE2D(_ColorBuffer);
		TEXTURE2D(_Skybox);

		SAMPLER(sampler_MainTex);
		SAMPLER(sampler_ColorBuffer);
		SAMPLER(sampler_Skybox);
		float _Blend;

		//sampler2D _MainTex;
		//sampler2D _ColorBuffer;
		//sampler2D _Skybox;
		//sampler2D_float _CameraDepthTexture;
		TEXTURE2D(_CameraDepthTexture);
		SAMPLER(sampler_CameraDepthTexture);
		half4 _CameraDepthTexture_ST;

		half4 _SunThreshold = half4(0.87, 0.74, 0.65, 1);

		half4 _SunColor = half4(0.87, 0.74, 0.65, 1);
		uniform half4 _BlurRadius4 = half4(2.5 / 768, 2.5 / 768, 0.0, 0.0);
		uniform half4 _SunPosition = half4(1, 1, 1, 1);
		uniform half4 _MainTex_TexelSize;
		uniform half4 _MainTexA_TexelSize;

#define SAMPLES_FLOAT 32.0f
#define SAMPLES_INT 32

		// Vertex manipulation
		float2 TransformTriangleVertexToUV(float2 vertex)
		{
			float2 uv = (vertex + 1.0) * 0.5;
			return uv;
		}

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			half4 screenPos : TEXCOORD1;
			half2 sunScreenPosition : TEXCOORD2;
#if UNITY_UV_STARTS_AT_TOP
			float2 uv1 : TEXCOORD3;
#endif		
		};

		struct v2f_radial {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
			float2 blurVector : TEXCOORD1;
			float2 sunScreenPosition: TEXCOORD2;
		};

		struct Varyings
		{
			float2 uv        : TEXCOORD0;
			float4 vertex : SV_POSITION;
			UNITY_VERTEX_OUTPUT_STEREO
		};


		half4x4 _CameraVP[2];
		half3 worldToScreenPositionA(half3 pnt)
		{
			half4x4 camVP = _CameraVP[0];

			half3 result;
			result.x = camVP._m00 * pnt.x + camVP._m01 * pnt.y + camVP._m02 * pnt.z + camVP._m03;
			result.y = camVP._m10 * pnt.x + camVP._m11 * pnt.y + camVP._m12 * pnt.z + camVP._m13;
			result.z = camVP._m20 * pnt.x + camVP._m21 * pnt.y + camVP._m22 * pnt.z + camVP._m23;
			half num = camVP._m30 * pnt.x + camVP._m31 * pnt.y + camVP._m32 * pnt.z + camVP._m33;
			num = 1.0 / num;
			result.x *= num;
			result.y *= num;
			result.z = num;

			result.x = result.x * 0.5 + 0.5;
			result.y = result.y * 0.5 + 0.5;

			return result;
		}

		float2 worldToScreenPosition(float3 pos) {
			pos = normalize(pos - _WorldSpaceCameraPos) * (_ProjectionParams.y + (_ProjectionParams.z - _ProjectionParams.y)) + _WorldSpaceCameraPos;
			float2 uv = 0;
			float3 toCam = mul(unity_WorldToCamera, pos);
			float camPosZ = toCam.z;
			float height = 2 * camPosZ / unity_CameraProjection._m11;
			float width = _ScreenParams.x / _ScreenParams.y * height;
			uv.x = (toCam.x + width / 2) / width;
			uv.y = (toCam.y + height / 2) / height;
			return uv;
		}



		float Linear01DepthA(float2 uv)
		{
#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
			return SAMPLE_TEXTURE2D_ARRAY(_CameraDepthTexture, sampler_CameraDepthTexture, uv, unity_StereoEyeIndex).r;
#else
			return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv);
#endif
		}

		//sampler2D _MainTexA
		TEXTURE2D(_MainTexB);
		SAMPLER(sampler_MainTexB);

		float4 FragGrey(v2f i) : SV_Target
		{
			float4 color = SAMPLE_TEXTURE2D(_MainTexB, sampler_MainTexB, i.uv.xy);//
			// color = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, float2(i.uv.x,1-i.uv.y));
			half4 colorB = SAMPLE_TEXTURE2D(_ColorBuffer, sampler_ColorBuffer, i.uv.xy);
			//float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
			//color.rgb = lerp(color.rgb, luminance.xxx, _Blend.xxx);
			//return color/2 + colorB/2;
			return color * 1.5;
		}

			half4 fragScreen(v2f i) : SV_Target{

						UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

		//half4 colorA = tex2D(_MainTex, i.uv.xy);
		//half4 colorA = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy); // half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
	

		////// NOISE
		//Get the colors at the right point on the first texture
		half4 col = SAMPLE_TEXTURE2D(_MainTexFBM, sampler_MainTexFBM, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw); //tex2D(_MainTexFBM, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw);

		//Use that to create an offset for the second texture
		//float2 offset = float2(_Distort*(col.x - .5), _Distort*(col.y - .5));
		//float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.001 * cloudSpeed.x, _Time.y * 0.001 * cloudSpeed.y);
		float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.25 * cloudSpeed.x, _Time.y * 0.011 * cloudSpeed.y) * 1;


#if !UNITY_UV_STARTS_AT_TOP
		///half4 colorB = tex2D(_ColorBuffer, i.uv1.xy);
		half4 colorB = SAMPLE_TEXTURE2D(_ColorBuffer, sampler_ColorBuffer, i.uv.xy );//v0.2 //i.uv1.xy);//v0.2
#else
		//half4 colorB = tex2D(_ColorBuffer, i.uv.xy);
		half4 colorB = SAMPLE_TEXTURE2D(_ColorBuffer, sampler_ColorBuffer, i.uv.xy );//v1.1
#endif
		half4 depthMask = saturate(colorB * _SunColor);

		//Get the colors from the second texture, using the offset to distort the image
		half4 col2 = SAMPLE_TEXTURE2D(_Tex2, sampler_Tex2, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset); //tex2D(_Tex2, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset);
		half4 colorA = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy);

		//return pow(depthMask,3.5);
		float4 output1 = (1.0f - (1.0f - colorA) * (1.0f - depthMask * (0.75+col2.r*0.76)) ) + brightnessContrast.x * 0.45 * depthMask + brightnessContrast.y * 0.75 * col.r * col2.r * pow(depthMask, 3.5 * brightnessContrast.z);// +brightnessContrast.y * 0.75 * 1 * pow(depthMask, 3.5 * brightnessContrast.z);//brightnessContrast
		return (output1)*_Blend + colorA * (1 - _Blend);



		return  (1.0f - (1.0f - colorA) * (1.0f - depthMask))* _Blend + colorA * (1 - _Blend);//colorA * 5.6;// 1.0f - (1.0f - colorA) * (1.0f - depthMask);
		}


			half4 fragAdd(v2f i) : SV_Target{

				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

		//half4 colorA = tex2D(_MainTex, i.uv.xy);
		half4 colorA = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy);
#if !UNITY_UV_STARTS_AT_TOP
		//half4 colorB = tex2D(_ColorBuffer, i.uv1.xy);
		half4 colorB = SAMPLE_TEXTURE2D(_ColorBuffer, sampler_ColorBuffer, i.uv.xy); //v0.1 - i.uv1.xy
#else
		//half4 colorB = tex2D(_ColorBuffer, i.uv.xy);
		half4 colorB = SAMPLE_TEXTURE2D(_ColorBuffer, sampler_ColorBuffer, i.uv.xy);
#endif
		half4 depthMask = saturate(colorB * _SunColor);







		////// NOISE
		//Get the colors at the right point on the first texture
		half4 col = SAMPLE_TEXTURE2D(_MainTexFBM, sampler_MainTexFBM, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw); //tex2D(_MainTexFBM, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw);
		
		//Use that to create an offset for the second texture
		//float2 offset = float2(_Distort*(col.x - .5), _Distort*(col.y - .5));
		//float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.001 * cloudSpeed.x, _Time.y * 0.001 * cloudSpeed.y);
		float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.25 * cloudSpeed.x, _Time.y * 0.011 * cloudSpeed.y) * 1;

		//Get the colors from the second texture, using the offset to distort the image
		half4 col2 = SAMPLE_TEXTURE2D(_Tex2, sampler_Tex2, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset); //tex2D(_Tex2, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset);
		
		//return pow(depthMask,3.5);
		float4 output1 = colorA + brightnessContrast.x*0.45*depthMask + brightnessContrast.y*0.75*col.r * col2.r * pow(depthMask, 3.5* brightnessContrast.z);//brightnessContrast
		return (output1)*_Blend + colorA * (1 - _Blend);





		return  colorA + depthMask + col.r * col2.r * depthMask *1;
		//Create a circular mask: if we're close to the edge the value is 0
		//If we're by the center the value is 1
		//By multipling the final alpha by this, we mask the edges of the box
		float radA = max(1 - max(length(half2(.5, .5) - i.uv.xy) - .25, 0) / .25, 0);

		//Get the mask color from our mask texture
		//half4 mask = tex2D(_MaskTex, i.uv.xy * _MaskTex_ST.xy + _MaskTex_ST.zw);

		//Add the color portion : apply the gradient from the highlight to the color
		//To the gray value from the blue channel of the distorted noise
		float3 final_color = lerp(_HighLight, _noiseColor, col2.b * .5).rgb;

		//calculate the final alpha value:
		//First combine several of the distorted noises together
		float final_alpha = col2.a * col2.g * col.b;

		//Apply the a combination of two tendril masks
		//final_alpha *= mask.g * mask.r;//

		//Apply the circular mask
		final_alpha *= radA;

		//Raise it to a power to dim it a bit 
		//it should be between 0 and 1, so the higher the power
		//the more transparent it becomes
		final_alpha = pow(final_alpha, _Pow);

		//Finally, makes sure its never more than 90% opaque
		final_alpha = min(final_alpha, .9);

		//v0.1
		final_color = pow(final_color, 5);

		//We're done! Return the final pixel color!
		float4 finalOUT = float4(final_color, final_alpha);

		finalOUT = pow(finalOUT, brightnessContrast.y) * brightnessContrast.x;



		/*
		float3 pos = (i.pos - _WorldSpaceCameraPos) * 1;
		float2 newUVA = float2(0, 0);
		newUVA.x = 0.5 + atan2(pos.z, pos.x) / (PI * 2);
		newUVA.y = 0.5 - asin(pos.y) / PI;

		float2 dx = ddx(newUVA);
		float2 dy = ddy(newUVA);
		float2 du = float2(dx.x, dy.x);
		du -= (abs(du) > 0.5f) * sign(du);
		dx.x = du.x;
		dy.x = du.y;

		// In case you want to rotate your view using the texture x-offset.
		newUVA.x += _MainTexFBM_ST.z;

		// Sample the texture with our calculated UV & seam fixup.
	//	float4 col = SAMPLE_TEXTURE2D_GRAD(_MainTexFBM, sampler_MainTexFBM, newUVA, dx, dy);// tex2Dgrad(_MainTexFBM, newUVA, dx, dy);//



		float2 UVs = newUVA;// i.uv.xy;// -_WorldSpaceCameraPos.xz * 1.4;
		//sunScreenPosition

		///// NOISE
				//Get the colors at the right point on the first texture
		half4 col = SAMPLE_TEXTURE2D(_MainTexFBM, sampler_MainTexFBM, UVs * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw); //tex2D(_MainTexFBM, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw);

		//Use that to create an offset for the second texture
		//float2 offset = float2(_Distort*(col.x - .5), _Distort*(col.y - .5));
		//float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.001 * cloudSpeed.x, _Time.y * 0.001 * cloudSpeed.y);
		float2 offset = float2(_Distort * (col.x - .5), _Distort * (col.y - .5)) + float2(_Time.y * 0.101 * 1, _Time.y * 0.011 * 1);

		col = SAMPLE_TEXTURE2D(_MainTexFBM, sampler_MainTexFBM, UVs * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset);
		col = SAMPLE_TEXTURE2D_GRAD(_MainTexFBM, sampler_MainTexFBM, UVs + offset, dx, dy);
		return colorA + col * depthMask;
	//
		//Get the colors from the second texture, using the offset to distort the image
		//half4 col2 = SAMPLE_TEXTURE2D(_Tex2, sampler_Tex2, UVs * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset); //tex2D(_Tex2, i.uv.xy * _MainTexFBM_ST.xy + _MainTexFBM_ST.zw + offset);
		float4 col2 = SAMPLE_TEXTURE2D_GRAD(_Tex2, sampler_Tex2, UVs + offset, dx, dy);
		//float4 col2 = SAMPLE_TEXTURE2D(_Tex2, sampler_Tex2, newUVA);

		//Create a circular mask: if we're close to the edge the value is 0
		//If we're by the center the value is 1
		//By multipling the final alpha by this, we mask the edges of the box
		float radA = max(1 - max(length(half2(.5, .5) - UVs) - .25, 0) / .25, 0);

		//Get the mask color from our mask texture
		//half4 mask = tex2D(_MaskTex, i.uv.xy * _MaskTex_ST.xy + _MaskTex_ST.zw);

		//Add the color portion : apply the gradient from the highlight to the color
		//To the gray value from the blue channel of the distorted noise
		float3 final_color = lerp(_HighLight, _noiseColor, col2.b * .5).rgb;

		//calculate the final alpha value:
		//First combine several of the distorted noises together
		float final_alpha = col2.a * col2.g * col.b;

		//Apply the a combination of two tendril masks
		//final_alpha *= mask.g * mask.r;//

		//Apply the circular mask
		final_alpha *= radA;

		//Raise it to a power to dim it a bit
		//it should be between 0 and 1, so the higher the power
		//the more transparent it becomes
		final_alpha = pow(final_alpha, _Pow);

		//Finally, makes sure its never more than 90% opaque
		final_alpha = min(final_alpha, .9);

		//v0.1
		final_color = pow(final_color, 5);

		//We're done! Return the final pixel color!
		float4 finalOUT = float4(final_color, final_alpha);

		finalOUT = pow(finalOUT, brightnessContrast.y) * brightnessContrast.x;
		*/
		return depthMask;
		return  colorA + 0.12* depthMask + 0.7* finalOUT * pow(depthMask,5);











		return 1 * colorA + depthMask;
		}

			struct Attributes
		{
			float4 positionOS       : POSITION;
			float2 uv               : TEXCOORD0;
		};

		v2f vert(Attributes v) {//v2f vert(AttributesDefault v) { //appdata_img v) {
								//v2f o;
			v2f o = (v2f)0;
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

			//VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
			//o.pos = vertexInput.positionCS;
			//o.uv = v.uv;
			//Varyings output = (Varyings)0;
			//UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
			VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
			//output.vertex = vertexInput.positionCS;
			//output.uv = input.uv;
			//return output;


			//o.pos = UnityObjectToClipPos(v.vertex);
			//	o.pos = float4(v.vertex.xy, 0.0, 1.0);
			//	float2 uv = TransformTriangleVertexToUV(v.vertex.xy);

			o.pos = float4(vertexInput.positionCS.xy, 0.0, 1.0);
			float2 uv = v.uv;

			//o.uv = uv;// v.texcoord.xy;

			//o.uv1 = uv.xy;



			//// NEW 1
			//o.pos = float4(v.positionOS.xy, 0.0, 1.0);
			//uv = TransformTriangleVertexToUV(v.positionOS.xy);

#if !UNITY_UV_STARTS_AT_TOP
			uv = uv * float2(1.0, -1.0) + float2(0.0, 1.0);
			//uv.y = 1-uv.y;
#endif

			o.uv = uv;// v.texcoord.xy;

#if !UNITY_UV_STARTS_AT_TOP
			o.uv = uv.xy;//o.uv1 = uv.xy;
			if (_MainTexA_TexelSize.y < 0)
				o.uv.y = 1 - o.uv.y;//o.uv1.y = 1 - o.uv1.y;
#endif	



			o.pos = TransformObjectToHClip(v.positionOS.xyz);


			float3 circle_worldPos = _SunPosition.xyz;
			// this avoids the extra line of code to subtract the world space camera position
			float4 circle_cameraPos = mul(unity_WorldToCamera, float4(circle_worldPos, 1.0));

			// if behind the camera, ignore
			//if (circle_cameraPos.z <= 0.0)
			//	return tex;

			// the WorldToCamera matrix is +z forwad, but the projection matrix expects a -z forward view space
			circle_cameraPos.z = -circle_cameraPos.z;

			// transform view space to clip space position
			float4 circle_clipPos = mul(unity_CameraProjection, circle_cameraPos);

			// clip space has a -w to +w range for on screen elements, so divide x and y by w to get a -1 to +1 range
			// then multiply by 0.5 and 0.5 to bring from a -1 to +1 range to 0.0 to 1.0 screen position UV
			float2 circle_screenPos = (circle_clipPos.xy / circle_clipPos.w) * 0.5 + 0.5;

			//circle_screenPos.y = 1 - circle_screenPos.y;

			o.screenPos.xy = circle_screenPos;// ComputeScreenPos(o.pos);







			o.sunScreenPosition = circle_screenPos;//  worldToScreenPosition(_SunPosition.xyz);
			o.screenPos.xy = o.sunScreenPosition;
			o.uv = UnityStereoTransformScreenSpaceTex(v.uv);

			/*#if UNITY_UV_STARTS_AT_TOP
					if (_ColorTexture_TexelSize.y < 0)
					{
						o.uv.y = 1 - o.uv.y;
					}
			#endif*/
			o.uv.y = 1 - o.uv.y;

			return o;
		}


		v2f_radial vert_radial(Attributes v) {//v2f_radial vert_radial(AttributesDefault v) { //appdata_img v) {
			//v2f_radial o;

			v2f_radial o = (v2f_radial)0;
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
			////		o.pos = UnityObjectToClipPos(v.vertex);

			//o.pos = float4(v.vertex.xyz,1);
			//o.pos = float4(v.vertex.xy, 0.0, 1.0);
			//float2 uv = TransformTriangleVertexToUV(v.vertex.xy);

			VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
			o.pos = float4(vertexInput.positionCS.xy, 0.0, 1.0);
			float2 uv = v.uv;
			//output.vertex = vertexInput.positionCS;

			//uv = TransformTriangleVertexToUV(vertexInput.positionCS.xy);

#if !UNITY_UV_STARTS_AT_TOP
		//uv = uv * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif


//uv.y += 0.5;
//uv.y *= 1.25;



			o.uv.xy = uv;//v.texcoord.xy;
						 //o.blurVector = (_SunPosition.xy - v.texcoord.xy) * _BlurRadius4.xy;
			//o.uv1 = uv.xy;
			//o.uv.y = 1 - o.uv.y;
			//uv.y = 1 - uv.y;
			//o.uv.y = 1 - o.uv.y;
			//_SunPosition.y = _SunPosition.y*0.5 + 0.5;
			//_SunPosition.x = _SunPosition.x*0.5 + 0.5;
			//uv.x += 0.5;
			//uv.x *= 0.5+0.5;
			//uv.y *= 1.2;
			//uv = uv * float2(1.0, -1.0) + float2(0.0, 1.0);
			o.blurVector = (_SunPosition.xy - uv.xy) * _BlurRadius4.xy;

			o.pos = TransformObjectToHClip(v.positionOS.xyz);


			float3 circle_worldPos = _SunPosition.xyz;
			// this avoids the extra line of code to subtract the world space camera position
			float4 circle_cameraPos = mul(unity_WorldToCamera, float4(circle_worldPos, 1.0));

			// if behind the camera, ignore
			//if (circle_cameraPos.z <= 0.0)
			//	return tex;

			// the WorldToCamera matrix is +z forwad, but the projection matrix expects a -z forward view space
			circle_cameraPos.z = -circle_cameraPos.z;

			// transform view space to clip space position
			float4 circle_clipPos = mul(unity_CameraProjection, circle_cameraPos);

			// clip space has a -w to +w range for on screen elements, so divide x and y by w to get a -1 to +1 range
			// then multiply by 0.5 and 0.5 to bring from a -1 to +1 range to 0.0 to 1.0 screen position UV
			float2 circle_screenPos = (circle_clipPos.xy / circle_clipPos.w) * 0.5 + 0.5;
			//circle_screenPos.y = 1 - circle_screenPos.y;


			half2 sunScreenPosition = circle_screenPos;// worldToScreenPosition(_SunPosition.xyz + _WorldSpaceCameraPos * 0);
			o.sunScreenPosition = sunScreenPosition.xy;// ComputeScreenPos(o.pos);//    sunScreenPosition;

			o.uv = UnityStereoTransformScreenSpaceTex(v.uv);



			o.blurVector = (sunScreenPosition.xy - o.uv.xy) * _BlurRadius4.xy;

			return o;
		}

		half4 frag_radial(v2f_radial i) : SV_Target
		{
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

			half4 color = half4(0,0,0,0);
			for (int j = 0; j < SAMPLES_INT; j++)
			{
				//half4 tmpColor = tex2D(_MainTex, i.uv.xy);
				half4 tmpColor = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy);
				color += tmpColor;
				i.uv.xy += i.blurVector;
			}
			return color / SAMPLES_FLOAT;
		}

			half TransformColor(half4 skyboxValue) {
			return dot(max(skyboxValue.rgb - _SunThreshold.rgb, half3(0, 0, 0)), half3(1, 1, 1)); // threshold and convert to greyscale
		}

		half4 frag_depth(v2f i) : SV_Target{

			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

		#if !UNITY_UV_STARTS_AT_TOP
		//float depthSample = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv1.xy);
		float depthSample = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv.xy), _ZBufferParams); //v0.1 URP i.uv1.xy
#else
		//float depthSample = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv.xy);
		float depthSample = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv.xy), _ZBufferParams);
#endif

		//half4 tex = tex2D(_MainTex, i.uv.xy);
		half4 tex = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy);
		//depthSample = Linear01Depth(depthSample, _ZBufferParams);

		//i.uv.x += 0.5;
		//i.uv.x *= 1.25;

		// consider maximum radius
	#if !UNITY_UV_STARTS_AT_TOP
		half2 vec = i.sunScreenPosition.xy - i.uv.xy;  // _SunPosition.xy - i.uv.xy; //i.uv1.xy;
	#else
		half2 vec = i.sunScreenPosition.xy - i.uv.xy; //_SunPosition.xy - i.uv.xy;
	#endif
		half dist = saturate(_SunPosition.w - length(vec.xy));

		half4 outColor = 0;

		// consider shafts blockers
		//if (depthSample > 0.99)
		//if (depthSample > 0.103)
		if (depthSample > 1 - 0.018) {//if (depthSample < 0.018) {
			//outColor = TransformColor(tex) * dist;
		}





#if !UNITY_UV_STARTS_AT_TOP
		if (depthSample < 0.018) {
			outColor = TransformColor(tex) * dist;
		}
#else
		if (depthSample > 1 - 0.018) {
			outColor = TransformColor(tex) * dist;
		}
#endif

		return outColor * 1;
		}

			//inline half Luminance(half3 rgb)
			//{
				//return dot(rgb, unity_ColorSpaceLuminance.rgb);
			//	return dot(rgb, rgb);
			//}

			half4 frag_nodepth(v2f i) : SV_Target{

				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

			#if !UNITY_UV_STARTS_AT_TOP
		//float4 sky = (tex2D(_Skybox, i.uv1.xy));
		float4 sky = SAMPLE_TEXTURE2D(_Skybox, sampler_Skybox, i.uv.xy);
#else
		//float4 sky = (tex2D(_Skybox, i.uv.xy));
		float4 sky = SAMPLE_TEXTURE2D(_Skybox, sampler_Skybox, i.uv.xy); //i.uv1.xy;
#endif




		//float4 tex = (tex2D(_MainTex, i.uv.xy));
		half4 tex = SAMPLE_TEXTURE2D(_MainTexA, sampler_MainTexA, i.uv.xy);


		//i.uv.x += 0.5;
		//i.uv.x *= 1.25;
		//sky = float4(0.3, 0.05, 0.05,  1);
		/// consider maximum radius
#if !UNITY_UV_STARTS_AT_TOP
		half2 vec = i.sunScreenPosition.xy - i.uv.xy;
#else
		half2 vec = i.sunScreenPosition.xy - i.uv.xy;//i.uv1.xy;
#endif
		half dist = saturate(_SunPosition.w - length(vec));

		half4 outColor = 0;

		// find unoccluded sky pixels
		// consider pixel values that differ significantly between framebuffer and sky-only buffer as occluded


		if (Luminance(abs(sky.rgb - tex.rgb)) < 0.2) {
			outColor = TransformColor(tex) * dist;
			//outColor = TransformColor(sky) * dist;
		}

		return outColor * 1;
		}

			ENDHLSL

			//		SubShader
			//	{
			//		//Cull Off ZWrite Off ZTest Always
			//
			//			Pass
			//		{
			//			HLSLPROGRAM
			//
			//#pragma vertex VertDefault
			//#pragma fragment Frag
			//
			//			ENDHLSL
			//		}
			//	}
			Subshader {
			//Tags{ "RenderType" = "Opaque" }
			Pass{
			ZTest Always Cull Off ZWrite Off

			HLSLPROGRAM

#pragma vertex vert
#pragma fragment fragScreen

			ENDHLSL
			}

				Pass{
				ZTest Always Cull Off ZWrite Off

				HLSLPROGRAM

	#pragma vertex vert_radial
	#pragma fragment frag_radial

				ENDHLSL
			}

				Pass{
				ZTest Always Cull Off ZWrite Off

				HLSLPROGRAM

	#pragma vertex vert
	#pragma fragment frag_depth

				ENDHLSL
			}

				Pass{
				ZTest Always Cull Off ZWrite Off

				HLSLPROGRAM

	#pragma vertex vert
	#pragma fragment frag_nodepth

				ENDHLSL
			}

				Pass{
				ZTest Always Cull Off ZWrite Off

				HLSLPROGRAM

	#pragma vertex vert
	#pragma fragment fragAdd

				ENDHLSL
			}


				//PASS5
				Pass{
				ZTest Always Cull Off ZWrite Off

				HLSLPROGRAM

	#pragma vertex vert
	#pragma fragment FragGrey

				ENDHLSL
			}


		}
}