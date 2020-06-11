using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : IEvent
{
    public int NumberLevel { get; set; }
	public LevelCompleted(int level)
	{
		NumberLevel = level;
	}
}
