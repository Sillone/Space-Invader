using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourBulletMove", menuName = "Behaviour/Bullet/Move Bullet Behaviour")]
public class BehaviourBulletMove : BehaviourBase
{
    public override void SetUp(ActorBase _actor)
    {
        _actor.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _actor.GetData<DataBullet>().speed,ForceMode.VelocityChange);
    }
}
