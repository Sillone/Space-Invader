using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBulletMove : BehaviourBase, IEnable
{
    Rigidbody rigidbodyGO;
    float speed = 10;

    public override void SetUp(ActorBase _actor)
    {
        rigidbodyGO = _actor.GetComponent<Rigidbody>();
        speed = _actor.GetData<DataBullet>().speed;
    }
    public void OnEnable()
    {
        rigidbodyGO.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
    }

   
    
}
