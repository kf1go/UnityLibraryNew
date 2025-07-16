using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum DebugKey
{
    None = 0,
    DefaultFlag = 1 << 0,
    Temp2 = 1 << 1,
    Temp3 = 1 << 2,
    Temp4 = 1 << 3,
    Temp5 = 1 << 4
}
/// <summary>
/// NOTE : this script must be executed before any other scripts to prevent the Debuginfo collection from being cleared after Debuginfo has been added
/// </summary>
[DefaultExecutionOrder(-2000)]
public class IMGUIMono : MonoSingleton<IMGUIMono>
{
    // TOOD : remove monoBehaviour dependnecy and use RuntimeInitializeLoadType.AfterAssembliesLoaded ?
    // chk multicastDelegate evaulation order;
    protected override MonoSingletonFlags SingletonFlag => MonoSingletonFlags.DontDestroyOnLoad;

    public static DebugKey CurrentDebugKey { get; set; } = DebugKey.DefaultFlag;
    private static bool EnableGUI { get; set; } = false;
    private static readonly List<DebugInfo> debugInfoList = new List<DebugInfo>(16);

    [SerializeField, Range(0, 1)] private float screenMultiplier = 1;
    [SerializeField] private GUISkin debugGuiSkin;

    private void Update()
    {
        debugInfoList.Clear();
        if (Input.GetKeyDown(KeyCode.F10))
        {
            EnableGUI = !EnableGUI;
        }
    }
    private void OnGUI()
    {
        // uncomment all to use matrix based scaling
        // Matrix-based scaling does proper ImGui scaling

        if (!EnableGUI)
        {
            return;
        }

        //Matrix4x4 originalGUIMatrix = GUI.matrix;

        Camera camera = Camera.main;

        int screenHeight = Screen.height;
        int screenWidth = Screen.width;
        int fontSize = Mathf.Min(screenHeight, screenWidth);

        //fontSize = (int)(fontSize * 0.005f * screenMultiplier);   //Matrix based
        fontSize = (int)(fontSize * 0.05f * screenMultiplier);      //Font size based

        //Vector3 matrixScale = new Vector3(fontSize, fontSize, fontSize);
        //matrix4x4 guiMatrixResolutionIndependent = Matrix4x4.Scale(matrixScale);
        /*bool isZaxisZero = guiMatrixResolutionIndependent.m22 == 0;
        if (isZaxisZero)
        {
            Debug.LogWarning($"adjust {nameof(screenMultiplier)}");
        }*/
        //GUI.matrix = guiMatrixResolutionIndependent;

        debugGuiSkin.label.fontSize = fontSize;
        debugGuiSkin.box.fontSize = fontSize;

        int non3DCnt = 0;
        int infoListCount = debugInfoList.Count;
        for (int i = 0; i < infoListCount; i++)
        {
            DebugInfo item = debugInfoList[i];
            int ratioAppliedFontSize = fontSize;
            debugGuiSkin.label.fontSize = ratioAppliedFontSize;
            debugGuiSkin.box.fontSize = ratioAppliedFontSize;

            Rect position;
            Vector2 screenPosition;
            Vector2 size;
            string message = item.message;
            size = new Vector3((int)(item.message.Length * fontSize * 0.61f), fontSize);
            if (item.Is3D) //iterate World Text
            {
                screenPosition = item.GetScreenPos(camera);
                screenPosition.y = screenHeight - screenPosition.y;

                /*screenPosition.x /= fontSize;
                screenPosition.y /= fontSize;*/

                position = new Rect(screenPosition, size);

                GUI.Label(position, message, debugGuiSkin.label);
            }
            else //top left
            {
                screenPosition = new Vector2(5, non3DCnt * fontSize);
                /*screenPosition.x /= fontSize;
                screenPosition.y /= fontSize;*/

                position = new Rect(screenPosition, size);
                GUI.Box(position, message, debugGuiSkin.box);
                non3DCnt++;
            }
        }

        //set original GUI matrix
        //GUI.matrix = originalGUIMatrix;
    }
    public static void DebugText(DebugInfo debugInfo, DebugKey key = DebugKey.DefaultFlag)
    {
        if (!IsDebugAvailable(key))
        {
            return;
        }

        debugInfoList.Add(debugInfo);
    }
    internal static bool IsDebugAvailable(DebugKey key)
    {
        bool result = (key & CurrentDebugKey) > 0 && EnableGUI;
        return result;
    }
}
