using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SomeoneDied: IEvent
{
    public ActorBase DiedActor{ get; }
    public SomeoneDied(ActorBase actor)
    {
        DiedActor = actor;
    }
}
