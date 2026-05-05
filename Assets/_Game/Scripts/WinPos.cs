using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    [SerializeField] private GameObject chestClose,chestOpen;
    private GameManager manager => GameManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    void OnInit()
    {
        if (manager == null) return;
        manager.onWinGame.AddListener(SetOffFirework);
        manager.onWinGame.AddListener(OpenChest);
        CloseChest();
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

    private void OpenChest()
    {
        chestOpen.SetActive(true);
        chestClose.SetActive(false);
    }
    private void CloseChest()
    {
        chestOpen.SetActive(false);
        chestClose.SetActive(true);
    }

    private void OnDisable()
    {
        OnDespawn();
    }

    private void OnDespawn()
    {
        if (manager == null) return;
        manager.onWinGame.RemoveListener(SetOffFirework);
        manager.onWinGame.RemoveListener(OpenChest);
    }
    
}
