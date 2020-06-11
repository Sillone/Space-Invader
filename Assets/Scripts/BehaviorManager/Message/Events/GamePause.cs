using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : IEvent
{
    public bool IsPaused { get; set; }
	public GamePause(bool value)
	{
		IsPaused = value;
	}
}
