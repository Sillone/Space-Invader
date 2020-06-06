using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourShootPlayer : BehaviourBase ,ITick
{
    private ActorBase actor;
    ManagerPool pool;
    private Queue<GameObject> poolables = new Queue<GameObject>();
    private DataShoot dataShoot;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;
        pool = ToolBox.Get<ManagerPool>();
        dataShoot = actor.GetData<DataShoot>();
    }

    public void Tick()
    {
        if(Input.GetKey(KeyCode.Space))
        {
          var shell = pool.Spawn(PoolType.Bullet,dataShoot.BulletPrefab, rotation:actor.transform.localRotation,parent:actor.gameObject.transform);
            shell.layer = 13;
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
