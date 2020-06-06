using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class Actor : ActorBase, ITick, IFixedTick, ILateTick,IPoolable
{

    private List<ITick> ticks = new List<ITick>();
    private List<IFixedTick> fixedTicks = new List<IFixedTick>();
    private List<ILateTick> lateTicks = new List<ILateTick>();
    private List<ICollisionEnter> collisonsEnter = new List<ICollisionEnter>();
    private List<IEnable> enables= new List<IEnable>();


    [SerializeField]
    private List<ScriptableObject> Data = new List<ScriptableObject>();
    [SerializeField]
    private List<ScriptType> behaviours = new List<ScriptType>();

    private Dictionary<Type, object> data = new Dictionary<Type, object>();

    private void Awake()
    {
        var mngUpdate = ToolBox.Get<ManagerUpdate>();      
        mngUpdate.AddTo(this);    
        foreach (var item in Data)
        {
            data.Add(item.GetType(),item);
        }
        var mngScript = ToolBox.Get<ManagerScripts>();
       
        foreach (var item in behaviours)
        {
            var comp = mngScript.GetScript(item) as BehaviourBase;
            comp.SetUp(this);
            Add(comp);
        }
             
    }

    public void Add(object obj)
    {
        if (obj is ITick)
            ticks.Add(obj as ITick);
        if (obj is IFixedTick)
            fixedTicks.Add(obj as IFixedTick);      
        if (obj is ICollisionEnter)
            collisonsEnter.Add(obj as ICollisionEnter);
        if (obj is IEnable)
            enables.Add(obj as IEnable);
    }

    public override T GetData<T>()
    {
        object resolve;
        data.TryGetValue(typeof(T),out resolve);
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

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in collisonsEnter)
        {
            item.CollisionEnter(collision);
        }
    }
    private void OnEnable()
    {
        foreach (var item in enables)
        {
            item.OnEnable();
        }
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation= Quaternion.identity;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
