using UnityEngine;

public class BehaviourShootPlayer : BehaviourBase, ITick,IAwake
{
    private Transform parentBulletTransform;

    private ActorBase actor;
    ManagerPool pool;

    private DataShoot dataShoot;

    //private bool isShooting;
    //private bool IsPaused;

    float reloadTime = 0;
    float delayTime = 0;
    float countShell = 0;

    public void OnAwake()
    {
        reloadTime = dataShoot.ReloadTime;
        delayTime += dataShoot.delayShoot;
    }

    public override void SetUp(ActorBase _actor)
    {
        parentBulletTransform = GameObject.Find("BulletObject").transform;
        actor = _actor;
        pool = ToolBox.Get<ManagerPool>();
        dataShoot = actor.GetData<DataShoot>();
    }

    public void Tick()
    {
        //if ((actor.gameObject.GetComponent<IPoolable>().IsPoolable == true) && (IsPaused == false))
        //{
        //    if(isShooting==false)
        //    isShooting = true;
        //    ToolBox.Get<ManagerCorountine>().CoroutineComponent.StartCoroutine(Shoot());
        //}
        //else
        //{
        //    isShooting = false;
        //}
        reloadTime += Time.deltaTime;
        delayTime += Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space)) && (reloadTime >= dataShoot.ReloadTime))
      {

            if (countShell < dataShoot.BurstsAmmoCount)
            {
                
                if (delayTime >= dataShoot.delayShoot)
                {
                    pool.Spawn(PoolType.Bullet, dataShoot.BulletPrefab, position: actor.transform.position, rotation: actor.transform.rotation, parent: parentBulletTransform).layer = 13;
                    countShell++;
                    delayTime = 0;
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
    //    while (isShooting)
    //    {
    //        if(Input.GetKeyDown(KeyCode.Space))
    //        {
    //            for (int i = 0; i < dataShoot.BurstsAmmoCount; i++)
    //            {
    //                var sheel = pool.Spawn(PoolType.Bullet, dataShoot.BulletPrefab, position: actor.transform.position, rotation: actor.transform.rotation, parent: parentBulletTransform);
    //                sheel.layer = 13;
    //                yield return new WaitForSeconds(dataShoot.delayShoot);
    //            }

    //        }
    //        yield return new WaitForSeconds(dataShoot.ReloadTime);
    //    }
    //    yield break;
    //}

}
