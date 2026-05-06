using System.Collections.Generic;
using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected int poolSize = 50;

    protected Queue<GameObject> pool = new Queue<GameObject>();
    protected void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            newObj.transform.SetParent(this.transform);
            pool.Enqueue(newObj);
        }
    }

    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetParent(this.transform);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
        pool.Enqueue(obj);
    }
}