using UnityEngine;
using Core;
#if UNITY_EDITOR
using Core.Lib;
using Core.Tasks;
using UnityEditor;

namespace Lib
{
    [InitializeOnLoad]
    public class HierarchyTaskHelper
    {
        private const string Path = "Assets/Core/Lib/MyTasks/Icons/";

        private static Texture2D _imytask = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "imytask.png");
        private static Texture2D _sequence = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "sequence.png");
        private static Texture2D _parallel = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "parallel.png");

        private static Texture2D _wait = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "wait.png");
        private static Texture2D _trigger = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "trigger.png");

        private static Texture2D _activate = AssetDatabase.LoadAssetAtPath<Texture2D>(Path + "activate.png");

        static HierarchyTaskHelper() => EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj is null)
                return;

            TryDrawTask(obj, selectionRect);
            TryDrawTrigger(obj, selectionRect);
        }

        private static void TryDrawTask(GameObject obj, Rect selectionRect)
        {
            var current = obj.GetComponent<IMyTask>();

            if (current is null)
                return;

            var icon = current switch
            {
                TaskSequence => _sequence,
                TaskParallel => _parallel,
                TaskAwaitSecond => _wait,
                TaskSetActive => _activate,
                _ => _imytask
            };

            if (icon is not null)
                GUI.DrawTexture(new Rect(selectionRect.x - 30f, selectionRect.y, 16f, 16f), icon);

            var task = obj.GetComponentInChildren<IMyTask>();

            if (!task.InProgress)
                return;

            EditorGUI.DrawRect(selectionRect, new Color(0.2f, 0.6f, 0.1f, .25f));
        }

        private static void TryDrawTrigger(GameObject obj, Rect selectionRect)
        {
            var current = obj.GetComponent<ITrigger>();

            if (current is null)
                return;

            var icon = current switch
            {
                Trigger => _trigger,
                _ => null
            };

            if (icon is null)
                return;

            GUI.DrawTexture(new Rect(selectionRect.x - 30f, selectionRect.y, 16f, 16f), icon);
        }
    }
}
#endif