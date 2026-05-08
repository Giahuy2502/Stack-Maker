using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    bool isTaken = false;
    private DataManager DataManager => DataManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnDisable()
    {
        OnDespawn();
    }
    
    private void OnDespawn()
    {
        brick.SetActive(false);
    }

    public void OnInit()
    {
        isTaken = false;
        brick.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTaken && other.CompareTag(Variables.PLAYER_TAG))
        {
            brick.SetActive(false);
            other.GetComponent<Player>().AddBrick();
            DataManager.AddScore(1);
            isTaken = true;
        }
    }
}
