using UnityEngine;

public class BehaviourEnemyGetDamage : BehaviourBase, ICollisionEnter,IAwake,IEventHandler
{
    ActorBase actor;
    DataHealth dataHealth;
    private int halthBonus;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;
        dataHealth = ScriptableObject.CreateInstance<DataHealth>();
        dataHealth.Health = actor.GetData<DataHealth>().Health;
      
    }
    public void CollisionEnter(Collision collision)
    {     
        ActorBase hitactor;
        collision.gameObject.TryGetComponent<ActorBase>(out hitactor);
        if (hitactor)
        {
            var damage = hitactor.GetData<DataBullet>().Damage;
            if (dataHealth.Health <= 0)
                return;
             dataHealth.Health -= damage;
            if (dataHealth.Health <= 0 && (actor as IPoolable).IsPoolable)
            {
                ToolBox.Get<ManagerPool>().Despawn(actor.PoolType, actor.gameObject);               
                ToolBox.Get<ManagerEvent>().SendMessage(MessageType.EnemyDied ,new SomeoneDied(actor));               
            }
           
        }


    }

    public void OnAwake()
    {
        dataHealth.Health = actor.GetData<DataHealth>().Health;
        dataHealth.Health += halthBonus; 
    }

    public void Handle(MessageType type, IEvent arg)
    {
        if (type == MessageType.LevelCompleted)
            halthBonus = (arg as LevelCompleted).NumberLevel;
    }
}
