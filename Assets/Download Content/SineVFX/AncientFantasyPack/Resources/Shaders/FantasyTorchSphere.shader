// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33382,y:32673,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:33088,y:32872,varname:node_2393,prsc:2|A-797-RGB,B-2176-OUT,C-9707-OUT,D-640-OUT;n:type:ShaderForge.SFN_Color,id:797,x:32221,y:32748,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2688,x:32221,y:32902,varname:node_2688,prsc:2|EXP-1698-OUT;n:type:ShaderForge.SFN_Slider,id:2176,x:32064,y:33150,ptovrint:False,ptlb:Final Power,ptin:_FinalPower,varname:node_2176,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:4;n:type:ShaderForge.SFN_Slider,id:1698,x:32064,y:33053,ptovrint:False,ptlb:Fresnel Exp,ptin:_FresnelExp,varname:node_1698,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:3,max:6;n:type:ShaderForge.SFN_FragmentPosition,id:8870,x:30803,y:33128,varname:node_8870,prsc:2;n:type:ShaderForge.SFN_Append,id:6219,x:30982,y:33128,varname:node_6219,prsc:2|A-8870-Y,B-8870-Z;n:type:ShaderForge.SFN_Tex2d,id:3691,x:31448,y:33035,ptovrint:False,ptlb:Noise 01,ptin:_Noise01,varname:node_3691,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2795-UVOUT;n:type:ShaderForge.SFN_Panner,id:2795,x:31256,y:33035,varname:node_2795,prsc:2,spu:-0.1,spv:0|UVIN-6219-OUT;n:type:ShaderForge.SFN_Fresnel,id:9860,x:31459,y:33478,varname:node_9860,prsc:2|EXP-2937-OUT;n:type:ShaderForge.SFN_Vector1,id:2937,x:31459,y:33611,varname:node_2937,prsc:2,v1:2.5;n:type:ShaderForge.SFN_Add,id:3474,x:32532,y:33092,varname:node_3474,prsc:2|A-2688-OUT,B-6165-OUT;n:type:ShaderForge.SFN_Panner,id:9556,x:31256,y:33214,varname:node_9556,prsc:2,spu:-0.2,spv:0|UVIN-6219-OUT;n:type:ShaderForge.SFN_Tex2d,id:7097,x:31448,y:33214,ptovrint:False,ptlb:Noise 02,ptin:_Noise02,varname:_Noise02,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9556-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6165,x:32221,y:33242,varname:node_6165,prsc:2|A-3691-R,B-7097-R,C-9650-OUT,D-9860-OUT;n:type:ShaderForge.SFN_Slider,id:9650,x:31291,y:33400,ptovrint:False,ptlb:1mmmm,ptin:_1mmmm,varname:node_9650,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:6;n:type:ShaderForge.SFN_Clamp01,id:9707,x:32700,y:33092,varname:node_9707,prsc:2|IN-3474-OUT;n:type:ShaderForge.SFN_NormalVector,id:2451,x:32236,y:33427,prsc:2,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:9283,x:32398,y:33427,varname:node_9283,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-2451-OUT;n:type:ShaderForge.SFN_OneMinus,id:2240,x:32560,y:33427,varname:node_2240,prsc:2|IN-9283-OUT;n:type:ShaderForge.SFN_Clamp01,id:640,x:32721,y:33427,varname:node_640,prsc:2|IN-2240-OUT;proporder:797-2176-1698-3691-7097-9650;pass:END;sub:END;*/

Shader "SineVFX/MeshPacks/FantasyTorchSphere" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _FinalPower ("Final Power", Range(0, 4)) = 0
        _FresnelExp ("Fresnel Exp", Range(1, 6)) = 3
        _Noise01 ("Noise 01", 2D) = "white" {}
        _Noise02 ("Noise 02", 2D) = "white" {}
        _1mmmm ("1mmmm", Range(0, 6)) = 4
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
            uniform float4 _TintColor;
            uniform float _FinalPower;
            uniform float _FresnelExp;
            uniform sampler2D _Noise01; uniform float4 _Noise01_ST;
            uniform sampler2D _Noise02; uniform float4 _Noise02_ST;
            uniform float _1mmmm;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
                float4 node_8769 = _Time;
                float2 node_6219 = float2(i.posWorld.g,i.posWorld.b);
                float2 node_2795 = (node_6219+node_8769.g*float2(-0.1,0));
                float4 _Noise01_var = tex2D(_Noise01,TRANSFORM_TEX(node_2795, _Noise01));
                float2 node_9556 = (node_6219+node_8769.g*float2(-0.2,0));
                float4 _Noise02_var = tex2D(_Noise02,TRANSFORM_TEX(node_9556, _Noise02));
                float3 emissive = (_TintColor.rgb*_FinalPower*saturate((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)+(_Noise01_var.r*_Noise02_var.r*_1mmmm*pow(1.0-max(0,dot(normalDirection, viewDirection)),2.5))))*saturate((1.0 - i.normalDir.g)));
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
