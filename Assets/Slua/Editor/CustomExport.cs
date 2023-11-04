// The MIT License (MIT)

// Copyright 2015 Siney/Pangweiwei siney@yeah.net
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace SLua
{
    using System.Collections.Generic;
    using System;

    public class CustomExport
    {
        public static void OnGetAssemblyToGenerateExtensionMethod(out List<string> list)
        {
            list = new List<string> {
                "Assembly-CSharp",
            };
        }

        public static void OnAddCustomClass(LuaCodeGen.ExportGenericDelegate add)
        {
            // below lines only used for demostrate how to add custom class to export, can be delete on your app

            add(typeof(System.Func<int>), null);
            add(typeof(System.Action<int, string>), null);
            add(typeof(System.Action<int, Dictionary<int, object>>), null);
            add(typeof(List<int>), "ListInt");
            // .net 4.6 export class not match used class on runtime, so skip it
            //add(typeof(Dictionary<int, string>), "DictIntStr");
            add(typeof(string), "String");

            // add your custom class here
            // add( type, typename)
            // type is what you want to export
            // typename used for simplify generic type name or rename, like List<int> named to "ListInt", if not a generic type keep typename as null or rename as new type name
        }

        public static void OnAddCustomAssembly(ref List<string> list)
        {
            // add your custom assembly here
            // you can build a dll for 3rd library like ngui titled assembly name "NGUI", put it in Assets folder
            // add its name into list, slua will generate all exported interface automatically for you

            //list.Add("NGUI");
        }

        public static HashSet<string> OnAddCustomNamespace()
        {
            return new HashSet<string>
            {
                //"NLuaTest.Mock"
            };
        }

        // if uselist return a white list, don't check noUseList(black list) again
        public static void OnGetUseList(out List<string> list)
        {
            list = new List<string>
            {
                // "UnityEngine.Font",
            };
        }

        public static List<string> FunctionFilterList = new List<string>()
        {
            "UIWidget.showHandles",
            "UIWidget.showHandlesWithMoveTool",
            "UnityEngine.QualitySettings.get_streamingMipmapsRenderersPerFrame",
            "UnityEngine.QualitySettings.set_streamingMipmapsRenderersPerFrame",
            "UnityEngine.QualitySettings.streamingMipmapsRenderersPerFrame",
            "UnityEngine.Texture.get_imageContentsHash",
            "UnityEngine.Texture.set_imageContentsHash",
            "UnityEngine.Texture.imageContentsHash",

"UnityEngine.Device.Screen.MoveMainWindowTo",

"UnityEngine.ComputeShader.EnableKeyword",
"UnityEngine.ComputeShader.DisableKeyword",
"UnityEngine.ComputeShader.SetKeyword",
"UnityEngine.ComputeShader.IsKeywordEnabled",

"UnityEngine.Graphics.RenderMesh",
"UnityEngine.Graphics.RenderMeshIndirect",
"UnityEngine.Graphics.RenderMeshPrimitives",
"UnityEngine.Graphics.RenderPrimitives",

"UnityEngine.LineRenderer.GetPositions",

"UnityEngine.Graphics.RenderPrimitivesIndexed",
"UnityEngine.Graphics.RenderPrimitivesIndirect",
"UnityEngine.Graphics.RenderPrimitivesIndexedIndirect",
"UnityEngine.Material.EnableKeyword",
"UnityEngine.Material.DisableKeyword",
"UnityEngine.Material.IsKeywordEnabled",
"UnityEngine.Material.SetKeyword",

"UnityEngine.Screen.MoveMainWindowTo",

"UnityEngine.Shader.IsKeywordEnabled",
"UnityEngine.Shader.SetKeyword",
"UnityEngine.Shader.DisableKeyword",
"UnityEngine.Shader.EnableKeyword",

"UnityEngine.Screen.sleepTimeout",

"UnityEngine.TrailRenderer.GetPositions",
"UnityEngine.TrailRenderer.GetVisiblePositions",
"UnityEngine.TrailRenderer.AddPositions",
"UnityEngine.TrailRenderer.GetVisiblePositions",
"UnityEngine.TrailRenderer.GetVisiblePositions",

"Unity.Profiling.LowLevel.Unsafe.ProfilerCategoryDescription.NameUtf8",
"Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData.Ptr",
"Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData.set_Ptr",
"Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility.CreateCounterValue",
"Unity.Profiling.LowLevel.Unsafe.ProfilerRecorderDescription.NameUtf8",        

"UnityEngine.AudioSource.PlayOnGamepad",
"UnityEngine.AudioSource.DisableGamepadOutput",
"UnityEngine.AudioSource.SetGamepadSpeakerMixLevel",
"UnityEngine.AudioSource.SetGamepadSpeakerMixLevelDefault",
"UnityEngine.AudioSource.SetGamepadSpeakerRestrictedAudio",

"UnityEngine.AudioSource.GamepadSpeakerSupportsOutputType",
"UnityEngine.AudioSource.gamepadSpeakerOutputType",
"UnityEngine.AudioSource.PlayOneShot",

"UnityEngine.MeshRenderer.scaleInLightmap",
"UnityEngine.MeshRenderer.receiveGI",
"UnityEngine.MeshRenderer.stitchLightmapSeams",

"UnityEngine.QualitySettings.GetAllRenderPipelineAssetsForPlatform",

"UnityEngine.Rendering.RenderPipelineAsset.terrainBrushPassIndex",
//"UnityEngine.Rendering.VideoShadersIncludeMode",
"UnityEngine.Rendering.GraphicsSettings.videoShadersIncludeMode",
"UnityEngine.Rendering.ScriptableRenderContext.EmitWorldGeometryForSceneView",
"UnityEngine.UI.DefaultControls.factory",

};

        // black list if white list not given
        public static void OnGetNoUseList(out List<string> list)
        {
            list = new List<string>
            {
                "HideInInspector",
                "ExecuteInEditMode",
                "AddComponentMenu",
                "ContextMenu",
                "RequireComponent",
                "DisallowMultipleComponent",
                "SerializeField",
                "AssemblyIsEditorAssembly",
                "Attribute",
                "Types",
                "UnitySurrogateSelector",
                "TrackedReference",
                "TypeInferenceRules",
                "FFTWindow",
                "RPC",
                "Network",
                "MasterServer",
                "BitStream",
                "HostData",
                "ConnectionTesterStatus",
                "GUI",
                "EventType",
                "EventModifiers",
                "FontStyle",
                "TextAlignment",
                "TextEditor",
                "TextEditorDblClickSnapping",
                "TextGenerator",
                "TextClipping",
                "Gizmos",
                "ADBannerView",
                "ADInterstitialAd",
                "Android",
                "Tizen",
                "jvalue",
                "iPhone",
                "iOS",
                "Windows",
                "CalendarIdentifier",
                "CalendarUnit",
                "CalendarUnit",
                "ClusterInput",
                "FullScreenMovieControlMode",
                "FullScreenMovieScalingMode",
                "Handheld",
                "LocalNotification",
                "NotificationServices",
                "RemoteNotificationType",
                "RemoteNotification",
                "SamsungTV",
                "TextureCompressionQuality",
                "TouchScreenKeyboardType",
                "TouchScreenKeyboard",
                "MovieTexture",
                "UnityEngineInternal",
                "Terrain",
                "Tree",
                "SplatPrototype",
                "DetailPrototype",
                "DetailRenderMode",
                "MeshSubsetCombineUtility",
                "AOT",
                "Social",
                "Enumerator",
                "SendMouseEvents",
                "Cursor",
                "Flash",
                "ActionScript",
                "OnRequestRebuild",
                "Ping",
                "ShaderVariantCollection",
                "SimpleJson.Reflection",
                "CoroutineTween",
                "GraphicRebuildTracker",
                "Advertisements",
                "UnityEditor",
                "WSA",
                "EventProvider",
                "Apple",
                "ClusterInput",
                "Motion",
                "UnityEngine.UI.ReflectionMethodsCache",
                "NativeLeakDetection",
                "NativeLeakDetectionMode",
                "WWWAudioExtensions",
                "UnityEngine.Experimental",
                "Unity.Jobs",
                "Unity.Collections",
                "Unity.IO.LowLevel",
                "UnityEngine.AudioSettings",
                "UnityEngine.DrivenRectTransformTracker",
                "UnityEngine.tvOS",
                "UnityEngine.Light",
                "UnityEngine.LightProbeGroup",
                "UnityEngine.Playables",
                "UnityEngine.LineRenderer",
                "UnityEngine.TrailRenderer",
                "UnityEngine.Rendering.CommandBuffer",
                "Unity.Profiling.LowLevel.Unsafe.ProfilerCategoryDescription",
                "Unity.Profiling.LowLevel.Unsafe.ProfilerMarkerData",
                "Unity.Profiling.LowLevel.Unsafe.ProfilerRecorderDescription",
                "Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility",
                "Unity.Profiling.LowLevel.Unsafe.ProfilerUnsafeUtility",
            };

        }
    }
}
