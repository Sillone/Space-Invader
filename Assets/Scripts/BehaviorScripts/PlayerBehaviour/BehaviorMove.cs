using System;
using UnityEngine;

[Serializable]
public class BehaviorMove : BehaviourBase, ITick, IFixedTick
{
    private Rigidbody rigidbody;
    private DataMove dataSpeed;
    public override void SetUp(ActorBase _actor)
    {
        
        rigidbody = _actor.gameObject.GetComponent<Rigidbody>();
        dataSpeed = _actor.GetData<DataMove>();
    }

    public void FixedTick()
    {
        var velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * dataSpeed.Speed ;
        rigidbody.velocity = velocity;

    }
    public void Tick()
    {

    }
}
