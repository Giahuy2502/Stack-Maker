using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    private GameManager manager => GameManager.Instance;

    private void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        manager.onWinGame.AddListener(SetOffFirework);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.OnWinGame();
        }
    }

    private void SetOffFirework()
    {
        foreach (var firework in fireworks)
        {
            firework.Play();
        }
    }

    private void OnDisable()
    {
        OnDespawn();
    }

    private void OnDespawn()
    {
        manager.onWinGame.RemoveListener(SetOffFirework);
    }
    
}
