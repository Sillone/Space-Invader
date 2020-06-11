using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCoroutineComponent : MonoBehaviour
{   
   public void CoroutineStar(IEnumerator coroutin)
    {
        StartCoroutine(coroutin);
    }
    public void CoroutineStop(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
}
