using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable 
{	
	bool IsPoolable { get; set; }
    void OnSpawn();

    void OnDespawn();
}
