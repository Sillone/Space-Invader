using System.Collections;
using UnityEngine;

public class BehaviourEnemyShoot : BehaviourBase, IFixedTick, IEventHandler, IAwake
{
    DataShoot dataShoot;
    private ManagerPool managerPool;
    private Transform ParentBulletTransform;
    private Transform actorTransform;
    private bool isShooting = false;
    private bool IsPaused = false;
    float reloadTime = 0;
    float delayTime  = 0;
    float countShell = 0;
    public override void SetUp(ActorBase _actor)
    {
        ParentBulletTransform = GameObject.Find("BulletObject").transform;
        dataShoot = _actor.GetData<DataShoot>();
        actorTransform = _actor.transform;
        managerPool = ToolBox.Get<ManagerPool>();
        _actor.gameObject.name = actorTransform.gameObject.GetInstanceID().ToString();
       
    }
    public void OnAwake()
    {
        reloadTime = Random.Range(0.5f, 2.5f);
    }
    public void FixedTick()
    {
        //if ((actorTransform.gameObject.GetComponent<IPoolable>().IsPoolable == true) && (IsPaused == false))
        //{
        //    if (isShooting == false)
        //    {
        //        isShooting = true;
        //        ToolBox.Get<ManagerCorountine>().CoroutineComponent.StartCoroutine(Shoot());
        //    }
        //}
        //else
        //{
        //    isShooting = false;
        //}
        
        reloadTime += Time.deltaTime;
        if (reloadTime >= dataShoot.ReloadTime)
        {
            
            if(countShell<dataShoot.BurstsAmmoCount)
            {
                delayTime += Time.deltaTime;
                if (delayTime >= dataShoot.delayShoot)
                {
                    managerPool.Spawn(PoolType.Bullet, dataShoot.BulletPrefab, position: actorTransform.position, rotation: actorTransform.rotation, parent: ParentBulletTransform).layer=12;                    
                    delayTime = 0;
                    countShell++;
                }
            }
            else
            {
                reloadTime = 0;
                countShell = 0;
            }
            

        }


    }

    //IEnumerator Shoot()
    //{
    //    yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

    //    while (isShooting)
    //    {
    //        if ((actorTransform.gameObject.GetComponent<IPoolable>().IsPoolable == true) && (IsPaused == false))
    //        {
    //            for (int i = 0; i < dataShoot.BurstsAmmoCount; i++)
    //            {
    //                yield return new WaitForSeconds(dataShoot.delayShoot);
    //                var sheel = managerPool.Spawn(PoolType.Bullet, dataShoot.BulletPrefab, position: actorTransform.position, rotation: actorTransform.rotation, parent: ParentBulletTransform);
    //                sheel.layer = 12;
    //            }
    //            yield return new WaitForSeconds(dataShoot.ReloadTime);

    //        }
    //        else
    //            isShooting = false;

    //    }
        
    //    yield break;
    //}
 
    public void Handle(MessageType type, IEvent arg)
    {
        if (type == MessageType.Pause)
        {
            if (dataShoot)
                IsPaused = (arg as GamePause).IsPaused;
        }
    }

  
}

