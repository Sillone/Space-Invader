using UnityEngine;

public class BehaviourPlayerGetDamage : BehaviourBase, ICollisionEnter
{
    ActorBase actor;
    DataHealth dataHealth;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;
        dataHealth = actor.GetData<DataHealth>();
    }
    public void CollisionEnter(Collision collision)
    {

        ActorBase hitactor;
        collision.gameObject.TryGetComponent<ActorBase>(out hitactor);
        if (hitactor && hitactor.PoolType == PoolType.Bullet)
        {
            var damage = hitactor.GetData<DataBullet>().Damage;
            dataHealth.Health -= damage;
            if (dataHealth.Health <= 0)
            {
                ToolBox.Get<ManagerPool>().Despawn(actor.PoolType, actor.gameObject);
                ToolBox.Get<ManagerEvent>().SendMessage(MessageType.PlayerDead, new SomeoneDied(actor));
            }
        }
    }

}
