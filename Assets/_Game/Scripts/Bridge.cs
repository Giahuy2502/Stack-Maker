using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    bool isPlaced = false;
    public void OnInit()
    {
        isPlaced = false;
        brick.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaced)
        {
            brick.SetActive(true);
            other.GetComponent<Player>().RemoveBrick();
            isPlaced = true;
        }
    }

    private void OnDespawn()
    {
        brick.SetActive(false);
    }
}
