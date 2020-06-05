using UnityEngine;

public class BehaviourGetDamage : BehaviourBase, ICollisionEnter
{
    ActorBase actor;
    DataHealth dataHealth;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;
        dataHealth = ScriptableObject.CreateInstance<DataHealth>();
        dataHealth.Health = actor.GetData<DataHealth>().Health;
    }
    public void CollisionEnter(Collision collision)
    {
        ActorBullet hitactor;
        collision.gameObject.TryGetComponent<ActorBullet>(out hitactor);
        if (hitactor)
        {
            var damage = hitactor.GetData<DataBullet>().Damage;
             dataHealth.Health -= damage;
            if (dataHealth.Health <= 0)
            {
                ToolBox.Get<ManagerPool>().Despawn(actor.PoolType, actor.gameObject);
                ToolBox.Get<ManagerSpawn>().EnemyDead();
            }
        }


    }

}
