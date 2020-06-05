using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActorPlayer))]
public class ActorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ActorPlayer actor = (ActorPlayer)target;
        base.OnInspectorGUI();
    }
    
}
