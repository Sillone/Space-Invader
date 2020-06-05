using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourShootPlayer : BehaviourBase ,ITick
{
    private ActorBase actor;
    ManagerPool pool;
    private List<GameObject> poolables = new List<GameObject>();
    private DataShoot dataShoot;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;
        pool = ToolBox.Get<ManagerPool>();
        dataShoot = actor.GetData<DataShoot>();
    }

    public void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            poolables.Add(pool.Spawn(PoolType.Bullet,dataShoot.BulletPrefab, rotation:actor.transform.localRotation,parent:actor.gameObject.transform));
            
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach (var item in poolables)
            {
                pool.Despawn(item.GetComponent<ActorBase>().PoolType, item);
            }
            poolables.Clear();
        }
    }

  
}
