
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ManagerGameLogic", menuName = "Manager/Game Logic")]
public class ManagerGameLogic : ManagerBase, IAwake, IEventHandler, IMustBeWipe
{
    [SerializeField]
    private DataLevel currentLevl;

    public int CurrentLevel { get; private set; }

    private GameObject EnemySpawnPosition;

    private GameObject PlayerSpawnPosition;

    private List<ActorBase> enemyInWave = new List<ActorBase>();

    private ActorBase player;
    [SerializeField]
    private Transform LaserWall;
    ManagerCoroutineComponent managerCorountine;
    private ManagerPool managerPool;

    private bool weveReset = false;

    public void OnAwake()
    {
        EnemySpawnPosition = GameObject.FindGameObjectWithTag("SpawnEnemy");
        PlayerSpawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer");

        managerPool = ToolBox.Get<ManagerPool>();
        ToolBox.Get<ManagerEvent>().Subscribe(this);

        LaserWall = GameObject.Instantiate(LaserWall.gameObject).transform;
        managerCorountine = ToolBox.Get<ManagerCorountine>().CoroutineComponent;
        StartGame();
    }

    private void StartGame()
    {
        currentLevl.currentWaves = 1;

        ResetWave();

    }

    private void ResetWave()
    {
        if (!weveReset)
        {
            managerCorountine.StartCoroutine(SetupWave());
        }

    }
    IEnumerator SetupWave()
    {
        weveReset = true;

        ToolBox.Get<ManagerPauseGame>().SetPause(true);

        ClearArena();

        if (player == null)
            player = managerPool.Spawn(PoolType.Entities, currentLevl.PlayerPrefab, parent: PlayerSpawnPosition.transform).GetComponent<ActorBase>();
        else if (player.gameObject.activeSelf == false)
            managerPool.Spawn(PoolType.Entities, currentLevl.PlayerPrefab, parent: PlayerSpawnPosition.transform).GetComponent<ActorBase>();
        else
            player.transform.localPosition = Vector3.zero;


        LaserWall.position = new Vector3((40 - 20 * currentLevl.currentWaves), 0, 0);



        int enemyId = 0;
        for (int SpawnPosition = currentLevl.currentWaves; SpawnPosition > 0; SpawnPosition--)
        {
            int enemyCount = 0;
            while (enemyCount < currentLevl.EnemyCountInWave)
            {
                var position = CalculatePosition(SpawnPosition, enemyCount);
                enemyInWave.Add(managerPool.Spawn(PoolType.Enemy, currentLevl.EnemyPrefabs[enemyId], position, EnemySpawnPosition.transform.localRotation, EnemySpawnPosition.transform).GetComponent<ActorBase>());
                enemyCount++;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            enemyId += 1;
        }

        ToolBox.Get<ManagerEvent>().SendMessage(MessageType.NewWaveStart, null);

        ToolBox.Get<ManagerPauseGame>().SetPause(false);

        weveReset = false;
        yield break;
    }
    private void ClearArena()
    {
        if (enemyInWave.Count > 0)
        {
            for (int enemy = 0; enemy < enemyInWave.Count; enemy++)
            {
                managerPool.Despawn(enemyInWave[enemy].PoolType, enemyInWave[enemy].gameObject);
            }
            enemyInWave.Clear();
        }
        var Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (Bullets != null)
        {
            for (int i = 0; i < Bullets.Length; i++)
            {
                managerPool.Despawn(PoolType.Bullet, Bullets[i]);
            }
        }
    }


    public Vector3 CalculatePosition(int spawnPos, int enemyCount)
    {
        var SpawnZone = EnemySpawnPosition.GetComponent<BoxCollider>();

        var scale = SpawnZone.size;
        var step = scale.z / currentLevl.Waves;

        float z = 0 - (scale.z / 2) + step * spawnPos - step;
        float x = 0 - (scale.x / 2) + ((scale.x / currentLevl.EnemyCountInWave) * enemyCount);
        Vector3 spawnPosition = new Vector3(x, 0, z + Random.Range(0f, 10f));

        return spawnPosition;
    }

    public void Handle(MessageType messageType, IEvent messageArg)
    {
        switch (messageType)
        {
            case MessageType.EnemyDied:
                {
                    var arg = messageArg as SomeoneDied;
                    OnEnemyDead(arg.DiedActor);
                    break;
                }
            case MessageType.PlayerDead:
                {
                    ToolBox.Get<ManagerPauseGame>().SetPause(true);
                    var arg = messageArg as SomeoneDied;
                    OnPlayerDead(arg.DiedActor);
                    break;
                }
            case MessageType.RestartGame:
                {
                    StartGame();
                    break;
                }

            case MessageType.MainMenu:
                {
                    SceneManager.LoadScene("MainMenu");
                    ToolBox.ClearScene();
                    break;
                }
            default:
                break;
        }
    }

    private void OnPlayerDead(ActorBase actorPlayer)
    {
        actorPlayer.GetData<DataHealth>().ResetHelth();
    }

    private void OnEnemyDead(ActorBase enemyActor)
    {
        if (enemyInWave.Contains(enemyActor))
        {
            enemyInWave.Remove(enemyActor);
        }
        else return;
        if (enemyInWave.Count == 0)
        {

            enemyInWave.Clear();
            if (currentLevl.currentWaves < currentLevl.Waves)
            {
                currentLevl.currentWaves++;
                ResetWave();
            }
            else
            {
                ToolBox.Get<ManagerEvent>().SendMessage(MessageType.LevelCompleted, new LevelCompleted(currentLevl.Lelv));
                currentLevl.Lelv += 1;
                StartGame();
            }
        }
    }
    public void onDispose()
    {
        managerPool = null;
        EnemySpawnPosition = null;
        PlayerSpawnPosition = null;
        managerCorountine = null;
        enemyInWave.Clear();

    }

}
