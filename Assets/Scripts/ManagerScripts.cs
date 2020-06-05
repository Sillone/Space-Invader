using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScriptType
{
    BehaviorMove,
    BehaviourBulletMove,
    BehaviourGetDamage,
    BehaviourLiveBullet,
    BehaviourShootPlayer,
    BehaviourSimpleEnemyMove,
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
            case ScriptType.BehaviourGetDamage:
                return new BehaviourGetDamage();
            case ScriptType.BehaviourLiveBullet:
                return new BehaviourLiveBullet();
            case ScriptType.BehaviourShootPlayer:
                return new BehaviourShootPlayer();
            case ScriptType.BehaviourSimpleEnemyMove:
                return new BehaviourSimpleEnemyMove();
            default:
                return null;
        }

    }

}
