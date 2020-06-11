using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ManagerGameUI", menuName = "Manager/Game UI")]
public class ManagerGameUI : ManagerBase, IAwake, IEventHandler, IMustBeWipe
{
    [SerializeField] 
    private GameObject newWaveTextPrefab;
    [SerializeField]
    private GameObject diedMenuPrefab;
    [SerializeField]
    private GameObject HealthBarBrefab;
    [SerializeField]
    private  DataLevel dataLevel;
    [SerializeField]
    private DataHealth PlayerHP;
   
    private GameObject newWaveText;
    private GameObject diedMenu;

    private ManagerCoroutineComponent managerCorountine;

    public void OnAwake()
    {
        ToolBox.Get<ManagerEvent>().Subscribe(this);
        managerCorountine = ToolBox.Get<ManagerCorountine>().CoroutineComponent;

        var canvas = GameObject.Find("Canvas");
       

        newWaveText = GameObject.Instantiate(newWaveTextPrefab, canvas.transform);
        diedMenu    = GameObject.Instantiate(diedMenuPrefab, canvas.transform);
      
        diedMenu.SetActive(false);
        newWaveText.SetActive(false);        

        var healthBar = GameObject.Instantiate(HealthBarBrefab, canvas.transform).GetComponent<Slider>();
            healthBar.maxValue = PlayerHP.MaxHealth;
            healthBar.value = PlayerHP.Health;

        managerCorountine.StartCoroutine(HealthBarChange(healthBar));

    }
    public void ShowNewWaveText()
    {
        managerCorountine.StartCoroutine(ReloadWaveText());    
    }

    public void ShowDiedMenu()
    {
        diedMenu.GetComponentInChildren<TextMeshProUGUI>().text = $"Level: {dataLevel.Lelv} Wave: {dataLevel.currentWaves}";
        diedMenu.SetActive(true);
    }

    IEnumerator HealthBarChange(Slider healthBar)
    {
           
        while(true)
        {
            yield return new WaitForFixedUpdate();
            if (PlayerHP.Health<=0)
                yield return null;
            if(healthBar.value!=PlayerHP.Health)
            healthBar.value = PlayerHP.Health;    
        }
       
    }
    IEnumerator ReloadWaveText()
    {
        newWaveText.SetActive(false);
        newWaveText.GetComponent<TextMeshProUGUI>().text = dataLevel.currentWaves + " Wawe";
        newWaveText.SetActive(true);
        yield break;
    }
    public void Retry()
    {    
        ToolBox.Get<ManagerEvent>().SendMessage(MessageType.RestartGame, null);
        diedMenu = GameObject.FindGameObjectWithTag("DiedMenu");
        diedMenu.SetActive(false);      
    }
    public void MainMenu()
    {
        ToolBox.Get<ManagerEvent>().SendMessage(MessageType.MainMenu, null);     
    }

    public void Handle(MessageType type, IEvent arg)
    {
        switch (type)
        {           
            case MessageType.PlayerDead:
                {
                    ShowDiedMenu();
                    break;
                }
               
            case MessageType.Pause:
                break;
            case MessageType.RestartGame:
                break;
            case MessageType.MainMenu:
                break;
            case MessageType.NewWaveStart:
                {
                    ShowNewWaveText();
                    break;
                }

            case MessageType.LevelCompleted:
                break;
            default:
                break;
        }
    }
    public void onDispose()
    {
        newWaveText = null;
        diedMenu = null;
    }

}
