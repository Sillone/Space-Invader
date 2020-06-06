using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;

public class BehaviourEnemyShoot : BehaviourBase, IFixedTick
{
    DataShoot dataShoot;
    private float reloadTime;
    private float timeStartShoot;
    private ManagerPool managerPool;
    private Transform transformParent;
    public override void SetUp(ActorBase _actor)
    {
        transformParent = _actor.transform;
        dataShoot = _actor.GetData<DataShoot>();
        managerPool = ToolBox.Get<ManagerPool>();
    }
    public void FixedTick()
    {
        reloadTime += Time.deltaTime;
        if(reloadTime>=dataShoot.ReloadTime)
        {

            managerPool.Spawn(PoolType.Bullet, dataShoot.BulletPrefab, position: default, rotation: transformParent.rotation, parent: transformParent).layer = 12;
            reloadTime = Time.deltaTime;
       
        }
    }

    private void Shoot()
    {
        bool isShoot = false;
        var delayShoot = timeStartShoot;
        for (int i = 0; i < dataShoot.BurstsAmmoCount; i++)
        {
            isShoot = false;
            delayShoot = Time.realtimeSinceStartup;
            while (!isShoot)
            {
                
                if ( Time.realtimeSinceStartup-delayShoot>=dataShoot.delayShoot)
                {
                  
                    isShoot = true;
                }
            }
            
        }

    }
  
}

