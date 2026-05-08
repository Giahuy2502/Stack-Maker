using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject chestClose,chestOpen;
    private LevelManager LevelManager => LevelManager.Instance;
    private GameManager GameManager => GameManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    void OnInit()
    {
        if (LevelManager == null) return;
        LevelManager.onWinGame += SetOffFirework;
        LevelManager.onWinGame += OpenChest;
        CloseChest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.OnWinGame();
            other.transform.position = target.position;
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
        if (LevelManager == null) return;
        LevelManager.onWinGame -= SetOffFirework;
        LevelManager.onWinGame -= OpenChest;
    }
    
}
