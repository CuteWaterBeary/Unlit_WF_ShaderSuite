﻿/*
 *  The MIT License
 *
 *  Copyright 2018-2023 whiteflare.
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 *  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 *  TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
Shader "UnlitWF_URP/WF_UnToon_Transparent_MaskOut" {

    Properties {
        // 基本
        [WFHeader(Base)]
            _MainTex                ("Main Texture", 2D) = "white" {}
        [HDR]
            _Color                  ("Color", Color) = (1, 1, 1, 1)
        [Enum(OFF,0,FRONT,1,BACK,2)]
            _CullMode               ("Cull Mode", int) = 0
        [Toggle(_)]
            _UseVertexColor         ("Use Vertex Color", Range(0, 1)) = 0

        // StencilMask
        [WFHeader(Stencil Mask)]
        [Enum(A_1000,8,B_1001,9,C_1010,10,D_1100,11)]
            _StencilMaskID          ("ID", int) = 8

        // Alpha
        [WFHeader(Transparent Alpha)]
        [Enum(MAIN_TEX_ALPHA,0,MASK_TEX_RED,1,MASK_TEX_ALPHA,2)]
            _AL_Source              ("[AL] Alpha Source", Float) = 0
        [NoScaleOffset]
            _AL_MaskTex             ("[AL] Alpha Mask Texture", 2D) = "white" {}
        [Toggle(_)]
            _AL_InvMaskVal          ("[AL] Invert Mask Value", Range(0, 1)) = 0
            _AL_Power               ("[AL] Power", Range(0, 2)) = 1.0
        [Enum(OFF,0,ON,1)]
            _AL_ZWrite              ("[AL] ZWrite", int) = 1

        // 裏面テクスチャ
        [WFHeaderToggle(BackFace Texture)]
            _BKT_Enable             ("[BKT] Enable", Float) = 0
        [Enum(UV1,0,UV2,1)]
            _BKT_UVType             ("[BKT] UV Type", Float) = 0
            _BKT_BackTex            ("[BKT] Back Texture", 2D) = "white" {}
        [HDR]
            _BKT_BackColor          ("[BKT] Back Color", Color) = (1, 1, 1, 1)

        // 3chカラーマスク
        [WFHeaderToggle(3ch Color Mask)]
            _CHM_Enable             ("[CHM] Enable", Float) = 0
        [NoScaleOffset]
            _CHM_3chMaskTex         ("[CHM] 3ch Mask Texture", 2D) = "black" {}
        [HDR]
            _CHM_ColorR             ("[CHM] R ch Color", Color) = (1, 1, 1, 1)
        [HDR]
            _CHM_ColorG             ("[CHM] G ch Color", Color) = (1, 1, 1, 1)
        [HDR]
            _CHM_ColorB             ("[CHM] B ch Color", Color) = (1, 1, 1, 1)

        // 色変換
        [WFHeaderToggle(Color Change)]
            _CLC_Enable             ("[CLC] Enable", Float) = 0
        [Toggle(_)]
            _CLC_Monochrome         ("[CLC] monochrome", Range(0, 1)) = 0
            _CLC_DeltaH             ("[CLC] Hur", Range(0, 1)) = 0
            _CLC_DeltaS             ("[CLC] Saturation", Range(-1, 1)) = 0
            _CLC_DeltaV             ("[CLC] Brightness", Range(-1, 1)) = 0

        // ノーマルマップ
        [WFHeaderToggle(NormalMap)]
            _NM_Enable              ("[NM] Enable", Float) = 0
        [NoScaleOffset]
            _BumpMap                ("[NM] NormalMap Texture", 2D) = "bump" {}
            _BumpScale              ("[NM] Bump Scale", Range(0, 2)) = 1.0
            _NM_Power               ("[NM] Shadow Power", Range(0, 1)) = 0.25
        [Enum(NONE,0,X,1,Y,2,XY,3)]
            _FlipMirror             ("[NM] Flip Mirror", Float) = 0

        // Detailノーマルマップ
        [WFHeaderToggle(Detail NormalMap)]
            _NS_Enable              ("[NS] Enable", Float) = 0
        [Enum(UV1,0,UV2,1)]
            _NS_UVType              ("[NS] 2nd Normal UV Type", Float) = 0
            _DetailNormalMap        ("[NS] 2nd NormalMap Texture", 2D) = "bump" {}
            _DetailNormalMapScale   ("[NS] 2nd Bump Scale", Range(0, 2)) = 0.4
        [NoScaleOffset]
            _NS_2ndMaskTex          ("[NS] 2nd NormalMap Mask Texture (R)", 2D) = "white" {}
        [Toggle(_)]
            _NS_InvMaskVal          ("[NS] Invert Mask Value", Range(0, 1)) = 0

        // メタリックマップ
        [WFHeaderToggle(Metallic)]
            _MT_Enable              ("[MT] Enable", Float) = 0
            _MT_Metallic            ("[MT] Metallic", Range(0, 1)) = 1
            _MT_ReflSmooth          ("[MT] Smoothness", Range(0, 1)) = 1
            _MT_Brightness          ("[MT] Brightness", Range(0, 1)) = 0.2
            _MT_BlendNormal         ("[MT] Blend Normal", Range(0, 1)) = 0.1
            _MT_BlendNormal2        ("[MT] Blend Normal 2nd", Range(0, 1)) = 0.1
            _MT_Monochrome          ("[MT] Monochrome Reflection", Range(0, 1)) = 0
        [Toggle(_)]
            _MT_GeomSpecAA          ("[MT] Geometric Specular AA", Range(0, 1)) = 1
        [Enum(MASK,0,METALLIC,1)]
            _MT_MetallicMapType     ("[MT] MetallicMap Type", Float) = 0
        [NoScaleOffset]
            _MetallicGlossMap       ("[MT] MetallicSmoothnessMap Texture", 2D) = "white" {}
        [Toggle(_)]
            _MT_InvMaskVal          ("[MT] Invert Mask Value", Range(0, 1)) = 0
        [NoScaleOffset]
            _SpecGlossMap           ("[MT] RoughnessMap Texture", 2D) = "black" {}
        [Toggle(_)]
            _MT_InvRoughnessMaskVal ("[MT] Invert Mask Value", Range(0, 1)) = 0

        [Header(Metallic Specular)]
            _MT_Specular            ("[MT] Specular", Range(0, 1)) = 0
            _MT_SpecSmooth          ("[MT] Smoothness", Range(0, 1)) = 0.8

        [Header(Metallic Secondary)]
        [Enum(OFF,0,ONLY_SECOND_MAP,2)]
            _MT_CubemapType         ("[MT] 2nd CubeMap Blend", Float) = 0
        [NoScaleOffset]
            _MT_Cubemap             ("[MT] 2nd CubeMap", Cube) = "" {}
            _MT_CubemapPower        ("[MT] 2nd CubeMap Power", Range(0, 2)) = 1
            _MT_CubemapHighCut      ("[MT] 2nd CubeMap Hi-Cut Filter", Range(0, 1)) = 0

        // Matcapハイライト
        [WFHeaderToggle(Light Matcap)]
            _HL_Enable              ("[HL] Enable", Float) = 0
        [WF_Enum(UnlitWF.BlendModeHL)]
            _HL_CapType             ("[HL] Matcap Type", Float) = 0
        [NoScaleOffset]
            _HL_MatcapTex           ("[HL] Matcap Sampler", 2D) = "gray" {}
            _HL_MedianColor         ("[HL] Matcap Base Color", Color) = (0.5, 0.5, 0.5, 1)
            _HL_Power               ("[HL] Power", Range(0, 2)) = 1
            _HL_BlendNormal         ("[HL] Blend Normal", Range(0, 1)) = 0.1
            _HL_BlendNormal2        ("[HL] Blend Normal 2nd", Range(0, 1)) = 0.1
        [Toggle(_)]
            _HL_ChangeAlpha         ("[HL] Change Alpha Transparency", Range(0, 1)) = 0
        [NoScaleOffset]
            _HL_MaskTex             ("[HL] Mask Texture (RGB)", 2D) = "white" {}
        [Toggle(_)]
            _HL_InvMaskVal          ("[HL] Invert Mask Value", Range(0, 1)) = 0
        [Header(Matcap Advance)]
            _HL_Parallax            ("[HL] Parallax", Range(0, 1)) = 0.75
            _HL_MatcapMonochrome    ("[HL] Matcap Monochrome", Range(0, 1)) = 0
            _HL_MatcapColor         ("[HL] Matcap Tint Color", Color) = (0.5, 0.5, 0.5, 1)

        [WFHeaderToggle(Light Matcap 2)]
            _HL_Enable_1            ("[HA] Enable", Float) = 0
        [WF_Enum(UnlitWF.BlendModeHL)]
            _HL_CapType_1           ("[HA] Matcap Type", Float) = 0
        [NoScaleOffset]
            _HL_MatcapTex_1         ("[HA] Matcap Sampler", 2D) = "gray" {}
            _HL_MedianColor_1       ("[HA] Matcap Base Color", Color) = (0.5, 0.5, 0.5, 1)
            _HL_Power_1             ("[HA] Power", Range(0, 2)) = 1
            _HL_BlendNormal_1       ("[HA] Blend Normal", Range(0, 1)) = 0.1
            _HL_BlendNormal2_1      ("[HA] Blend Normal 2nd", Range(0, 1)) = 0.1
        [Toggle(_)]
            _HL_ChangeAlpha_1       ("[HA] Change Alpha Transparency", Range(0, 1)) = 0
        [NoScaleOffset]
            _HL_MaskTex_1           ("[HA] Mask Texture", 2D) = "white" {}
        [Toggle(_)]
            _HL_InvMaskVal_1        ("[HA] Invert Mask Value", Range(0, 1)) = 0
        [Header(Matcap Advance)]
            _HL_Parallax_1          ("[HA] Parallax", Range(0, 1)) = 0.75
            _HL_MatcapMonochrome_1  ("[HA] Matcap Monochrome", Range(0, 1)) = 0
            _HL_MatcapColor_1       ("[HA] Matcap Tint Color", Color) = (0.5, 0.5, 0.5, 1)

        // ラメ
        [WFHeaderToggle(Lame)]
            _LME_Enable             ("[LME] Enable", Float) = 0
        [Enum(UV1,0,UV2,1)]
            _LME_UVType             ("[LME] UV Type", Float) = 0
        [HDR]
            _LME_Color              ("[LME] Color", Color) = (1, 1, 1, 1)
            _LME_Texture            ("[LME] Texture", 2D) = "white" {}
        [HDR]
            _LME_RandColor          ("[LME] Random Color", Color) = (0, 0, 0, 1)
        [Toggle(_)]
            _LME_ChangeAlpha        ("[LME] Change Alpha Transparency", Range(0, 1)) = 0
        [Enum(POLYGON,0,POINT,1)]
            _LME_Shape              ("[LME] Shape", Float) = 0
        [PowerSlider(4.0)]
            _LME_Scale              ("[LME] Scale", Range(0, 4)) = 0.5
        [PowerSlider(4.0)]
            _LME_Dencity            ("[LME] Dencity", Range(0.3, 4)) = 0.5
            _LME_Glitter            ("[LME] Glitter", Range(0, 1)) = 0.5
            _LME_MinDist            ("[LME] FadeOut Distance (Near)", Range(0, 5)) = 2.0
            _LME_MaxDist            ("[LME] FadeOut Distance (Far)", Range(0, 5)) = 4.0
            _LME_Spot               ("[LME] FadeOut Angle", Range(0, 16)) = 2.0
            _LME_AnimSpeed          ("[LME] Anim Speed", Range(0, 1)) = 0.2
        [NoScaleOffset]
            _LME_MaskTex            ("[LME] Mask Texture (R)", 2D) = "white" {}
        [Toggle(_)]
            _LME_InvMaskVal         ("[LME] Invert Mask Value", Range(0, 1)) = 0

        // 階調影
        [WFHeaderToggle(ToonShade)]
            _TS_Enable              ("[TS] Enable", Float) = 0
        [IntRange]
            _TS_Steps               ("[TS] Steps", Range(1, 3)) = 2
            _TS_BaseColor           ("[TS] Base Color", Color) = (1, 1, 1, 1)
        [NoScaleOffset]
            _TS_BaseTex             ("[TS] Base Shade Texture", 2D) = "white" {}
            _TS_1stColor            ("[TS] 1st Shade Color", Color) = (0.81, 0.81, 0.9, 1)
        [NoScaleOffset]
            _TS_1stTex              ("[TS] 1st Shade Texture", 2D) = "white" {}
            _TS_2ndColor            ("[TS] 2nd Shade Color", Color) = (0.68, 0.68, 0.8, 1)
        [NoScaleOffset]
            _TS_2ndTex              ("[TS] 2nd Shade Texture", 2D) = "white" {}
            _TS_3rdColor            ("[TS] 3rd Shade Color", Color) = (0.595, 0.595, 0.7, 1)
        [NoScaleOffset]
            _TS_3rdTex              ("[TS] 3rd Shade Texture", 2D) = "white" {}
            _TS_Power               ("[TS] Shade Power", Range(0, 2)) = 1
            _TS_MinDist             ("[TS] FadeOut Distance (Near)", Range(0, 15)) = 1.0
            _TS_MaxDist             ("[TS] FadeOut Distance (Far)", Range(0, 15)) = 4.0
        [Toggle(_)]
            _TS_FixContrast         ("[TS] Dont Ajust Contrast", Range(0, 1)) = 0
            _TS_BlendNormal         ("[TS] Blend Normal", Range(0, 1)) = 0.1
            _TS_BlendNormal2        ("[TS] Blend Normal 2nd", Range(0, 1)) = 0.1
        [NoScaleOffset]
            _TS_MaskTex             ("[TS] Anti-Shadow Mask Texture (R)", 2D) = "black" {}
        [Toggle(_)]
            _TS_InvMaskVal          ("[TS] Invert Mask Value", Range(0, 1)) = 0
        [Header(ToonShade Advance)]
            _TS_1stBorder           ("[TS] 1st Border", Range(0, 1)) = 0.4
            _TS_2ndBorder           ("[TS] 2nd Border", Range(0, 1)) = 0.2
            _TS_3rdBorder           ("[TS] 3rd Border", Range(0, 1)) = 0.1
            _TS_1stFeather          ("[TS] 1st Feather", Range(0, 0.2)) = 0.05
            _TS_2ndFeather          ("[TS] 2nd Feather", Range(0, 0.2)) = 0.05
            _TS_3rdFeather          ("[TS] 3rd Feather", Range(0, 0.2)) = 0.05

        // リムライト
        [WFHeaderToggle(RimLight)]
            _TR_Enable              ("[TR] Enable", Float) = 0
        [HDR]
            _TR_Color               ("[TR] Rim Color", Color) = (0.8, 0.8, 0.8, 1)
        [WF_Enum(UnlitWF.BlendModeTR,ADD,ALPHA,ADD_AND_SUB)]
            _TR_BlendType           ("[TR] Blend Type", Float) = 0
            _TR_Power               ("[TR] Power", Range(0, 2)) = 1
            _TR_Feather             ("[TR] Feather", Range(0, 0.2)) = 0.05
            _TR_BlendNormal         ("[TR] Blend Normal", Range(0, 1)) = 0
            _TR_BlendNormal2        ("[TR] Blend Normal 2nd", Range(0, 1)) = 0
        [NoScaleOffset]
            _TR_MaskTex             ("[TR] Mask Texture (RGB)", 2D) = "white" {}
        [Toggle(_)]
            _TR_InvMaskVal          ("[TR] Invert Mask Value", Range(0, 1)) = 0
        [Header(RimLight Advance)]
            _TR_PowerTop            ("[TR] Power Top", Range(0, 0.5)) = 0.05
            _TR_PowerSide           ("[TR] Power Side", Range(0, 0.5)) = 0.1
            _TR_PowerBottom         ("[TR] Power Bottom", Range(0, 0.5)) = 0.1

        // Overlay Texture
        [WFHeaderToggle(Overlay Texture)]
            _OVL_Enable             ("[OVL] Enable", Float) = 0
        [Enum(UV1,0,UV2,1,SKYBOX,2,MATCAP,4,ANGEL_RING,3)]
            _OVL_UVType             ("[OVL] UV Type", Float) = 0
        [HDR]
            _OVL_Color              ("[OVL] Overlay Color", Color) = (1, 1, 1, 1)
            _OVL_OverlayTex         ("[OVL] Overlay Texture", 2D) = "white" {}
        [WF_Vector2]
            _OVL_UVScroll           ("[OVL] UV Scroll", Vector) = (0, 0, 0, 0)
        [Toggle(_)]
            _OVL_VertColToDecal     ("[OVL] Multiply VertexColor To Overlay Texture", Range(0, 1)) = 0
        [WF_Enum(UnlitWF.BlendModeOVL)]
            _OVL_BlendType          ("[OVL] Blend Type", Float) = 0
            _OVL_Power              ("[OVL] Blend Power", Range(0, 1)) = 1
            _OVL_CustomParam1       ("[OVL] Customize Parameter 1", Range(0, 1)) = 0
        [NoScaleOffset]
            _OVL_MaskTex            ("[OVL] Mask Texture (R)", 2D) = "white" {}
        [Toggle(_)]
            _OVL_VertColToMask      ("[OVL] Multiply VertexColor To Mask Texture", Range(0, 1)) = 0
        [Toggle(_)]
            _OVL_InvMaskVal         ("[OVL] Invert Mask Value", Range(0, 1)) = 0

        // Ambient Occlusion
        [WFHeaderToggle(Ambient Occlusion)]
            _AO_Enable              ("[AO] Enable", Float) = 0
        [Enum(UV1,0,UV2,1)]
            _AO_UVType              ("[AO] UV Type", Float) = 0
        [NoScaleOffset]
            _OcclusionMap           ("[AO] Occlusion Map (RGB)", 2D) = "white" {}
        [Toggle(_)]
            _AO_UseGreenMap         ("[AO] Use Green Channel Only", Float) = 0
            _AO_TintColor           ("[AO] Tint Color", Color) = (0, 0, 0, 1)
        [Toggle(_)]
            _AO_UseLightMap         ("[AO] Use LightMap", Float) = 1
            _AO_Contrast            ("[AO] Contrast", Range(0, 2)) = 1
            _AO_Brightness          ("[AO] Brightness", Range(-1, 1)) = 0

        // Emission
        [WFHeaderToggle(Emission)]
            _ES_Enable              ("[ES] Enable", Float) = 0
        [HDR]
            _EmissionColor          ("[ES] Emission", Color) = (1, 1, 1, 1)
        [NoScaleOffset]
            _EmissionMap            ("[ES] Emission Texture", 2D) = "white" {}
        [WF_Enum(UnlitWF.BlendModeES,ADD,ALPHA,LEGACY_ALPHA)]
            _ES_BlendType           ("[ES] Blend Type", Float) = 0

        [Header(Emissive Scroll)]
        [Toggle(_)]
            _ES_ScrollEnable        ("[ES] Enable EmissiveScroll", Float) = 0
        [Enum(STANDARD,0,SAWTOOTH,1,SIN_WAVE,2)]
            _ES_SC_Shape            ("[ES] Wave Type", Float) = 0
        [Toggle(_)]
            _ES_SC_AlphaScroll      ("[ES] Change Alpha Transparency", Range(0, 1)) = 0
        [Enum(WORLD_SPACE,0,LOCAL_SPACE,1,UV,2)]
            _ES_SC_DirType          ("[ES] Direction Type", Float) = 0
        [Enum(UV1,0,UV2,1)]
            _ES_SC_UVType           ("[ES] UV Type", Float) = 0
        [WF_Vector3]
            _ES_SC_Direction        ("[ES] Direction", Vector) = (0, -10, 0, 0)
            _ES_SC_LevelOffset      ("[ES] LevelOffset", Range(-1, 1)) = 0
            _ES_SC_Sharpness        ("[ES] Sharpness", Range(0, 4)) = 1
            _ES_SC_Speed            ("[ES] ScrollSpeed", Range(0, 8)) = 2

        // Lit
        [WFHeader(Lit)]
        [Gamma]
            _GL_LevelMin            ("Unlit Intensity", Range(0, 1)) = 0.125
        [Gamma]
            _GL_LevelMax            ("Saturate Intensity", Range(0, 1)) = 0.8
            _GL_BlendPower          ("Chroma Reaction", Range(0, 1)) = 0.8
        [Toggle(_)]
            _GL_CastShadow          ("Cast Shadows", Range(0, 1)) = 1

        [WFHeader(Lit Advance)]
        [Enum(AUTO,0,ONLY_DIRECTIONAL_LIT,1,ONLY_POINT_LIT,2,CUSTOM_WORLD_DIR,3,CUSTOM_LOCAL_DIR,4,CUSTOM_WORLD_POS,5)]
            _GL_LightMode           ("Sun Source", Float) = 0
            _GL_CustomAzimuth       ("Custom Sun Azimuth", Range(0, 360)) = 0
            _GL_CustomAltitude      ("Custom Sun Altitude", Range(-90, 90)) = 45
        [WF_Vector3]
            _GL_CustomLitPos        ("Custom Light Pos", Vector) = (0, 3, 0)
        [Toggle(_)]
            _GL_DisableBackLit      ("Disable BackLit", Range(0, 1)) = 0
        [Toggle(_)]
            _GL_DisableBasePos      ("Disable ObjectBasePos", Range(0, 1)) = 0

        [WFHeaderToggle(Light Bake Effects)]
            _LBE_Enable             ("[LBE] Enable", Float) = 0
            _LBE_IndirectMultiplier ("[LBE] Indirect Multiplier", Range(0, 2)) = 1
            _LBE_EmissionMultiplier ("[LBE] Emission Multiplier", Range(0, 2)) = 1
            _LBE_IndirectChroma     ("[LBE] Indirect Chroma", Range(0, 2)) = 1

        [HideInInspector]
        [WF_FixFloat(0.0)]
            _CurrentVersion         ("2023/01/07", Float) = 0
    }

    SubShader {
        Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass {
            Name "MAIN"
            Tags { "LightMode" = "UniversalForward" }

            Cull [_CullMode]
            ZWrite [_AL_ZWrite]
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha

            Stencil {
                Ref [_StencilMaskID]
                ReadMask 15
                Comp notEqual
            }

            HLSLPROGRAM

            #pragma exclude_renderers d3d11_9x gles

            #pragma vertex vert
            #pragma fragment frag

            #pragma target 3.0

            #define _WF_ALPHA_BLEND
            #define _WF_PLATFORM_LWRP

            #pragma shader_feature_local _ _ES_SCROLL_ENABLE
            #pragma shader_feature_local _ _GL_AUTO_ENABLE _GL_ONLYDIR_ENABLE _GL_ONLYPOINT_ENABLE _GL_WSDIR_ENABLE _GL_LSDIR_ENABLE _GL_WSPOS_ENABLE
            #pragma shader_feature_local _ _MT_ONLY2ND_ENABLE
            #pragma shader_feature_local _ _TS_FIXC_ENABLE
            #pragma shader_feature_local _ _TS_STEP1_ENABLE _TS_STEP2_ENABLE _TS_STEP3_ENABLE
            #pragma shader_feature_local _AO_ENABLE
            #pragma shader_feature_local _BKT_ENABLE
            #pragma shader_feature_local _CHM_ENABLE
            #pragma shader_feature_local _CLC_ENABLE
            #pragma shader_feature_local _ES_ENABLE
            #pragma shader_feature_local _HL_ENABLE
            #pragma shader_feature_local _HL_ENABLE_1
            #pragma shader_feature_local _LME_ENABLE
            #pragma shader_feature_local _MT_ENABLE
            #pragma shader_feature_local _NM_ENABLE
            #pragma shader_feature_local _NS_ENABLE
            #pragma shader_feature_local _OVL_ENABLE
            #pragma shader_feature_local _TR_ENABLE
            #pragma shader_feature_local _TS_ENABLE
            #pragma shader_feature_local _VC_ENABLE

            #pragma multi_compile _ _WF_EDITOR_HIDE_LMAP

            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            //--------------------------------------
            #pragma multi_compile_instancing

            #include "../WF_INPUT_UnToon.cginc"
            #include "../WF_UnToon.cginc"

            ENDHLSL
        }

        Pass {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            Cull[_CullMode]
            ZWrite [_AL_ZWrite]
            ColorMask 0

            Stencil {
                Ref [_StencilMaskID]
                ReadMask 15
                Comp notEqual
            }

            HLSLPROGRAM

            #pragma exclude_renderers d3d11_9x gles

            #pragma vertex vert_depth
            #pragma fragment frag_depth

            #define _WF_ALPHA_BLEND
            #define _WF_PLATFORM_LWRP

            #pragma shader_feature_local _VC_ENABLE

            #pragma multi_compile_instancing

            #include "../WF_INPUT_UnToon.cginc"
            #include "../WF_UnToon_DepthOnly.cginc"

            ENDHLSL
        }

        UsePass "UnlitWF_URP/WF_UnToon_Transparent/SHADOWCASTER"
        UsePass "UnlitWF_URP/WF_UnToon_Transparent/META"
    }

    FallBack "Hidden/InternalErrorShader"

    CustomEditor "UnlitWF.ShaderCustomEditor"
}
