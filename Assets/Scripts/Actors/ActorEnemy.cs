using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorEnemy : ActorBase, ITick, IFixedTick, ILateTick, IPoolable
{

    private List<ITick> ticks = new List<ITick>();
    private List<IFixedTick> fixedTicks = new List<IFixedTick>();
    private List<ILateTick> lateTicks = new List<ILateTick>();
    private List<ICollisionEnter> collisioners = new List<ICollisionEnter>();



    [SerializeReference]
    public List<ScriptableObject> Data = new List<ScriptableObject>();
    [SerializeField]
    public List<BehaviourBase> Behaviour = new List<BehaviourBase>(10);

    private Dictionary<Type, object> data = new Dictionary<Type, object>();

    private void Awake()
    {
        var mng = ToolBox.Get<ManagerUpdate>();
        mng.AddTo(this);
        Behaviour.Add(new BehaviourSimpleEnemyMove());
        Behaviour.Add(new BehaviourGetDamage());
        foreach (var item in Data)
        {
            data.Add(item.GetType(), item);
        }
        foreach (var item in Behaviour)
        {
            item.SetUp(this);
            Add(item);
        }

    }

    public void Add(object obj)
    {
        if (obj is IFixedTick)
            fixedTicks.Add(obj as IFixedTick);
        if (obj is ITick)
            ticks.Add(obj as ITick);
        if (obj is ICollisionEnter)
            collisioners.Add(obj as ICollisionEnter);

    }

    public override T GetData<T>()
    {
        object resolve;
        data.TryGetValue(typeof(T), out resolve);
        return (T)resolve;
    }
    public void Tick()
    {
        for (int i = 0; i < ticks.Count; i++)
        {
            ticks[i].Tick();
        }
    }
    public void FixedTick()
    {
        for (int i = 0; i < fixedTicks.Count; i++)
        {
            fixedTicks[i].FixedTick();
        }
    }

    public void LateTick()
    {
        for (int i = 0; i < lateTicks.Count; i++)
        {
            lateTicks[i].LateTick();
        }
    }

    public void OnSpawn()
    {
        foreach (var item in Behaviour)
        {
            //  item.SetUp(this);          
        }
    }

    public void OnDespawn()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in collisioners)
        {
            item.CollisionEnter(collision);
        }
    }
}
