using UnityEngine;

public class ManagerUpdateComponent : MonoBehaviour
{
    private ManagerUpdate mng;

    public void Setup(ManagerUpdate managerUpdate)
    {
        mng = managerUpdate;
    }

    private void Update()
    {
        mng.Tick();
    }
    private void FixedUpdate()
    {
        mng.FixedTick();
    }

    private void LateUpdate()
    {
        mng.LateTick();
    }
}
