using System.Collections.Generic;
using UnityEngine;

public class BrickPool : MonoBehaviour
{
   
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int poolSize = 50;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject brick = Instantiate(brickPrefab);
            brick.SetActive(false);
            brick.transform.SetParent(this.transform);
            pool.Enqueue(brick);
        }
    }

    public GameObject GetBrick()
    {
        if (pool.Count > 0)
        {
            GameObject brick = pool.Dequeue();
            brick.SetActive(true);
            return brick;
        }
        GameObject newBrick = Instantiate(brickPrefab);
        newBrick.transform.SetParent(this.transform);
        return newBrick;
    }

    public void ReturnBrick(GameObject brick)
    {
        brick.SetActive(false);
        brick.transform.SetParent(this.transform);
        pool.Enqueue(brick);
    }
}
