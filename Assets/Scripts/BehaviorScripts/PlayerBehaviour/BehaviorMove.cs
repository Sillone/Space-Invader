using System;
using UnityEngine;

[Serializable]
public class BehaviorMove : IFixedTick, ITick
{
    private Rigidbody rigidbody;
    private DataMove dataSpeed;
    public BehaviorMove(ActorPlayer actor)
    {
        rigidbody = actor.gameObject.GetComponent<Rigidbody>();
        dataSpeed = actor.GetData<DataMove>();
    
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
