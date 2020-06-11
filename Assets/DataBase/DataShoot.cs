using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Shoot", menuName = "Data/Data Shoot")]
public class DataShoot : ScriptableObject
{
    [SerializeField]
    public GameObject BulletPrefab;

   
    public int BurstsAmmoCount;
    public float ReloadTime;
    public float delayShoot;


}
