using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    [SerializeField]
    public PoolType PoolType;
    public virtual T GetData<T>() 
    {
        object obj = null;
        return (T)obj;
    }
}
