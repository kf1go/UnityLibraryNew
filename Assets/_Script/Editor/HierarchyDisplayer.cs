using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//reduce number of displayed component (ex : if there is multiple collider just show one collider
//tag
//layer
//sorted component?
//static 
//missing mono behaviour
[InitializeOnLoad]
public static class HierachyDisplayer
{
    //private static int firstInstanceID;
    private static GUIContent guiContentCache = new GUIContent();
    //private static readonly Dictionary<int, HierachyComponent> cache = new Dictionary<int, HierachyComponent>(30);
    static HierachyDisplayer()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= HandleOnHierarchyWindowItemOnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += HandleOnHierarchyWindowItemOnGUI;

        /*EditorApplication.hierarchyChanged -= UpdateCacheData;
        EditorApplication.hierarchyChanged += UpdateCacheData;*/

        /*SceneView.duringSceneGui -= OrbitVisual;
        SceneView.duringSceneGui += OrbitVisual;*/

        /*Undo.undoRedoPerformed -= EditorApplication.RepaintHierarchyWindow;
        Undo.undoRedoPerformed += EditorApplication.RepaintHierarchyWindow;*/
    }

    private static void UpdateCacheData()
    {
        //firstInstanceID = -1;
        Debug.Log(SceneManager.sceneCount);
        /*        for(int i = 0; i < SceneManager.sceneCount; i++)
                {

                }
        */
    }

    private static void HandleOnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject == null) return;
        //AlternatingLines(instanceID, selectionRect, gameObject);
        //GameObjectToggle(selectionRect, gameObject);
        CustomHierarchy(instanceID, selectionRect, gameObject);
    }
    private static void OrbitVisual(SceneView obj)
    {
        if (Event.current.alt)
        {
            Vector3 pivot = obj.pivot;
            float handleSize = HandleUtility.GetHandleSize(pivot);
            Vector3 upPoint = Vector3.up * 1.25f;

            Handles.DrawWireArc(pivot, Vector3.up, Vector3.forward, 360, handleSize);
            Handles.DrawLine(pivot, pivot + upPoint);
            Handles.DrawLine(pivot + Vector3.right * handleSize, pivot + upPoint);
        }
    }
    // TODO : make this
    private static void AlternatingLines(int instanceID, Rect selectionRect, GameObject gameObject)
    {
        selectionRect.xMin -= 28;
        bool isBlack = false;
        Color color = isBlack ? Color.black : Color.red;
        EditorGUI.DrawRect(selectionRect, Color.red);
    }
    private static void GameObjectToggle(Rect selectionRect, GameObject gameObject)
    {
        Rect toggleRect = selectionRect;
        toggleRect.x -= 0.25f;
        toggleRect.xMax = toggleRect.xMin + 15;

        Event currentEvent = Event.current;
        bool clicked = currentEvent.type == EventType.MouseDown
           && currentEvent.button == 0
           && toggleRect.Contains(currentEvent.mousePosition);
        if (clicked)
        {
            currentEvent.Use();
            Undo.RecordObject(gameObject, "Toggle Active State");
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
    // TODO : optimize this method
    private static void CustomHierarchy(int instanceID, Rect selectionRect, GameObject gameObject)
    {
        // TOOD : reuse Rect or something
        if (gameObject == null)
            return;

        bool isPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject) != null;
        //if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject) != null)
        //    return;

        Component[] components = gameObject.GetComponents<Component>(); // getCompos by list?

        //GUIContent content = new GUIContent();// EditorGUIUtility.ObjectContent(componentBackground, componentBackground.GetType());
        guiContentCache.image = AssetPreview.GetMiniThumbnail(components[0]);
        guiContentCache.tooltip = string.Empty;
        guiContentCache.text = string.Empty;

        /*Transform parentTransform = gameObject.transform.parent;
        if (parentTransform != null)
        {
            Rect test = selectionRect;
            float height = test.height * 0.45f;
            test.yMin += height;
            test.yMax -= height;
            test.width = gameObject.transform.childCount == 0 ? 20 : 5;
            test.x -= 20;
            EditorGUI.DrawRect(test, Color.red);

            Rect test2 = selectionRect;
            test2.width = test.height;
            test2.x = test.x - 2.5f;

            // TOOD : this feature works but i disabled this because of the optimazation
            bool isLastSibiling = false;//gameObject.transform == parentTransform.GetChild(parentTransform.childCount - 1);
            if (isLastSibiling)
            {
                test2.yMax -= 7.5f;
            }
            EditorGUI.DrawRect(test2, Color.red);
        }*/

        Rect backgroundRect = selectionRect;
        backgroundRect.width = 17f;
        bool isSelected = Selection.Contains(instanceID);
        bool isHovering = selectionRect.Contains(Event.current.mousePosition);
        Color backgroundColor = GetColor(isSelected, isHovering);

        if (!isPrefab)
        {
            //clear
            EditorGUI.DrawRect(backgroundRect, backgroundColor);

            //background draw
            EditorGUI.LabelField(backgroundRect, guiContentCache);

        }

        Rect componentListRect = selectionRect;
        componentListRect.xMin = selectionRect.xMax - selectionRect.height;

        int monoCnt = 0;
        int componentsLength = components.Length;
        for (int i = 1; i < componentsLength; i++)
        {
            Component item = components[i];
            //string namespaceStr = item.GetType().Namespace;

            //ban list
            /*            bool isBannedNamespace = !string.IsNullOrEmpty(namespaceStr)
                            && (namespaceStr.StartsWith(nameof(UnityEditor))
                            || namespaceStr.StartsWith(nameof(TMPro)));
            */
            bool isMono = true;// !isBannedNamespace;

            if (!isMono || item == null)
            {
                continue;
            }
            if (item == null)
            {
                continue;
            }

            guiContentCache = EditorGUIUtility.ObjectContent(item, item.GetType());

            guiContentCache.tooltip = guiContentCache.text;
            guiContentCache.text = string.Empty;

            Rect componentRect = componentListRect;
            componentRect.x -= monoCnt * 17;
            EditorGUI.DrawRect(componentRect, new Color(0.4f, 0.4f, 0.4f));
            EditorGUI.LabelField(componentRect, guiContentCache);

            monoCnt++;
        }

        /*bool isDirty = EditorUtility.IsDirty(instanceID);
        if (!isDirty)
        {
            for (int i = 1; i < componentsLength; i++)
            {
                isDirty = EditorUtility.IsDirty(components[i]);
                if (isDirty)
                {
                    break;
                }
            }
        }
        if (isDirty)
        {
            Rect dirtyPosition = selectionRect;
            dirtyPosition.x -= 1.5f;
            dirtyPosition.width = 1.5f;
            EditorGUI.DrawRect(dirtyPosition, GeneralEditorUtility.ColorUtility.Hierachy.NewBlue);
        }*/
    }
    private static Color GetColor(bool isSelected, bool isHovering)
    {
        if (isSelected)
        {
            return Color.blue;//GeneralEditorUtility.ColorUtility.Hierachy.NewBlue;
        }
        if (isHovering)
        {
            return Color.blue;//GeneralEditorUtility.ColorUtility.Hierachy.NewBlue;
        }
        Color white = Color.white;
        white *= 0.5f;
        white.a = 1;
        return white;//GeneralEditorUtility.ColorUtility.Hierachy.Default;
    }
}
