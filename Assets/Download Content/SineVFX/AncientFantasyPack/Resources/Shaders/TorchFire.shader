// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32810,y:32685,varname:node_4795,prsc:2|emission-2152-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31384,y:33064,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Tex2dAsset,id:8824,x:30108,y:32394,ptovrint:False,ptlb:Fire Texture,ptin:_FireTexture,varname:node_8824,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:9082,x:30108,y:32575,varname:node_9082,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:6050,x:30520,y:32691,varname:node_6050,prsc:2,spu:0,spv:-0.25|UVIN-9082-UVOUT;n:type:ShaderForge.SFN_Add,id:5574,x:30967,y:32633,varname:node_5574,prsc:2|A-2576-OUT,B-6050-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2576,x:30726,y:32466,varname:node_2576,prsc:2|A-8242-R,B-1139-OUT;n:type:ShaderForge.SFN_Slider,id:1139,x:30374,y:32514,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_1139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:7459,x:31242,y:32474,varname:node_7459,prsc:2,ntxv:0,isnm:False|UVIN-5574-OUT,TEX-8824-TEX;n:type:ShaderForge.SFN_Color,id:4601,x:31289,y:32018,ptovrint:False,ptlb:Red Channel Color,ptin:_RedChannelColor,varname:node_4601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4760649,c3:0.07352942,c4:1;n:type:ShaderForge.SFN_Multiply,id:573,x:31498,y:32074,varname:node_573,prsc:2|A-4601-RGB,B-6431-OUT;n:type:ShaderForge.SFN_Vector1,id:6431,x:31289,y:32175,varname:node_6431,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:9948,x:31980,y:32211,varname:node_9948,prsc:2|A-573-OUT,B-7142-OUT,C-4269-OUT;n:type:ShaderForge.SFN_Multiply,id:4269,x:31766,y:32354,varname:node_4269,prsc:2|A-573-OUT,B-2053-R;n:type:ShaderForge.SFN_Add,id:1289,x:32172,y:32551,varname:node_1289,prsc:2|A-9948-OUT,B-6055-OUT;n:type:ShaderForge.SFN_Color,id:170,x:31579,y:33255,ptovrint:False,ptlb:Vertex Channel Color,ptin:_VertexChannelColor,varname:node_170,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.7109534,c3:0.4485294,c4:1;n:type:ShaderForge.SFN_Multiply,id:2261,x:31819,y:33322,varname:node_2261,prsc:2|A-170-RGB,B-6843-OUT;n:type:ShaderForge.SFN_Vector1,id:6843,x:31579,y:33410,varname:node_6843,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:3249,x:32007,y:33369,varname:node_3249,prsc:2|A-2261-OUT,B-2053-G;n:type:ShaderForge.SFN_Multiply,id:6055,x:32216,y:33401,varname:node_6055,prsc:2|A-3249-OUT,B-7459-G;n:type:ShaderForge.SFN_Multiply,id:2152,x:32550,y:32653,varname:node_2152,prsc:2|A-6500-OUT,B-2053-A,C-4398-OUT,D-5805-OUT;n:type:ShaderForge.SFN_Slider,id:4398,x:31227,y:33215,ptovrint:False,ptlb:Final Power,ptin:_FinalPower,varname:node_4398,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:4;n:type:ShaderForge.SFN_Tex2d,id:8242,x:30108,y:32206,ptovrint:False,ptlb:Distortion Texture,ptin:_DistortionTexture,varname:node_8242,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:142,x:31527,y:32644,varname:node_142,prsc:2|A-7459-R,B-6299-B,C-6533-OUT;n:type:ShaderForge.SFN_Tex2d,id:6299,x:31242,y:32632,varname:node_6299,prsc:2,ntxv:0,isnm:False|UVIN-4938-OUT,TEX-8824-TEX;n:type:ShaderForge.SFN_Panner,id:1650,x:30520,y:32849,varname:node_1650,prsc:2,spu:0,spv:-0.5|UVIN-9082-UVOUT;n:type:ShaderForge.SFN_Add,id:4938,x:30967,y:32826,varname:node_4938,prsc:2|A-2576-OUT,B-1650-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:3123,x:32251,y:32813,varname:node_3123,prsc:2;n:type:ShaderForge.SFN_Vector1,id:6533,x:31242,y:32771,varname:node_6533,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:3227,x:31527,y:32506,varname:node_3227,prsc:2|A-457-OUT,B-7459-R;n:type:ShaderForge.SFN_Vector1,id:457,x:31242,y:32402,varname:node_457,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:7142,x:31724,y:32579,varname:node_7142,prsc:2|A-3227-OUT,B-142-OUT;n:type:ShaderForge.SFN_Clamp01,id:6500,x:32347,y:32551,varname:node_6500,prsc:2|IN-1289-OUT;n:type:ShaderForge.SFN_OneMinus,id:5805,x:32405,y:32813,varname:node_5805,prsc:2|IN-3123-OUT;proporder:8824-1139-4601-170-4398-8242;pass:END;sub:END;*/

Shader "SineVFX/MeshPacks/TorchFire" {
    Properties {
        _FireTexture ("Fire Texture", 2D) = "white" {}
        _Distortion ("Distortion", Range(0, 1)) = 0
        _RedChannelColor ("Red Channel Color", Color) = (1,0.4760649,0.07352942,1)
        _VertexChannelColor ("Vertex Channel Color", Color) = (1,0.7109534,0.4485294,1)
        _FinalPower ("Final Power", Range(0, 4)) = 0
        _DistortionTexture ("Distortion Texture", 2D) = "white" {}
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
            uniform sampler2D _FireTexture; uniform float4 _FireTexture_ST;
            uniform float _Distortion;
            uniform float4 _RedChannelColor;
            uniform float4 _VertexChannelColor;
            uniform float _FinalPower;
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 node_573 = (_RedChannelColor.rgb*2.0);
                float4 _DistortionTexture_var = tex2D(_DistortionTexture,TRANSFORM_TEX(i.uv0, _DistortionTexture));
                float node_2576 = (_DistortionTexture_var.r*_Distortion);
                float4 node_4649 = _Time;
                float2 node_5574 = (node_2576+(i.uv0+node_4649.g*float2(0,-0.25)));
                float4 node_7459 = tex2D(_FireTexture,TRANSFORM_TEX(node_5574, _FireTexture));
                float2 node_4938 = (node_2576+(i.uv0+node_4649.g*float2(0,-0.5)));
                float4 node_6299 = tex2D(_FireTexture,TRANSFORM_TEX(node_4938, _FireTexture));
                float3 emissive = (saturate(((node_573*((1.0*node_7459.r)+(node_7459.r*node_6299.b*3.0))*(node_573*i.vertexColor.r))+(((_VertexChannelColor.rgb*2.0)*i.vertexColor.g)*node_7459.g)))*i.vertexColor.a*_FinalPower*(1.0 - (1.0-max(0,dot(normalDirection, viewDirection)))));
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
