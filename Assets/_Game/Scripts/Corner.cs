using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private Animator anim;
    
    private DataManager data => DataManager.Instance;
    
    bool isTaken = false;

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
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("take");
            if (!isTaken)
            {
                brick.SetActive(false);
                other.GetComponent<Player>().AddBrick();
                data.AddScore(1);
                isTaken = true;
            }
        }
        
    }
    
    
}
