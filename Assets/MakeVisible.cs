using UnityEngine;
using System.Collections;

// Makes a script execute in edit mode.

// By default, script components are only executed in play mode.
// By adding this attribute, each script component will also have
// its callback functions executed while the Editor is not in playmode.

// https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html
[ExecuteInEditMode]
public class makeVisible : MonoBehaviour
{
    // The functions are not called constantly like they are in play mode.
    // - Update is only called when something in the scene changed.
    void Update ()
    {
        GameObject[] obj = (GameObject[]) GameObject.FindObjectsOfType(typeof (GameObject));
        foreach (GameObject o in obj)
            o.hideFlags = HideFlags.None; // https://docs.unity3d.com/ScriptReference/HideFlags.html
    }
}