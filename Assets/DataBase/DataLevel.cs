using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevel", menuName = "Data/DataLevel")]
public class DataLevel : ScriptableObject
{
    public int Lelv;
    public int Waves;
    public int currentWaves;
    public int EnemyCountInWave;
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public GameObject PlayerPrefab;  

}
