using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScriptType
{
    BehaviorMove,
    BehaviourBulletMove,
    BehaviourEnemyGetDamage,
    BehaviourPlayerGetDamage,
    BehaviourLiveBullet,
    BehaviourShootPlayer,
    BehaviourSimpleEnemyMove,
    BehaviourEnemyShoot

}
[CreateAssetMenu(fileName = "ManagerEvent", menuName = "Manager/ManagerScript")]
public class ManagerScripts : ManagerBase
{ 
    public object GetScript(ScriptType type)
    {
        switch (type)
        {
            case ScriptType.BehaviorMove:
                return new BehaviorMove();
            case ScriptType.BehaviourBulletMove:
                return new BehaviourBulletMove();
            case ScriptType.BehaviourEnemyGetDamage:
                return new BehaviourEnemyGetDamage();
            case ScriptType.BehaviourPlayerGetDamage:
                return new BehaviourPlayerGetDamage();
            case ScriptType.BehaviourLiveBullet:
                return new BehaviourLiveBullet();
            case ScriptType.BehaviourShootPlayer:
                return new BehaviourShootPlayer();
            case ScriptType.BehaviourSimpleEnemyMove:
                return new BehaviourSimpleEnemyMove();
            case ScriptType.BehaviourEnemyShoot:
                return new BehaviourEnemyShoot();
            default:
                return null;
        }
        
    }

}
