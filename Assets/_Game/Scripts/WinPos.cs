using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject chestClose,chestOpen;
    private LevelManager levelManager => LevelManager.Instance;
    private GameManager manager => GameManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    void OnInit()
    {
        if (levelManager == null) return;
        levelManager.onWinGame.AddListener(SetOffFirework);
        levelManager.onWinGame.AddListener(OpenChest);
        CloseChest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.OnWinGame();
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
        if (levelManager == null) return;
        levelManager.onWinGame.RemoveListener(SetOffFirework);
        levelManager.onWinGame.RemoveListener(OpenChest);
    }
    
}
