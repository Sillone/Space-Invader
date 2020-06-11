using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUpdate : ManagerBase, IAwake,IEventHandler
{
    private  List<ITick> ticks = new List<ITick>();
    private  List<IFixedTick> fixedTicks = new List<IFixedTick>();
    private  List<ILateTick> lateTicks = new List<ILateTick>();
    private bool isPaused = false;

    public void AddTo(object updateble)
    {
        var mngUpdate = ToolBox.Get<ManagerUpdate>();
        if (updateble is IFixedTick)
            mngUpdate.ticks.Add(updateble as ITick);

        if (updateble is IFixedTick)
            mngUpdate.fixedTicks.Add(updateble as IFixedTick);

        if (updateble is ILateTick)
            mngUpdate.lateTicks.Add(updateble as ILateTick);
    }

    public void RemoveFrom(object updateble)
    {
        var mngUpdate = ToolBox.Get<ManagerUpdate>();
        if (updateble is IFixedTick)
            mngUpdate.ticks.Remove(updateble as ITick);

        if (updateble is IFixedTick)
            mngUpdate.fixedTicks.Remove(updateble as IFixedTick);

        if (updateble is ILateTick)
            mngUpdate.lateTicks.Remove(updateble as ILateTick);
    }
    public void Tick()
    {
        if (!isPaused)
            for (int i = 0; i < ticks.Count; i++)
        {
            ticks[i].Tick();
        }
    }
    public void FixedTick()
    {
        if (!isPaused)
            for (int i = 0; i < fixedTicks.Count; i++)
        {
           fixedTicks[i].FixedTick();
        }
    }
     public void LateTick()
    {
        if(!isPaused)
        for (int i = 0; i < lateTicks.Count; i++)
        {
           lateTicks[i].LateTick();
        }
    }

    public void OnAwake()
    {
            GameObject.Find("[SETUP]").AddComponent<ManagerUpdateComponent>().Setup(this);       
    }

    public void Handle(MessageType type, IEvent arg)
    {
        if (type == MessageType.Pause)
            isPaused =(arg as GamePause).IsPaused;
    }
}
