using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manager Event", menuName = "Manager/Manager Event")]
public class ManagerEvent : ManagerBase, IMustBeWipe
{
    private readonly List<IEventHandler> handlers = new List<IEventHandler>();


    public void Subscribe(IEventHandler handler) 
    {      
        handlers.Add(handler);
    }
    public void SendMessage(MessageType type,IEvent arg)
    {
        for (int i = 0; i < handlers.Count; i++)
        {
            handlers[i].Handle(type, arg);
        }
           
    }

    public void onDispose()
    {
        handlers.Clear();
    }
}
