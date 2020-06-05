using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class ActorPlayer : ActorBase, ITick, IFixedTick, ILateTick
{

    private List<ITick> ticks = new List<ITick>();
    private List<IFixedTick> fixedTicks = new List<IFixedTick>();
    private List<ILateTick> lateTicks = new List<ILateTick>();


    [SerializeReference]
    public List<ScriptableObject> Data = new List<ScriptableObject>();

    private Dictionary<Type, object> data = new Dictionary<Type, object>();

    private void Awake()
    {
        var mng = ToolBox.Get<ManagerUpdate>();      
        mng.AddTo(this);    
        foreach (var item in Data)
        {
            data.Add(item.GetType(),item);
        }
       
        Add(new BehaviorMove(this));
        Add(new BehaviourShootPlayer(this));
    }

    public void Add(object obj)
    {
        if (obj is IFixedTick)
            fixedTicks.Add(obj as IFixedTick);
        if (obj is ITick)
            ticks.Add(obj as ITick);
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
            this.gameObject.GetComponent<Animator>().SetFloat("Speed", gameObject.GetComponent<Rigidbody>().velocity.z);
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

}
