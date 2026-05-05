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
        SetToRoad();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTaken)
        {
            brick.SetActive(false);
            other.GetComponent<Player>().AddBrick();
            data.AddScore(1);
            isTaken = true;
            anim.SetTrigger("take");
            SetToWall();
        }
    }

    private void SetToWall()
    {
        this.tag = "Wall";
        gameObject.layer = LayerMask.NameToLayer("Wall");
    }
    private void SetToRoad()
    {
        this.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    
}
