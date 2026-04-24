using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private GameManager manager => GameManager.Instance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.OnWinGame(other.GetComponent<Player>());
        }
    }

    
}
