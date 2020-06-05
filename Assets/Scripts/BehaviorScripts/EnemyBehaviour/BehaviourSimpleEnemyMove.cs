
using UnityEngine;


public class BehaviourSimpleEnemyMove : BehaviourBase, IFixedTick,ICollisionEnter
{
    private Rigidbody rigibodyGO;
    private float timer;
    private DataEnemyMove dataMove;
    

    public override void SetUp(ActorBase _actor)
    {
        rigibodyGO = _actor.gameObject.GetComponent<Rigidbody>();
        dataMove = _actor.GetData<DataEnemyMove>();
    }
    public void FixedTick()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= dataMove.MaxTime)
        {
            rigibodyGO.velocity = Vector3.zero;
            rigibodyGO.AddForce(CalculateDirection() * dataMove.Speed, ForceMode.VelocityChange);
            timer = 0f;
        }
    }

    private Vector3 CalculateDirection()
    {
        var x = Random.Range(-1f, 1);
        var y = Random.Range(-1f, 1f);
        var dir = new Vector3(x, 0, y).normalized;
        if (dir == Vector3.zero)
            CalculateDirection();
        return dir;

    }

    public void CollisionEnter(Collision collision)
    {
        var tagCol = collision.gameObject.tag;
        if (tagCol == "Wall")
        {
            rigibodyGO.AddForce(collision.contacts[0].normal * dataMove.Speed, ForceMode.VelocityChange);
        }
    }
}
