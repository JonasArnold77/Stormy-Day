// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33371,y:32266,varname:node_4795,prsc:2|emission-9156-OUT;n:type:ShaderForge.SFN_Fresnel,id:4617,x:32318,y:32238,varname:node_4617,prsc:2|EXP-7084-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7084,x:32036,y:32280,ptovrint:False,ptlb:Fresnel Exp,ptin:_FresnelExp,varname:node_7084,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9156,x:32922,y:32336,varname:node_9156,prsc:2|A-801-OUT,B-5731-RGB,C-7060-OUT,D-2393-OUT;n:type:ShaderForge.SFN_Color,id:5731,x:32555,y:32456,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5731,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:801,x:32555,y:32291,varname:node_801,prsc:2|A-4617-OUT,B-4328-OUT;n:type:ShaderForge.SFN_DepthBlend,id:6421,x:31869,y:32442,varname:node_6421,prsc:2|DIST-2802-OUT;n:type:ShaderForge.SFN_OneMinus,id:8773,x:32036,y:32442,varname:node_8773,prsc:2|IN-6421-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:9762,x:32036,y:32000,varname:node_9762,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:6639,x:32036,y:31797,ptovrint:False,ptlb:Top Point,ptin:_TopPoint,varname:node_6639,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:25;n:type:ShaderForge.SFN_ValueProperty,id:6960,x:32036,y:31719,ptovrint:False,ptlb:Bot Point,ptin:_BotPoint,varname:_TopPoint_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Vector1,id:2740,x:32036,y:31861,varname:node_2740,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3240,x:32036,y:31929,varname:node_3240,prsc:2,v1:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:84,x:32331,y:31835,varname:node_84,prsc:2|IN-9762-Y,IMIN-6960-OUT,IMAX-6639-OUT,OMIN-3240-OUT,OMAX-2740-OUT;n:type:ShaderForge.SFN_Clamp01,id:7060,x:32491,y:31835,varname:node_7060,prsc:2|IN-84-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2802,x:31684,y:32442,ptovrint:False,ptlb:Depth Dist,ptin:_DepthDist,varname:node_2802,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:4328,x:32318,y:32373,varname:node_4328,prsc:2|VAL-8773-OUT,EXP-292-OUT;n:type:ShaderForge.SFN_ValueProperty,id:292,x:32036,y:32591,ptovrint:False,ptlb:Depth Power,ptin:_DepthPower,varname:node_292,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Slider,id:2393,x:32398,y:32628,ptovrint:False,ptlb:Final Power,ptin:_FinalPower,varname:node_2393,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:4;proporder:7084-5731-6639-6960-2802-292-2393;pass:END;sub:END;*/

Shader "SineVFX/MeshPacks/NewSandForceField" {
    Properties {
        _FresnelExp ("Fresnel Exp", Float ) = 1
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _TopPoint ("Top Point", Float ) = 25
        _BotPoint ("Bot Point", Float ) = 0
        _DepthDist ("Depth Dist", Float ) = 1
        _DepthPower ("Depth Power", Float ) = 1
        _FinalPower ("Final Power", Range(0, 4)) = 1
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
            Cull Off
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
            uniform sampler2D _CameraDepthTexture;
            uniform float _FresnelExp;
            uniform float4 _Color;
            uniform float _TopPoint;
            uniform float _BotPoint;
            uniform float _DepthDist;
            uniform float _DepthPower;
            uniform float _FinalPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float node_3240 = 1.0;
                float3 emissive = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)+pow((1.0 - saturate((sceneZ-partZ)/_DepthDist)),_DepthPower))*_Color.rgb*saturate((node_3240 + ( (i.posWorld.g - _BotPoint) * (0.0 - node_3240) ) / (_TopPoint - _BotPoint)))*_FinalPower);
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
