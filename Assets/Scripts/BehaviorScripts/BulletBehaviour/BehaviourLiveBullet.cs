﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourLiveBullet : BehaviourBase, ICollisionEnter
{
    ActorBase actor;

    public override void SetUp(ActorBase _actor)
    {
        actor = _actor;      
    }
    public void CollisionEnter(Collision collision)
    {
         
            ToolBox.Get<ManagerPool>().Despawn(actor.PoolType, actor.gameObject);
        
    }
}
