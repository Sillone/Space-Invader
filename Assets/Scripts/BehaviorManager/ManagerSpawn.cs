using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[CreateAssetMenu(fileName ="dd",menuName ="sss")]
public class ManagerSpawn : ManagerBase, IAwake
{
    [SerializeField] private List<GameObject> gameObject = new List<GameObject>();


    private ManagerPool pools;
    private int count;

    public void OnAwake()
    {
        var go = GameObject.Find("<Dynamic>");
        pools = ToolBox.Get<ManagerPool>();
        pools.AdPool(gameObject[1].GetComponent<ActorBase>().PoolType, 10).PopulateWith(gameObject[1], 5, 1);
        pools.AdPool(gameObject[2].GetComponent<ActorBase>().PoolType, 10).PopulateWith(gameObject[1], 30, 1);
        GameObject.Instantiate(gameObject[0],GameObject.Find("Spawn").transform);
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = GameObject.Find("SpawnEnemy").transform.position + new Vector3(0, 0, Random.Range(-10, 10)+5f);
            pools.Spawn(PoolType.Enemy, gameObject[1], pos, GameObject.Find("SpawnEnemy").transform.rotation);
        }
    }
    public void EnemyDead()
    {
        count++;
        if (count==1)
        {
            Vector3 pos = GameObject.Find("SpawnEnemy").transform.position + new Vector3(0, 0, Random.Range(-10, 10) + 5f);
            pools.Spawn(PoolType.Enemy, gameObject[1], pos, GameObject.Find("SpawnEnemy").transform.rotation);
            count = 0;
        }
        

    }
}
