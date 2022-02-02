Shader "Custom/VignetteBlur"
{
    Properties
    {
        _T("% Maximum Speed", Range(0, 1)) = 0.5
        _BlurStep("Blur Step", Int) = 3 // the higher it is, the broader the box-blur samples, too large will cause fracturing images
        _Period ("Period", Range(0, 1)) = 1 // the lower it is, the more radial "shards" appear on the edge of the screen
        _Extent("Extent of Non-Blur", Range(0, 10)) = 2 // the higher it is, the larger the central, non-blurred area on the screen is
    }

    SubShader
    {

        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "VignettePass"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionHCS   : POSITION;
                float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4  positionCS  : SV_POSITION;
                float2  uv          : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                // Note: The pass is setup with a mesh already in clip
                // space, that's why, it's enough to just output vertex
                // positions
                output.positionCS = float4(input.positionHCS.xyz, 1.0);

                #if UNITY_UV_STARTS_AT_TOP
                output.positionCS.y *= -1;
                #endif

                output.uv = input.uv;
                return output;
            }

            TEXTURE2D_X(_BlurTex);

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);
            
            float2 _CameraOpaqueTexture_TexelSize;

            float _Period;
            float _T;
            float _Extent;

            int _BlurStep;

            float hash(float n);
            float inoise(float3 x);

            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                float r = pow(length(input.uv - 0.5), _Extent);

                float theta = ( atan2(input.uv.y - 0.5, input.uv.x - 0.5) / PI + 1 ) / 2;

                float theta_st = sin((-4 * (theta) * (theta - 1)) / _Period + inoise(theta) * 5 * (theta - 1)) * 3;

                float n = inoise(float3(cos(3 * theta), 40 * r, _T));
                float theta_w = theta_st * r;

                int d = theta_w * _BlurStep + r;

                float2 dx = float2(d * _CameraOpaqueTexture_TexelSize.x, 0);
                float2 dy = float2(0, d * _CameraOpaqueTexture_TexelSize.y);

                theta_w *= n;

                // sample 9 points for box blur
                // 00 10 20
                // 01 11 21
                // 02 12 22

                float4
                    c00 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv - dx - dy),
                    c10 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv - dy),
                    c20 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv + dx - dy),
                    c01 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv - dx),
                    c11 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv),
                    c21 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv + dx),
                    c02 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv - dx + dy),
                    c12 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv + dy),
                    c22 = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv + dx + dy);

                float p = _T / 8;
                float4 c = (1 - _T) * c11 + p * (c00 + c10 + c20 + c01 + c21 + c02 + c12 + c22);

                return c;
            }

            // 
            // hash based 3d value noise
            // function taken from https://www.shadertoy.com/view/XslGRr
            // Created by inigo quilez - iq/2013
            // License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

            // ported from GLSL to HLSL

            float hash(float n)
            {
                return frac(sin(n) * 43758.5453);
            }

            float inoise(float3 x)
            {
                // The noise function returns a value in the range -1.0f -> 1.0f

                float3 p = floor(x);
                float3 f = frac(x);

                f = f * f * (3.0 - 2.0 * f);
                float n = p.x + p.y * 57.0 + 113.0 * p.z;

                return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
                    lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y),
                    lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
                        lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y), f.z);
            }

            ENDHLSL
        }
    }
}