using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

[CreateAssetMenu(fileName ="Data Bullet",menuName ="Data/DataBulet")]
public class DataBullet : ScriptableObject
{
    public float Damage;
    public float speed;
}
