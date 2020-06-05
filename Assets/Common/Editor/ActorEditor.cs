using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Actor actor = (Actor)target;
        base.OnInspectorGUI();
    }
    
}
