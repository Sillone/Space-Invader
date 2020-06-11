using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool 
{
	private Transform parentPool;

	private List<IPoolable> pooledObjects = new List<IPoolable>();

	private Dictionary<int, Stack<GameObject>> cachedObjects = new Dictionary<int, Stack<GameObject>>();

	private Dictionary<int, int> cachedIds = new Dictionary<int, int>();

	
	public void SetParent(Transform _parent)
	{

		parentPool = _parent;
	}

	public GameObject Spawn(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
	{
		var key = prefab.GetInstanceID();
		Stack<GameObject> stack;

		var stacked = cachedObjects.TryGetValue(key, out stack);

		if(stacked && stack.Count>0)
		{
			var transform = stack.Pop().transform;
			transform.SetParent(parent);
			transform.rotation = rotation;
			transform.gameObject.SetActive(true);
			if (parent == null)
				transform.position = position;
			else
			{
 				transform.localPosition= position;			
			}
				
			var isPoolable = transform.GetComponent<IPoolable>();
			if (isPoolable!=null)
				isPoolable.OnSpawn();
			return transform.gameObject;
		}

		if (!stacked) cachedObjects.Add(key, new Stack<GameObject>());


		var createdPrefab = Populate(prefab, position, rotation, parent);
		cachedIds.Add(createdPrefab.GetInstanceID(), key);
		

		createdPrefab.SetActive(true);

		var poolable = createdPrefab.GetComponent<IPoolable>();
		if (poolable != null)
			poolable.OnSpawn();
		poolable.IsPoolable = true;
		return createdPrefab;
	}

	public void Despawn(GameObject go)
	{
		go.SetActive(false);
		cachedObjects[cachedIds[go.GetInstanceID()]].Push(go);
		var poolable = go.GetComponent<IPoolable>();
		if (poolable != null)
			poolable.OnDespawn();
		if (parentPool != null)
			go.transform.SetParent(parentPool);
		
	}

	public void Dispose()
	{
		parentPool = null;
		cachedIds.Clear();
		cachedObjects.Clear();

	}

	public GameObject Populate(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
	{
		var go = Object.Instantiate(prefab, position, rotation, parent).transform;
		if (parent == null)
			go.position = position;
		else
		{
			go.localPosition = position;		
		}		
		go.gameObject.SetActive(false);	
		return go.gameObject;
	}


}
