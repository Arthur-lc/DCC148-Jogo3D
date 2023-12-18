using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool{
	private GameObject prefab;
	private GameObject[] queue;
	private int index = 0; //posição do proximo objeto a ser retornado pela pool
    private int poolSize;



	public ObjectPool(GameObject prefab, int poolSize)
	{
	    this.prefab = prefab;
	    this.poolSize = poolSize;
	    queue = new GameObject[poolSize];
	    for(int i = 0; i < this.poolSize; i++)
	    {
			GameObject obj = Object.Instantiate(prefab);
			obj.SetActive(false);
			queue[i] = obj;
	    }
	}

	public GameObject GetFromPool()
	{
	    GameObject obj = queue[index];
	    obj.SetActive(true);

		index++;
		index %= poolSize;
	    
	    return obj;
	}

}
