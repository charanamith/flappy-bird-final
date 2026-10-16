using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/Remove Missing Script From Main Camera")]
    static void RemoveMissingScript()
    {
        GameObject camera = GameObject.Find("Main Camera");

        if (camera == null)
        {
            Debug.LogError("Main Camera was not found!");
            return;
        }

        int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(camera);

        EditorUtility.SetDirty(camera);

        Debug.Log(
            "Missing scripts removed from Main Camera: " + removed
        );
    }
}