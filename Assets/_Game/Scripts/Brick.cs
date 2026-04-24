using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    
    bool isTaken = false;
    public void OnInit()
    {
        isTaken = false;
        brick.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTaken)
        {
            brick.SetActive(false);
            other.GetComponent<Player>().AddBrick();
        }
    }

    private void OnDespawn()
    {
        brick.SetActive(false);
    }
}
