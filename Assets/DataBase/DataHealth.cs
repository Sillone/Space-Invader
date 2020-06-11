using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Health", menuName = "Data/Data Health")]
public class DataHealth : ScriptableObject
{
    public float Health;
    public float MaxHealth;

    public void ResetHelth()
    {
        Health = MaxHealth;
    }
}
