using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "ManagerMainMenuUI", menuName = "Manager/MainMenu UI")]
public class ManagerMenuUI : ManagerBase,IAwake
{
    [SerializeField]
    private GameObject meinMenuPanel;

    private GameObject menu;
    public void OnAwake()
    {
        var canvasGO = GameObject.Find("Canvas");
        menu =  GameObject.Instantiate(meinMenuPanel, canvasGO.transform);
    }

    public void Play()
    {
        ToolBox.ClearScene();
        GameObject.Destroy(menu);
        SceneManager.LoadScene("Game");
      
    }

    public void Quit()
    {
        Application.Quit();
    }

}
