// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31574,y:32701,ptovrint:False,ptlb:Noise 01,ptin:_Noise01,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9048-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-366-OUT,B-2053-RGB,C-797-RGB,D-5382-OUT,E-2425-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7989,x:31574,y:32881,ptovrint:False,ptlb:Noise 02,ptin:_Noise02,varname:_Noise02,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3938-UVOUT;n:type:ShaderForge.SFN_Panner,id:9048,x:31347,y:32678,varname:node_9048,prsc:2,spu:0,spv:-0.05|UVIN-9136-OUT;n:type:ShaderForge.SFN_Panner,id:3938,x:31347,y:32844,varname:node_3938,prsc:2,spu:0,spv:-0.1|UVIN-9136-OUT;n:type:ShaderForge.SFN_Multiply,id:366,x:31839,y:32792,varname:node_366,prsc:2|A-6074-R,B-7989-R,C-7456-R;n:type:ShaderForge.SFN_TexCoord,id:8901,x:30166,y:32602,varname:node_8901,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3569,x:30568,y:32445,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_3569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4171-UVOUT;n:type:ShaderForge.SFN_Slider,id:6827,x:30166,y:32773,ptovrint:False,ptlb:Distortion Amount,ptin:_DistortionAmount,varname:node_6827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:0.4;n:type:ShaderForge.SFN_Panner,id:4171,x:30402,y:32445,varname:node_4171,prsc:2,spu:0,spv:-0.2|UVIN-8901-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7456,x:31574,y:33074,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_7456,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:5382,x:32078,y:33094,ptovrint:False,ptlb:Final Power,ptin:_FinalPower,varname:node_5382,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:40;n:type:ShaderForge.SFN_Append,id:9136,x:31060,y:32759,varname:node_9136,prsc:2|A-8901-U,B-8581-OUT;n:type:ShaderForge.SFN_Lerp,id:8581,x:30843,y:32586,varname:node_8581,prsc:2|A-8901-V,B-3569-R,T-6827-OUT;n:type:ShaderForge.SFN_Fresnel,id:3903,x:31436,y:33265,varname:node_3903,prsc:2|EXP-7110-OUT;n:type:ShaderForge.SFN_OneMinus,id:2425,x:31593,y:33265,varname:node_2425,prsc:2|IN-3903-OUT;n:type:ShaderForge.SFN_Vector1,id:7110,x:31267,y:33285,varname:node_7110,prsc:2,v1:2;proporder:6074-797-7989-3569-6827-7456-5382;pass:END;sub:END;*/

Shader "SineVFX/MeshPacks/WarmthCupVfx" {
    Properties {
        _Noise01 ("Noise 01", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Noise02 ("Noise 02", 2D) = "white" {}
        _Distortion ("Distortion", 2D) = "white" {}
        _DistortionAmount ("Distortion Amount", Range(0, 0.4)) = 0.4
        _Mask ("Mask", 2D) = "white" {}
        _FinalPower ("Final Power", Range(0, 40)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Noise01; uniform float4 _Noise01_ST;
            uniform float4 _TintColor;
            uniform sampler2D _Noise02; uniform float4 _Noise02_ST;
            uniform sampler2D _Distortion; uniform float4 _Distortion_ST;
            uniform float _DistortionAmount;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _FinalPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_3735 = _Time;
                float2 node_4171 = (i.uv0+node_3735.g*float2(0,-0.2));
                float4 _Distortion_var = tex2D(_Distortion,TRANSFORM_TEX(node_4171, _Distortion));
                float2 node_9136 = float2(i.uv0.r,lerp(i.uv0.g,_Distortion_var.r,_DistortionAmount));
                float2 node_9048 = (node_9136+node_3735.g*float2(0,-0.05));
                float4 _Noise01_var = tex2D(_Noise01,TRANSFORM_TEX(node_9048, _Noise01));
                float2 node_3938 = (node_9136+node_3735.g*float2(0,-0.1));
                float4 _Noise02_var = tex2D(_Noise02,TRANSFORM_TEX(node_3938, _Noise02));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 emissive = ((_Noise01_var.r*_Noise02_var.r*_Mask_var.r)*i.vertexColor.rgb*_TintColor.rgb*_FinalPower*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
