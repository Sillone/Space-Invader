using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneGenerator
{
    static SceneGenerator()
    {     
        EditorSceneManager.newSceneCreated += SceneCreating;
    }

    public static void SceneCreating(Scene scene, NewSceneSetup setup, NewSceneMode mode)
    {
  
        Transform camGO = Camera.main.transform;
        Transform lightGO = GameObject.Find("Directional Light").transform;

        Transform setupFolder = new GameObject("[SETUP]").transform;
       
        Transform cam = new GameObject("<Camera>").transform;
        cam.parent = setupFolder;
        camGO.parent = cam;

        Transform lights = new GameObject("<Lights>").transform;
        lights.parent = setupFolder;
        lightGO.parent = lights;

        Transform world = new GameObject("[WORLD]").transform;
        new GameObject("<Static>").transform.parent = world;
        new GameObject("<Dynamic>").transform.parent = world;

        new GameObject("[UI]");
    }

   

}
