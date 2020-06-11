using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerCoroutine", menuName = "Manager/ManagerCoroutine")]
public class ManagerCorountine : ManagerBase,IAwake,IMustBeWipe
{
    public ManagerCoroutineComponent CoroutineComponent { get; private set; }
    public void OnAwake()
    {
       CoroutineComponent = GameObject.Find("[SETUP]").AddComponent<ManagerCoroutineComponent>();
    }

    public void onDispose()
    {
        CoroutineComponent.StopAllCoroutines();
        Component.Destroy(CoroutineComponent);
        CoroutineComponent = null;
    }
}
