﻿/*
 *  The MIT License
 *
 *  Copyright 2018-2022 whiteflare.
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

#if UNITY_EDITOR

#if VRC_SDK_VRCSDK3
#define ENV_VRCSDK3
#if UDON
#define ENV_VRCSDK3_WORLD
#else
#define ENV_VRCSDK3_AVATAR
#endif
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnlitWF
{
    internal static class WFMenu
    {
        public const string PATH_ASSETS = "Assets/UnlitWF Material Tools/";
        public const string PATH_TOOLS = "Tools/UnlitWF/";
        public const string PATH_MATERIAL = "CONTEXT/Material/";
        public const string PATH_GAMEOBJECT = "GameObject/";

#if WF_ML_JP
        public const string ASSETS_AUTOCNV = PATH_ASSETS + "UnlitWF のマテリアルに変換する";

        public const string ASSETS_DEBUGVIEW = PATH_ASSETS + "シェーダ切替/DebugView シェーダに切り替える";
        public const string ASSETS_CNGMOBILE = PATH_ASSETS + "シェーダ切替/モバイル向けシェーダに変換する";

        public const string ASSETS_CREANUP = PATH_ASSETS + "マテリアルのクリンナップ";
        public const string ASSETS_COPY = PATH_ASSETS + "マテリアル設定値のコピー";
        public const string ASSETS_RESET = PATH_ASSETS + "マテリアル設定値のリセット";
        public const string ASSETS_MIGRATION = PATH_ASSETS + "マテリアルを最新に変換する";
        public const string ASSETS_DLSET = PATH_ASSETS + "シーンDL方向をマテリアルに焼き込む";

        public const string TOOLS_CREANUP = PATH_TOOLS + "マテリアルのクリンナップ";
        public const string TOOLS_COPY = PATH_TOOLS + "マテリアル設定値のコピー";
        public const string TOOLS_RESET = PATH_TOOLS + "マテリアル設定値のリセット";
        public const string TOOLS_MIGRATION = PATH_TOOLS + "マテリアルを最新に変換する";
        public const string TOOLS_MIGALL = PATH_TOOLS + "全てのマテリアルを最新に変換する";
        public const string TOOLS_DLSET = PATH_TOOLS + "シーンDL方向をマテリアルに焼き込む";

        public const string MATERIAL_AUTOCNV = PATH_MATERIAL + "UnlitWF のマテリアルに変換する";
        public const string MATERIAL_DEBUGVIEW = PATH_MATERIAL + "DebugView シェーダに切り替える";
        public const string MATERIAL_CNGMOBILE = PATH_MATERIAL + "モバイル向けシェーダに変換する";

        public const string GAMEOBJECT_CREANUP = PATH_GAMEOBJECT + "UnlitWFマテリアルのクリンナップ";
#else
        public const string ASSETS_AUTOCNV = PATH_ASSETS + "Convert UnlitWF Material";

        public const string ASSETS_DEBUGVIEW = PATH_ASSETS + "SwitchShader/Switch DebugView Shader";
        public const string ASSETS_CNGMOBILE = PATH_ASSETS + "SwitchShader/Change Mobile Shader";

        public const string ASSETS_CREANUP = PATH_ASSETS + "CleanUp Material Property";
        public const string ASSETS_COPY = PATH_ASSETS + "Copy Material Property";
        public const string ASSETS_RESET = PATH_ASSETS + "Reset Material Property";
        public const string ASSETS_MIGRATION = PATH_ASSETS + "Migration Material";
        public const string ASSETS_DLSET = PATH_ASSETS + "Bake DL into Material";

        public const string TOOLS_CREANUP = PATH_TOOLS + "CleanUp Material Property";
        public const string TOOLS_COPY = PATH_TOOLS + "Copy Material Property";
        public const string TOOLS_RESET = PATH_TOOLS + "Reset Material Property";
        public const string TOOLS_MIGRATION = PATH_TOOLS + "Migration Material";
        public const string TOOLS_MIGALL = PATH_TOOLS + "Migration All Materials";
        public const string TOOLS_DLSET = PATH_TOOLS + "Bake DL into Material";

        public const string MATERIAL_AUTOCNV = PATH_MATERIAL + "Convert UnlitWF Material";
        public const string MATERIAL_DEBUGVIEW = PATH_MATERIAL + "Switch WF_DebugView Shader";
        public const string MATERIAL_CNGMOBILE = PATH_MATERIAL + "Change Mobile shader";

        public const string GAMEOBJECT_CREANUP = PATH_GAMEOBJECT + "CleanUp UnlitWF Material Property";
#endif

        public const string TOOLS_LNG_EN = PATH_TOOLS + "Menu Language Change To English";
        public const string TOOLS_LNG_JP = PATH_TOOLS + "メニューの言語を日本語にする";

        public const int PRI_ASSETS_AUTOCNV = 2101;
        public const int PRI_ASSETS_DEBUGVIEW = 2202;
        public const int PRI_ASSETS_CNGMOBILE = 2203;
        public const int PRI_ASSETS_CREANUP = 2304;
        public const int PRI_ASSETS_COPY = 2305;
        public const int PRI_ASSETS_RESET = 2306;
        public const int PRI_ASSETS_MIGRATION = 2307;
        public const int PRI_ASSETS_DLSET = 2308;

        public const int PRI_TOOLS_CREANUP = 101;
        public const int PRI_TOOLS_COPY = 102;
        public const int PRI_TOOLS_RESET = 103;
        public const int PRI_TOOLS_MIGRATION = 104;
        public const int PRI_TOOLS_DLSET = 105;
        public const int PRI_TOOLS_MIGALL = 301;
        public const int PRI_TOOLS_CNGLANG = 501;

        public const int PRI_MATERIAL_AUTOCNV = 1654;
        public const int PRI_MATERIAL_DEBUGVIEW = 1655;
        public const int PRI_MATERIAL_CNGMOBILE = 1656;

        #region Convert UnlitWF material

        [MenuItem(WFMenu.ASSETS_AUTOCNV, priority = WFMenu.PRI_ASSETS_AUTOCNV)]
        private static void Menu_AutoConvertMaterial()
        {
            var mats = new MaterialSeeker().GetSelectionAllMaterial(MatSelectMode.FromAssetDeep);
            new Converter.WFMaterialFromOtherShaderConverter().ExecAutoConvert(mats.ToArray());
        }

        [MenuItem(WFMenu.MATERIAL_AUTOCNV, priority = WFMenu.PRI_MATERIAL_AUTOCNV)]
        private static void ContextMenu_AutoConvertMaterial(MenuCommand cmd)
        {
            new Converter.WFMaterialFromOtherShaderConverter().ExecAutoConvert(cmd.context as Material);
        }

        #endregion

        #region Migration

        [MenuItem(WFMenu.TOOLS_MIGALL, priority = WFMenu.PRI_TOOLS_MIGALL)]
        private static void Menu_ScanAndAllMigration()
        {
            Converter.ScanAndMigrationExecutor.ExecuteByManual();
        }

        #endregion

        #region DebugView

        [MenuItem(WFMenu.MATERIAL_DEBUGVIEW, priority = WFMenu.PRI_MATERIAL_DEBUGVIEW)]
        private static void ContextMenu_DebugView(MenuCommand cmd)
        {
            WFCommonUtility.ChangeShader(WF_DebugViewEditor.SHADER_NAME_DEBUGVIEW, cmd.context as Material);
        }

        [MenuItem(WFMenu.ASSETS_DEBUGVIEW, priority = WFMenu.PRI_ASSETS_DEBUGVIEW)]
        private static void Menu_DebugView()
        {
            foreach (var mat in new MaterialSeeker().GetSelectionAllMaterial(MatSelectMode.FromAsset))
            {
                WFCommonUtility.ChangeShader(WF_DebugViewEditor.SHADER_NAME_DEBUGVIEW, mat);
            }
        }

        #endregion

        #region Change Mobile Shader

        [MenuItem(WFMenu.MATERIAL_CNGMOBILE, priority = WFMenu.PRI_MATERIAL_CNGMOBILE)]
        private static void ContextMenu_ChangeMobileShader(MenuCommand cmd)
        {
            ChangeMobileShader(cmd.context as Material);
        }

        [MenuItem(WFMenu.ASSETS_CNGMOBILE, priority = WFMenu.PRI_ASSETS_CNGMOBILE)]
        private static void Menu_ChangeMobileShader()
        {
            var mats = new MaterialSeeker().GetSelectionAllMaterial(MatSelectMode.FromAssetDeep);
            ChangeMobileShader(mats.ToArray());
        }

        private static void ChangeMobileShader(params Material[] mats)
        {
            if (0 < mats.Length && EditorUtility.DisplayDialog("WF change Mobile shader", WFI18N.Translate(WFMessageText.DgChangeMobile), "OK", "Cancel"))
            {
                new Converter.WFMaterialToMobileShaderConverter().ExecAutoConvert(mats);
            }
        }

        #endregion

        #region Change Lang

#if WF_ML_JP
        [MenuItem(TOOLS_LNG_EN, priority = WFMenu.PRI_TOOLS_CNGLANG)]
        private static void Menu_ChangeLang()
        {
            if (!EditorUtility.DisplayDialog("WF", "Do you want to switch the menu about UnlitWF to English?\nIt may take a few minutes to switch.", "OK", "Cancel"))
            {
                return;
            }
            BuildTargetGroup currentTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbols = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(currentTarget).Split(';'));
            symbols.Remove("WF_ML_JP");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentTarget, string.Join(";", symbols));
        }
#else
        [MenuItem(TOOLS_LNG_JP, priority = WFMenu.PRI_TOOLS_CNGLANG)]
        private static void Menu_ChangeLang()
        {
            if (!EditorUtility.DisplayDialog("WF", "UnlitWF に関するメニューを日本語にしますか？\n切り替えには数分の時間がかかる場合があります。", "OK", "Cancel"))
            {
                return;
            }
            BuildTargetGroup currentTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbols = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(currentTarget).Split(';'));
            symbols.Remove("WF_ML_JP");
            symbols.Add("WF_ML_JP");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentTarget, string.Join(";", symbols));
        }
#endif

        #endregion

        [MenuItem(WFMenu.ASSETS_DEBUGVIEW, validate = true)]
        private static bool MenuValidation_HasMaterials()
        {
            return Selection.GetFiltered<Material>(SelectionMode.Assets).Length != 0;
        }
    }

    internal static class ToolCommon
    {
        public static bool IsUnlitWFMaterial(Material mm)
        {
            if (mm != null && mm.shader != null)
            {
                return mm.shader.name.Contains("UnlitWF") && !mm.shader.name.Contains("Debug");
            }
            return false;
        }

        public static bool IsNotUnlitWFMaterial(Material mm)
        {
            if (mm != null && mm.shader != null)
            {
                return !IsUnlitWFMaterial(mm);
            }
            return false;
        }

        public static Material[] FilterOnlyWFMaterial(Material[] array)
        {
            return array.Where(mat => IsUnlitWFMaterial(mat)).ToArray();
        }

        public static Material[] FilterOnlyNotWFMaterial(Material[] array)
        {
            return array.Where(mat => IsNotUnlitWFMaterial(mat)).ToArray();
        }

        public static bool NoticeIfIllegalMaterials(Material[] array, bool showRemoveButton = true)
        {
            foreach (var mm in array)
            {
                if (ToolCommon.IsNotUnlitWFMaterial(mm))
                {
                    EditorGUILayout.HelpBox("Found Not-UnlitWF materials. Continue?\n(UnlitWF以外のマテリアルが紛れていますが大丈夫ですか？)", MessageType.Warning);
                    if (showRemoveButton && GUILayout.Button("Remove other materials"))
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        public static void WindowHeader(string title, string subtitle, string helptext)
        {
            // タイトル
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(title, new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                fixedHeight = 32,
            });
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // メイン
            EditorGUILayout.LabelField(subtitle, new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                fixedHeight = 32,
            });
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(helptext, MessageType.Info);
            EditorGUILayout.Space();
        }

        public static bool ExecuteButton(string label, bool disable = false)
        {
            using (new EditorGUI.DisabledGroupScope(disable))
            {
                var oldColor = GUI.color;
                GUI.color = new Color(0.75f, 0.75f, 1f);
                bool exec = GUILayout.Button(label);
                GUI.color = oldColor;
                return exec;
            }
        }

        private static readonly List<Material> arguments = new List<Material>();

        public static void SetSelectedMaterials(MatSelectMode mode)
        {
            arguments.Clear();
            arguments.AddRange(new MaterialSeeker().GetSelectionAllMaterial(mode));
        }

        public static void SetMaterials(Material[] mats)
        {
            arguments.Clear();
            arguments.AddRange(mats);
        }

        public static void GetSelectedMaterials(ref Material[] array)
        {
            if (0 < arguments.Count)
            {
                array = arguments.Distinct().Where(mat => mat != null).OrderBy(mat => mat.name).ToArray();
                arguments.Clear();
            }
        }

        public static void MaterialProperty(UnityEngine.Object obj, string name)
        {
            var so = new SerializedObject(obj);
            so.Update();

            SerializedProperty property = so.FindProperty(name);
            if (property == null)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();
            if (property.isArray)
            {
                // 複数行マテリアルフィールド
                EditorGUILayout.PropertyField(property, new GUIContent("list (" + property.arraySize + ")"), true);
            }
            else
            {
                // 1行マテリアルフィールド
                EditorGUILayout.PropertyField(property, new GUIContent("material"), true);
            }
            if (EditorGUI.EndChangeCheck())
            {
                so.ApplyModifiedPropertiesWithoutUndo();
                so.SetIsDifferentCacheDirty();
            }
        }

        public static void SetArrayPropertyExpanded(UnityEngine.Object obj, string name)
        {
            var so = new SerializedObject(obj);
            so.Update();

            SerializedProperty property = so.FindProperty(name);
            if (property == null || !property.isArray)
            {
                return;
            }
            property.isExpanded = property.arraySize <= 10;
        }
    }

    #region クリンナップ系

    public class ToolCreanUpWindow : EditorWindow
    {
        [MenuItem(WFMenu.ASSETS_CREANUP, priority = WFMenu.PRI_ASSETS_CREANUP)]
        private static void OpenWindowFromMenu_Asset()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromAssetDeep);
            GetWindow<ToolCreanUpWindow>("UnlitWF/CleanUp material property");
        }

        [MenuItem(WFMenu.GAMEOBJECT_CREANUP, priority = 10)] // GameObject/配下は priority の扱いがちょっと特殊
        private static void OpenWindowFromMenu_GameObject()
        {
            if (Selection.GetFiltered<GameObject>(SelectionMode.Unfiltered).Length == 0)
            {
                ToolCommon.SetMaterials(new MaterialSeeker().GetAllSceneAllMaterial().ToArray());
            }
            else
            {
                ToolCommon.SetSelectedMaterials(MatSelectMode.FromScene);
            }
            GetWindow<ToolCreanUpWindow>("UnlitWF/CleanUp material property");
        }

        [MenuItem(WFMenu.TOOLS_CREANUP, priority = WFMenu.PRI_TOOLS_CREANUP)]
        private static void OpenWindowFromMenu_Tool()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromSceneOrAsset);
            GetWindow<ToolCreanUpWindow>("UnlitWF/CleanUp material property");
        }

        internal static void OpenWindowFromShaderGUI(Material[] mats)
        {
            ToolCommon.SetMaterials(mats);
            GetWindow<ToolCreanUpWindow>("UnlitWF/CleanUp material property");
        }

        Vector2 scroll = Vector2.zero;
        private CleanUpParameter param;

        private void OnEnable()
        {
            minSize = new Vector2(480, 640);
            param = CleanUpParameter.Create();
            ToolCommon.GetSelectedMaterials(ref param.materials);
            ToolCommon.SetArrayPropertyExpanded(param, nameof(param.materials));
        }

        private void OnGUI()
        {
            ToolCommon.WindowHeader("UnlitWF / CleanUp material property", "CleanUp disabled values", "materialsから無効化されている機能の設定値をクリアします。");

            // スクロール開始
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // マテリアルリスト
            EditorGUILayout.LabelField("materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(param, nameof(param.materials));
            EditorGUILayout.Space();

            // マテリアルに UnlitWF 以外のシェーダが紛れている場合には警告
            bool removeOther = ToolCommon.NoticeIfIllegalMaterials(param.materials);
            EditorGUILayout.Space();

            // UnlitWF 以外のマテリアルを除去
            if (removeOther)
            {
                param.materials = ToolCommon.FilterOnlyWFMaterial(param.materials);
            }

            // マテリアルにUnlitWF以外のシェーダが紛れている場合は追加の情報を表示
            if (param.materials.Any(ToolCommon.IsNotUnlitWFMaterial))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("UnlitWF以外のマテリアルは、未使用の値のみ除去します。", MessageType.Info);
                EditorGUILayout.Space();
            }

            if (ToolCommon.ExecuteButton("CleanUp", param.materials.Length == 0))
            {
                WFMaterialEditUtility.CleanUpProperties(param);
            }
            EditorGUILayout.Space();

            // スクロール終了
            EditorGUILayout.EndScrollView();
        }
    }

    #endregion

    #region リセット系

    public class ToolResetWindow : EditorWindow
    {
        [MenuItem(WFMenu.ASSETS_RESET, priority = WFMenu.PRI_ASSETS_RESET)]
        private static void OpenWindowFromMenu_Asset()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromAssetDeep);
            GetWindow<ToolResetWindow>("UnlitWF/Reset material property");
        }

        [MenuItem(WFMenu.TOOLS_RESET, priority = WFMenu.PRI_TOOLS_RESET)]
        private static void OpenWindowFromMenu_Tool()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromSceneOrAsset);
            GetWindow<ToolResetWindow>("UnlitWF/Reset material property");
        }

        Vector2 scroll = Vector2.zero;
        private ResetParameter param;

        private void OnEnable()
        {
            minSize = new Vector2(480, 640);
            param = ResetParameter.Create();
            ToolCommon.GetSelectedMaterials(ref param.materials);
            ToolCommon.SetArrayPropertyExpanded(param, nameof(param.materials));
        }

        private void OnGUI()
        {
            ToolCommon.WindowHeader("UnlitWF / Reset material property", "Reset properties", "materialsの設定値を初期化します。");

            // スクロール開始
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // マテリアルリスト
            EditorGUILayout.LabelField("materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(param, nameof(param.materials));
            EditorGUILayout.Space();

            // マテリアルに UnlitWF 以外のシェーダが紛れている場合には警告
            bool removeOther = ToolCommon.NoticeIfIllegalMaterials(param.materials);

            EditorGUILayout.Space();

            // 対象(種類から)
            EditorGUILayout.LabelField("Reset by Type", EditorStyles.boldLabel);
            param.resetColor = GUILayout.Toggle(param.resetColor, "Color (色) をデフォルトに戻す");
            param.resetTexture = GUILayout.Toggle(param.resetTexture, "Texture (テクスチャ) をデフォルトに戻す");
            param.resetFloat = GUILayout.Toggle(param.resetFloat, "Float (数値) をデフォルトに戻す");

            EditorGUILayout.Space();

            // 対象(機能から)
            EditorGUILayout.LabelField("Reset by Function", EditorStyles.boldLabel);
            param.resetColorAlpha = GUILayout.Toggle(param.resetColorAlpha, "Color (色) の Alpha を 1.0 にする");
            param.resetLit = GUILayout.Toggle(param.resetLit, "Lit & Lit Advance の設定をデフォルトに戻す");

            EditorGUILayout.Space();

            // オプション
            EditorGUILayout.LabelField("options", EditorStyles.boldLabel);
            param.resetUnused = GUILayout.Toggle(param.resetUnused, "UnUsed Properties (未使用の値) も一緒にクリアする");
            param.resetKeywords = GUILayout.Toggle(param.resetKeywords, "ShaderKeywords (Shaderキーワード) も一緒にクリアする");

            EditorGUILayout.Space();

            // UnlitWF 以外のマテリアルを除去
            if (removeOther)
            {
                param.materials = ToolCommon.FilterOnlyWFMaterial(param.materials);
            }

            if (ToolCommon.ExecuteButton("Reset Values", param.materials.Length == 0))
            {
                WFMaterialEditUtility.ResetProperties(param);
            }
            EditorGUILayout.Space();

            // スクロール終了
            EditorGUILayout.EndScrollView();
        }
    }

    #endregion

    #region コピー系

    public class ToolCopyWindow : EditorWindow
    {

        [MenuItem(WFMenu.ASSETS_COPY, priority = WFMenu.PRI_ASSETS_COPY)]
        private static void OpenWindowFromMenu_Asset()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromAssetDeep);
            GetWindow<ToolCopyWindow>("UnlitWF/Copy material property");
        }

        [MenuItem(WFMenu.TOOLS_COPY, priority = WFMenu.PRI_TOOLS_COPY)]
        private static void OpenWindowFromMenu_Tool()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromSceneOrAsset);
            GetWindow<ToolCopyWindow>("UnlitWF/Copy material property");
        }

        Vector2 scroll = Vector2.zero;
        private CopyPropParameter param;

        private void OnEnable()
        {
            minSize = new Vector2(480, 640);
            param = CopyPropParameter.Create();
            ToolCommon.GetSelectedMaterials(ref param.materialDestination);
            ToolCommon.SetArrayPropertyExpanded(param, nameof(param.materialDestination));
        }

        private void OnGUI()
        {
            ToolCommon.WindowHeader("UnlitWF / Copy material property", "Copy properties", "source material の設定値を destination materials にコピーします。");

            // スクロール開始
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // マテリアルリスト
            EditorGUILayout.LabelField("destination materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(param, nameof(param.materialDestination));
            EditorGUILayout.Space();

            // マテリアルに UnlitWF 以外のシェーダが紛れている場合には警告
            bool removeOther = ToolCommon.NoticeIfIllegalMaterials(param.materialDestination);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("source materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(param, nameof(param.materialSource));
            EditorGUILayout.Space();

            ToolCommon.NoticeIfIllegalMaterials(new Material[] { param.materialSource }, false);
            EditorGUILayout.Space();

            // 対象
            EditorGUILayout.LabelField("copy target functions", EditorStyles.boldLabel);

            var updatedFunctions = new List<string>();
            foreach (var func in WFShaderFunction.GetEnableFunctionList(param.materialSource))
            {
                bool value = param.labels.Contains(func.Label);
                if (GUILayout.Toggle(value, string.Format("[{0}] {1}", func.Label, func.Name)))
                {
                    updatedFunctions.Add(func.Label);
                }
            }
            if (!updatedFunctions.SequenceEqual(param.labels))
            {
                param.labels = updatedFunctions.ToArray();
            }

            EditorGUILayout.Space();

            // UnlitWF 以外のマテリアルを除去
            if (removeOther)
            {
                param.materialDestination = ToolCommon.FilterOnlyWFMaterial(param.materialDestination);
            }

            using (new EditorGUI.DisabledGroupScope(param.labels.Length == 0))
            {
                if (ToolCommon.ExecuteButton("Copy Values", param.materialSource == null || param.materialDestination.Length == 0))
                {
                    WFMaterialEditUtility.CopyProperties(param);
                }
            }
            EditorGUILayout.Space();

            // スクロール終了
            EditorGUILayout.EndScrollView();
        }
    }
    #endregion

    #region マイグレーション系

    public class ToolMigrationWindow : EditorWindow
    {
        [MenuItem(WFMenu.ASSETS_MIGRATION, priority = WFMenu.PRI_ASSETS_MIGRATION)]
        private static void OpenWindowFromMenu_Asset()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromAssetDeep);
            GetWindow<ToolMigrationWindow>("UnlitWF/Migration material");
        }

        [MenuItem(WFMenu.TOOLS_MIGRATION, priority = WFMenu.PRI_TOOLS_MIGRATION)]
        private static void OpenWindowFromMenu_Tool()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromSceneOrAsset);
            GetWindow<ToolMigrationWindow>("UnlitWF/Migration material");
        }

        Vector2 scroll = Vector2.zero;
        private MigrationParameter param;

        private void OnEnable()
        {
            minSize = new Vector2(480, 640);
            param = MigrationParameter.Create();
            ToolCommon.GetSelectedMaterials(ref param.materials);
            ToolCommon.SetArrayPropertyExpanded(param, nameof(param.materials));
        }

        private void OnGUI()
        {
            ToolCommon.WindowHeader("UnlitWF / Migration material", "Migration materials", "古いバージョンのUnlitWFで設定されたmaterialsを最新版に変換します。");

            // スクロール開始
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // マテリアルリスト
            EditorGUILayout.LabelField("materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(param, nameof(param.materials));
            EditorGUILayout.Space();

            // マテリアルに UnlitWF 以外のシェーダが紛れている場合には警告
            bool removeOther = ToolCommon.NoticeIfIllegalMaterials(param.materials);
            EditorGUILayout.Space();

            // UnlitWF 以外のマテリアルを除去
            if (removeOther)
            {
                param.materials = ToolCommon.FilterOnlyWFMaterial(param.materials);
            }

            if (ToolCommon.ExecuteButton("Convert", param.materials.Length == 0))
            {
                // 変換
                WFMaterialEditUtility.MigrationMaterial(param);
                // ShaderGUI側のマテリアルキャッシュをリセット
                ShaderCustomEditor.ResetOldMaterialTable();
                // 変更したマテリアルを保存
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.Space();

            // スクロール終了
            EditorGUILayout.EndScrollView();
        }
    }

    #endregion

    #region DL焼き込み系

    public class ToolDirectionalSetWindow : EditorWindow
    {

#if ENV_VRCSDK3_WORLD
        [MenuItem(WFMenu.ASSETS_DLSET, priority = WFMenu.PRI_ASSETS_DLSET)]
        private static void OpenWindowFromMenu_Asset()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromAssetDeep);
            GetWindow<ToolDirectionalSetWindow>("UnlitWF/DirectionalLight Setting");
        }

        [MenuItem(WFMenu.TOOLS_DLSET, priority = WFMenu.PRI_TOOLS_DLSET)]
        private static void OpenWindowFromMenu_Tool()
        {
            ToolCommon.SetSelectedMaterials(MatSelectMode.FromSceneOrAsset);
            GetWindow<ToolDirectionalSetWindow>("UnlitWF/DirectionalLight Setting");
        }
#endif

        Vector2 scroll = Vector2.zero;
        private Light directionalLight;
        [SerializeField]
        private Material[] materials = { };

        private void OnEnable()
        {
            minSize = new Vector2(480, 640);
            directionalLight = RenderSettings.sun;
            ToolCommon.GetSelectedMaterials(ref materials);
            ToolCommon.SetArrayPropertyExpanded(this, nameof(this.materials));
        }

        private void OnGUI()
        {
            ToolCommon.WindowHeader("UnlitWF / DirectionalLight Setting", "DirectionalLight Setting", "マテリアルにシーン DirectionalLight を焼き込みます。");

            // スクロール開始
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // DirectionalLight
            directionalLight = (Light)EditorGUILayout.ObjectField("Directional Light", directionalLight, typeof(Light), true);
            if (directionalLight != null && directionalLight.type != LightType.Directional)
            {
                EditorGUILayout.HelpBox(string.Format("{0} is NOT DirectionalLight.", directionalLight), MessageType.Warning);
            }
            EditorGUILayout.Space();

            // マテリアルリスト
            EditorGUILayout.LabelField("materials", EditorStyles.boldLabel);
            ToolCommon.MaterialProperty(this, nameof(this.materials));
            EditorGUILayout.Space();

            // マテリアルに UnlitWF 以外のシェーダが紛れている場合には警告
            bool removeOther = ToolCommon.NoticeIfIllegalMaterials(materials);
            EditorGUILayout.Space();

            // UnlitWF 以外のマテリアルを除去
            if (removeOther)
            {
                materials = ToolCommon.FilterOnlyWFMaterial(materials);
            }

            if (ToolCommon.ExecuteButton("Bake DirectionalLight", directionalLight == null || directionalLight.type != LightType.Directional || materials.Length == 0))
            {
                // 実行
                ExecuteDLBake();
                // 変更したマテリアルを保存
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.Space();

            // スクロール終了
            EditorGUILayout.EndScrollView();
        }

        private void ExecuteDLBake()
        {
            var lightWorldDir = directionalLight.transform.TransformVector(new Vector3(0, 0, 1));
            var azm = Mathf.RoundToInt(Mathf.Rad2Deg * Mathf.Atan2(lightWorldDir.x, lightWorldDir.z));
            if (azm < 0)
            {
                azm += 360;
            }
            var alt = Mathf.Rad2Deg * -Mathf.Atan2(lightWorldDir.y, new Vector2(lightWorldDir.x, lightWorldDir.z).magnitude);

            var targets = WFCommonUtility.AsMaterials(materials);
            Undo.RecordObjects(targets, "Bake DirectionalLight");
            foreach (var mat in targets)
            {
                if (!WFCommonUtility.IsSupportedShader(mat))
                {
                    continue;
                }
                mat.SetInt("_GL_LightMode", 3); // CUSTOM_WORLD_DIR
                mat.SetFloat("_GL_CustomAzimuth", azm);
                mat.SetFloat("_GL_CustomAltitude", alt);
                EditorUtility.SetDirty(mat);
            }
        }
    }

    #endregion
}

#endif
