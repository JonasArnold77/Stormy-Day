Shader "Custom/WaterStream"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Alpha("Alpha", Range( 0 , 3)) = 1
		_Speed("Speed", Float) = 1
		_Texture("Texture", 2D) = "white" {}
		_Tiling("Tiling", Vector) = (3,0.2,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float4 _Color;
		uniform float _Alpha;
		uniform sampler2D _Texture;
		uniform float _Speed;
		uniform float2 _Tiling;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = _Color.rgb;
			float2 uv_TexCoord19 = i.uv_texcoord * _Tiling;
			float2 panner18 = ( _Time.y * ( _Speed * float2( 0,1 ) ) + uv_TexCoord19);
			o.Alpha = ( _Alpha * ( tex2D( _Texture, panner18 ) * i.vertexColor ) ).r;
		}

		ENDCG
	}
	Fallback "Diffuse"
}